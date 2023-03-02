using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;
using YESS.DAL;
using System.Web;
using System.IO;

namespace YESS.BL
{
    /// <summary>
    ///  MedicineBL is a class of the medicine 
    /// </summary>
    public class MedicineBL
    {

        /// <summary>
        /// this function adds an image to a specific medicine, but anly if this image describes well the medicine
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that the function adds the image to its</param>
        /// <param name="file">the file of the image that will be in the drive</param>
        /// <returns>string that describe if the function successed to add the image or not</returns>
        public string AddImage(string ndc, HttpPostedFileBase file)
        {
            MedicineDAL medicineDAL = new MedicineDAL();
            if (MedicineExistent(ndc) == true)
            {
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"), Path.GetFileName(file.FileName));
                string imagePath = @path;
                file.SaveAs(path);
                List<string> img;
                // calling to the imagga server -> gets the tags
                img = GetTags(imagePath);
                //checks if the tags suit to the instance (medicine)
                bool flageAddImg = CheckTags(img);
                if (flageAddImg == true)
                {
                    medicineDAL.AddImage(ndc, file);
                    return "התמונה התווספה למאגר בהצלחה";
                }
                if(flageAddImg==false)
                {
                    System.IO.File.Delete(path);
                }
                return "התמונה לא התווספה למאגר כי היא אינה תמונה של התרופה, אנא בחר תמונה של התרופה ";
            }
            return "התרופה לא קיימת במאגר התרופות";
        }

        /// <summary>
        /// this function calls the imagga server to gets the tags of the image, and than it filter the tags, by a specific threshold
        /// </summary>
        /// <param name="path">the path of the image</param>
        /// <returns>list of the tags that belong to the image and suit the specific threshold</returns>
        public List<string> GetTags(string path)
        {
            List<string> Result = new List<string>();
            ImageDetails DrugImage = new ImageDetails(path);
            MedicineDAL dal = new MedicineDAL();
            dal.GetTags(DrugImage);
            //here is the filter of tags by the threshold
            var Threshold = 60.0;
            foreach (var item in DrugImage.Details)
            {
                if (item.Value > Threshold)
                {
                    Result.Add(item.Key);
                }
                else
                {
                    break;
                }
            }
            return Result;
        }

