using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using YESS.BE;
using YESS.BE.jsonServer;
using _myExel = Microsoft.Office.Interop.Excel;
namespace YESS.DAL
{
    /// <summary>
    /// MedicineDAL is a class of the medicine db
    /// </summary>
    public class MedicineDAL
    {

        /// <summary>
        /// this function gets all the medicines from the db, with the images that belong to them
        /// </summary> 
        /// <returns>list with all the medicines from the db, with the images that belong to them</returns>
        public List<Medicine> GetMedicinesWithImage()
        {
            MyDriveAPI d = new MyDriveAPI();
            var ctx = new MedicineDB();
            foreach (var item in ctx.Medicines.ToList())
            {
                if (item.Image != "")
                    d.DownloadGoogleFileByName(item.PRODUCTNDC);
            }
            return ctx.Medicines.ToList();
        }

        /// <summary>
        /// this function gets all the medicines from the db, without the images that belong to them
        /// </summary>
        /// <returns>list with all the medicines from the db, without the images that belong to them</returns>
        public List<Medicine> GetMedicines()
        {
            var ctx = new MedicineDB();
            
            return ctx.Medicines.ToList();
        }

        /// <summary>
        /// this function downloads the images from the drive
        /// </summary>
        public void downloadImgs()
        {
            var ctx = new MedicineDB();
            MyDriveAPI d = new MyDriveAPI();
            foreach (var item in ctx.Medicines.ToList())
            {
                if (item.Image != "")
                    d.DownloadGoogleFileByName(item.PRODUCTNDC);
            }

        }

        /// <summary>
        /// this function gets from the db the medicine that belong to a specific ndc 
        /// </summary>
        /// <param name="ndc">the ndc that the function gets the medicine that belongs to it </param>
        /// <returns>an object of medicine that it is with the specific ndc</returns>
        public Medicine GetMedicineByID(string ndc)
        {
            using (var ctx = new MedicineDB())
            {
                var medicine = ctx.Medicines.Where(s => s.PRODUCTNDC == ndc).FirstOrDefault();
                return medicine;
            }
        }

        /// <summary>
        /// this function adds an image to a specific medicine 
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that the function adds the image to its</param>
        /// <param name="file"> the file of the image that will be in the drive</param>
        /// <returns>string that describe if the function successed to add the image or not</returns>
        public string AddImage(string ndc, HttpPostedFileBase file)
        {
            //לטפל בבעיה של מחקית המאגר של התרופות בכל הרצה !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            MyDriveAPI d = new MyDriveAPI();
            Medicine medicine = GetMedicineByID(ndc);

            var deleteAnswer = DeleteMedicine(ndc);
            if (deleteAnswer == "התרופה הוסרה בהצלחה ממאגר התרופות")
            {
                medicine.Image = file.FileName;
                d.UplaodFileOnDriveInFolder(file, medicine.PRODUCTNDC, "medicinePic");
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                //Directory.Delete(path);
                var addAnswer = AddMedicine(medicine);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                if (addAnswer == " התרופה התווספה בהצלחה למאגר התרופות")
                {
                    return "התמונה התווספה בהצלחה";
                }
                return "התרופה הוסרה ולא התווספה תמונה חדשה";
            }
            else
            {
                if (medicine == null)
                {
                    return "התמונה לא התווספה בהצלחה ";
                }
                return "נסה שוב";
            }
        }


        /// <summary>
        /// this function gets the tags from the imagga server. this tags describe what the server thinks that there are in the image
        /// </summary>
        /// <param name="CurrentImage">the image instane (with a property of the tags) that this function gets its tags</param>
        /// <returns>the image instane (with a property of the tags) that this function gets its tags</returns>
        public ImageDetails GetTags(ImageDetails CurrentImage)
        {
            string apiKey = "acc_ca769efe70e15c1";
            string apiSecret = "c93b4ef3a865dd1353c03c289af4b5c6";
            string image = CurrentImage.ImagePath;

            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(String.Format("{0}:{1}", apiKey, apiSecret)));

            var client = new RestClient("https://api.imagga.com/v2/tags");
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", String.Format("Basic {0}", basicAuthValue));
            request.AddFile("image", image);
            IRestResponse response = client.Execute(request);

            //
            var res = response.Content;
            Console.WriteLine(res);
            //


            Root TagList = JsonConvert.DeserializeObject<Root>(response.Content);

            foreach (var item in TagList.result.tags)
            {
                CurrentImage.Details.Add(item.tag.en, item.confidence);
            }

            return CurrentImage;

        }

        /// <summary>
        /// this function deletes the image from the drive
        /// </summary>
        /// <param name="ndc">ths ndc of the medicine that its image will be delete by this function</param>
        /// <returns>string that describe if the function successed to delete the image</returns>
        public string DeleteImage(string ndc)
        {
            MyDriveAPI d = new MyDriveAPI();
            using (var ctx = new MedicineDB())
            {
                var medicine = GetMedicineByID(ndc);
                DeleteMedicine(ndc);

                d.deleteFile(medicine.PRODUCTNDC);

                medicine.Image = "";
                AddMedicine(medicine);


            }
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
               Path.GetFileName(ndc + ".png"));
            //Directory.Delete(path);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
            Path.GetFileName(ndc + ".JPEG"));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
            Path.GetFileName(ndc + ".JPG"));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            return "התמונה של התרופה הוסרה בהצלחה";
        }

        /// <summary>
        /// this function adds a medicine to the medecine db
        /// </summary>
        /// <param name="medicine">the medicine that this function will add to the db</param>
        /// <returns>string that tells if this function seccessed to add the medicine to the db</returns>
        public string AddMedicine(Medicine medicine)
        {
            using (var ctx = new MedicineDB())
            {
                ctx.Medicines.Add(medicine);
                ctx.SaveChanges();
            }

            return " התרופה התווספה בהצלחה למאגר התרופות";
        }

        /// <summary>
        /// this function deletes from the db the medicine with the specific ndc and also delete the image that belong its
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that this function deletes</param>
        /// <returns>string that tell if this function seccessed to delete the medicine from the db</returns>
        public string DeleteMedicine(string ndc)
        {
            MyDriveAPI d = new MyDriveAPI();
            using (var ctx = new MedicineDB())
            {
                // var medicine = GetMedicineByID(ndc);
                var medicine = ctx.Medicines.Where(s => s.PRODUCTNDC == ndc).FirstOrDefault();
                ctx.Medicines.Remove(medicine);
                d.deleteFile(medicine.PRODUCTNDC);
                ctx.SaveChanges();
            }
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
               Path.GetFileName(ndc + ".png"));
            //Directory.Delete(path);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
            Path.GetFileName(ndc + ".JPEG"));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
            Path.GetFileName(ndc + ".JPG"));
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return "התרופה הוסרה בהצלחה ממאגר התרופות";
        }


        /// <summary>
        /// this function deletes a medicine for edit its, so it does`nt delete the image 
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that this function deletes</param>
        /// <returns>string that tell if this function seccessed to delete the medicine from the db</returns>
        public string DeleteMedicineforEdit(string ndc)
        {
            using (var ctx = new MedicineDB())
            {
                // var medicine = GetMedicineByID(ndc);
                var medicine = ctx.Medicines.Where(s => s.PRODUCTNDC == ndc).FirstOrDefault();
                ctx.Medicines.Remove(medicine);
                ctx.SaveChanges();
            }
            return "התרופה הוסרה בהצלחה ממאגר התרופות";
        }

        /// <summary>
        /// The function imports medicine details from the excel to the data base
        /// </summary>
        public void ImportDataFromExcel()
        {
            string filePath = "C:\\Users\\User\\Desktop\\15.10 שעה 2030\\medicine.xls";
            _Application excel = new _myExel.Application();
            Worksheet ws = (excel.Workbooks.Open(filePath)).Worksheets[1];
            string name = "", producer = "", genericName = "", active = "", dosageFormName = "",myACTIVE_NUMERATOR_STRENGTH = "", myACTIVE_INGRED_UNIT = "", ndc = "";
            for (int i = 8; i < 100; i++)
            {
                name = Convert.ToString(ws.Cells[2][i].Value2);
                producer = Convert.ToString(ws.Cells[3][i].Value2);
                genericName = Convert.ToString(ws.Cells[4][i].Value2);
                active = Convert.ToString(ws.Cells[5][i].Value2);
                dosageFormName = Convert.ToString(ws.Cells[6][i].Value2);
                myACTIVE_NUMERATOR_STRENGTH = Convert.ToString(ws.Cells[7][i].Value2);
                myACTIVE_INGRED_UNIT = Convert.ToString(ws.Cells[8][i].Value2);
                ndc = Convert.ToString(ws.Cells[10][i].Value2);

                using (var ctx = new MedicineDB())
                {
                    //creat new medicine
                    var drug = new Medicine
                    {
                        Name = name,
                        Producer = producer,
                        GenericName = genericName,
                        ActiveIngredients = active,
                        DosageFormName = dosageFormName,
                        ACTIVE_NUMERATOR_STRENGTH = myACTIVE_NUMERATOR_STRENGTH,
                        ACTIVE_INGRED_UNIT = myACTIVE_INGRED_UNIT,
                        PRODUCTNDC = ndc
                    };
                    ctx.Medicines.Add(drug);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        Exception raise = dbEx;
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                string message = string.Format("{0}:{1}",
                                    validationErrors.Entry.Entity.ToString(),validationError.ErrorMessage);
                                raise = new InvalidOperationException(message, raise);
                            }
                        }
                        throw raise;
                    }

                }
            }
        }
    }
}









 



      
    