        /// <summary>
        /// this function checks the tags of a specific image, it checks if the tags suit well the image
        /// </summary>
        /// <param name="tags">the tags that this function checks</param>
        /// <returns>true if there is at least one tag in the tags that suits well to the image, if not-> return false</returns>
        public bool CheckTags(List<string> tags)
        {
            List<string> ourHealthTags = new List<string>(){"medicine","pills","pill","pharmacy",
                "medical","medication","drugs","drug","health","pill bottle","bottle","prescription drug","ball","clinic","applicator","health" ,"healthy","blister pack"};

            foreach (var item in tags)
            {
                foreach (var ourTags in ourHealthTags)
                {
                    if (item.Equals(ourTags))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// this function gets all the medicines from the db, with the images that belong to them
        /// </summary> 
        /// <returns>list with all the medicines from the db, with the images that belong to them</returns>
        public List<Medicine> GetMedicinesWithImage()
        {
            MedicineDAL dal = new MedicineDAL();
            return dal.GetMedicinesWithImage();
        }

        /// <summary>
        /// this function gets all the medicines from the db, without the images that belong to them
        /// </summary>
        /// <returns>list with all the medicines from the db, without the images that belong to them</returns>
        public List<Medicine> GetMedicines()
        {
            MedicineDAL dal = new MedicineDAL();
            return dal.GetMedicines();
        }


        /// <summary>
        /// this function downloads the images from the drive
        /// </summary>
        public void downloadImgs()
        {
            MedicineDAL dal = new MedicineDAL();
            dal.downloadImgs();
        }

        /// <summary>
        /// this function gets from the db the medicine that belong to a specific ndc 
        /// </summary>
        /// <param name="_NDC">the ndc that the function gets the medicine that belongs to it</param>
        /// <returns>an object of medicine that it is with the specific ndc</returns>
        public Medicine GetMedicineById(string _NDC)
        {
            MedicineDAL medicineDAL = new MedicineDAL();
            return medicineDAL.GetMedicineByID(_NDC);
        }


        /// <summary>
        /// this function deletes the image from the drive
        /// </summary>
        /// <param name="ndc">ths ndc of the medicine that its image will be delete by this function</param>
        /// <returns>string that describe if the function successed to delete the image</returns>
        public string DeleteImage(string ndc)
        {
            if (MedicineExistent(ndc) == true)
            {
                MedicineDAL dal = new MedicineDAL();
                return dal.DeleteImage(ndc);
            }
            return "התרופה לא קיימת במאגר התרופות";
        }

        /// <summary>
        /// this function checks if there is in the db a medicine with the specific ndc
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that this function check if it is being in the db</param>
        /// <returns>true if there is in the db a medicine with the specific ndc </returns>
        public bool MedicineExistent(string ndc)
        {
            var checkExistent = GetMedicineById(ndc);
            if (checkExistent != null)
            {
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// this function adds a medicine to the medecine db
        /// </summary>
        /// <param name="medicine">the medicine that this function will add to the db</param>
        /// <returns>string that tells if this function seccessed to add the medicine to the db</returns>
        public string AddMedicine(Medicine medicine)
        {
            if (MedicineExistent(medicine.PRODUCTNDC) == true)
            {
                return "תרופה זו כבר קיימת במאגר התרופות";
            }
            else
            {
                MedicineDAL dal = new MedicineDAL();
                return dal.AddMedicine(medicine);
            }
        }

        /// <summary>
        /// this function deletes from the db the medicine with the specific ndc and also delete the image that belong its
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that this function deletes</param>
        /// <returns>string that tell if this function seccessed to delete the medicine from the db</returns>
        public string DeleteMedicine(string ndc)
        {
            if (MedicineExistent(ndc) == true)
            {
                MedicineDAL medicineDAL = new MedicineDAL();
                return medicineDAL.DeleteMedicine(ndc);
            }
            else
                return "התרופה אינה קיימת במאגר התרופות ולכן לא ניתן למחוק אותה";
        }

        /// <summary>
        /// this function deletes a medicine for edit its, so it does`nt delete the image 
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that this function deletes</param>
        /// <returns>string that tells if this function seccessed to delete the medicine from the db</returns>
        public string DeleteMedicineforEdit(string ndc)
        {
            if (MedicineExistent(ndc) == true)
            {
                MedicineDAL medicineDAL = new MedicineDAL();
                return medicineDAL.DeleteMedicineforEdit(ndc);
            }
            else
                return "התרופה אינה קיימת במאגר התרופות ולכן לא ניתן למחוק אותה";
        }

        /// <summary>
        /// this function edits the properties of a specific medicine
        /// </summary>
        /// <param name="medicine">the medicine that this function edits its properties</param>
        /// <returns>string that tells if this function seccessed to edit the medicine </returns>
        public string EditMedicine(Medicine medicine)
        {
            var deleteAnswer = DeleteMedicineforEdit(medicine.PRODUCTNDC);

            if (deleteAnswer != "התרופה הוסרה בהצלחה ממאגר התרופות")
            {
                return "התרופה הנדרשת לעידכון אינה קיימת במאגר התרופות";
            }
            else {
                var addAnswer = AddMedicine(medicine);
                if (addAnswer != " התרופה התווספה בהצלחה למאגר התרופות")
                {
                    return "התרופה לא עודכנה בהצלחה";
                }
                else
                    return "התרופה עודכנה בהצלחה";
               }
        }

        /// <summary>
        /// this function edits the image of a specific medicine
        /// </summary>
        /// <param name="pRODUCTNDC">the ndc of the medicine that this function edits</param>
        /// <param name="file">the file of the image that the function will connect with the specific medicine</param>
        /// <returns>string that tells if this function seccessed to edit the image of the medicine</returns>
        public string EditImage(string pRODUCTNDC, HttpPostedFileBase file)
        {
            var deleteAnswer = DeleteImage(pRODUCTNDC);
           

            if (deleteAnswer != "התמונה של התרופה הוסרה בהצלחה")
            {
                return "התמונה לא עודכנה בהצלחה, כי היא אינה קיימת במאגר התרופות";
            }
            else
            {
                var addAnswer = AddImage(pRODUCTNDC, file);
                if (addAnswer != "התמונה התווספה למאגר בהצלחה")
                {
                    return "התמונה לא עודכנה בהצלחה, כי היא אינה תמונה של התרופה, אנא בחר תמונה של התרופה";
                }
                else
                    return "התמונה עודכנה בהצלחה";
            }
        }

        /// <summary>
        /// The function imports medicine details from the excel to the data base
        /// </summary>
        public void ImportDataFromExcel()
        {
            MedicineDAL medicineDAL = new MedicineDAL();
            medicineDAL.ImportDataFromExcel();
        }
    }

}




    

    




    

