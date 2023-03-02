using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using YESS.BE;
using YESS.BL;

namespace YESS.Models
{
    /// <summary>
    /// MedicineModel is a class of the medicine
    /// </summary>
    public class MedicineModel
    {
        /// <summary>
        /// this function adds an image to a specific medicine, but anly if this image describes well the medicine
        /// </summary>
        /// <param name="_PRODUCTNDC">the ndc of the medicine that the function adds the image to its</param>
        /// <param name="file">the file of the image that will be in the drive</param>
        /// <returns>string that describe if the function successed to add the image or not</returns>
        public string AddImage(string _PRODUCTNDC, HttpPostedFileBase file)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
              Path.GetFileName(file.FileName));
            file.SaveAs(path);
            MedicineBL medicineBL = new MedicineBL();

            return medicineBL.AddImage(_PRODUCTNDC, file);
        }

        /*
        public List<string> GetTags(string imagePath)
        {
            ImageTagsLogic bl = new ImageTagsLogic();
            List<string> tags = bl.GetTags(imagePath);
            return tags;
        }
     */

        /// <summary>
        /// this function gets all the medicines from the db, with the images that belong to them
        /// </summary> 
        /// <returns>list with all the medicines from the db, with the images that belong to them</returns>
        public List<MedicineViewModel> GetMedicinesWithImage()
        {
            MedicineBL medicineBL = new MedicineBL();
            List<Medicine> medicines = medicineBL.GetMedicinesWithImage();
            List<MedicineViewModel> result = new List<MedicineViewModel>();
            MedicineViewModel dvm = null;
            foreach (var item in medicines)
            {
                dvm = new MedicineViewModel(item);
                result.Add(dvm);
            }
            return result;
        }

        /// <summary>
        /// this function gets all the medicines from the db, without the images that belong to them
        /// </summary>
        /// <returns>list with all the medicines from the db, without the images that belong to them</returns>
        public List<MedicineViewModel> GetMedicines()
        {
            MedicineBL medicineBL = new MedicineBL();
            List<Medicine> medicines = medicineBL.GetMedicines();
            List<MedicineViewModel> result = new List<MedicineViewModel>();
            MedicineViewModel dvm = null;
            foreach (var item in medicines)
            {
                dvm = new MedicineViewModel(item);
                result.Add(dvm);
            }
            return result;
        }


        /// <summary>
        /// this function downloads the images from the drive
        /// </summary>
        public void downloadImgs()
        {
            MedicineBL medicineBL = new MedicineBL();
            medicineBL.downloadImgs();
        }

        /// <summary>
        /// thif function gets all the NDC of the medicines that are in the db
        /// </summary>
        /// <returns>list of the all NDC of the medicines that are in the db</returns>
        public List<string> GetNDC()
        {
            List<MedicineViewModel> medicines = GetMedicines();
            List<string> result = new List<string>();
            string ndc;
            foreach (var item in medicines)
            {
                ndc = item.PRODUCTNDC;
                result.Add(ndc);
            }
            return result;
        }

        /// <summary>
        /// this function gets from the db the medicine that belong to a specific ndc 
        /// </summary>
        /// <param name="_NDC">the ndc that the function gets the medicine that belongs to it</param>
        /// <returns>an object of medicine that it is with the specific ndc</returns>
        public MedicineViewModel GetMedicineById(string _NDC)
        {
            MedicineBL medicineBL = new MedicineBL();
            var medicine = medicineBL.GetMedicineById(_NDC);
            MedicineViewModel result = new MedicineViewModel(medicine);
            return result;

        }


        /// <summary>
        /// this function deletes the image from the drive
        /// </summary>
        /// <param name="ndc">ths ndc of the medicine that its image will be delete by this function</param>
        /// <returns>string that describe if the function successed to delete the image</returns>
        public string DeleteImage(string ndc)
        {
            MedicineBL bl = new MedicineBL();
            return bl.DeleteImage(ndc);
        }

        /// <summary>
        /// this function checks if there is in the db a medicine with the specific ndc
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that this function check if it is being in the db</param>
        /// <returns>true if there is in the db a medicine with the specific ndc </returns>
        public bool MedicineExistent(string ndc)
        {
            MedicineBL medicineBL = new MedicineBL();
            return medicineBL.MedicineExistent(ndc);
        }



        //
        /// <summary>
        /// this function adds a medicine to the medecine db
        /// </summary>
        /// <param name="name">the PROPRIETARYNAME of the medicine </param>
        /// <param name="producer">the producer of the medicine</param>
        /// <param name="genericName">the genericName of the medicine</param>
        /// <param name="activeIngredients">the NONPROPRIETARYNAME of the medicine </param>
        /// <param name="dosageFormName">the DOSAGEFORMNAME of the medicine</param>
        /// <param name="active_numerator_strength">the ACTIVE_NUMERATOR_STRENGTH of the medicine</param>
        /// <param name="active_ingred_unit">the ACTIVE_INGRED_UNIT of the medicine</param>
        /// <param name="productNdc">the ndc of the medicine</param>
        /// <returns>string that tells if this function seccessed to add the medicine to the db</returns>
        public string AddMedicine(string name, string producer, string genericName, string activeIngredients, string dosageFormName, string active_numerator_strength,
           string active_ingred_unit, string productNdc)
        {

            Medicine medicine = new Medicine()
            {
                Name = name,
                Producer = producer,
                GenericName = genericName,
                ActiveIngredients = activeIngredients,
                DosageFormName = dosageFormName,
                ACTIVE_NUMERATOR_STRENGTH = active_numerator_strength,
                ACTIVE_INGRED_UNIT = active_ingred_unit,
                Image = "",
                PRODUCTNDC = productNdc
            };

            MedicineBL bl = new MedicineBL();
            return bl.AddMedicine(medicine);

        }


        /// <summary>
        /// this function deletes from the db the medicine with the specific ndc and also delete the image that belong its
        /// </summary>
        /// <param name="ndc">the ndc of the medicine that this function deletes</param>
        /// <returns>string that tell if this function seccessed to delete the medicine from the db</returns>
        public string DeleteMedicine(string ndc)
        {
            MedicineBL medicineBL = new MedicineBL();
            return medicineBL.DeleteMedicine(ndc);
        }

        /// <summary>
        /// this function edits the properties of a specific medicine
        /// </summary>
        /// <param name="name">the PROPRIETARYNAME of the medicine </param>
        /// <param name="producer">the producer of the medicine</param>
        /// <param name="genericName">the genericName of the medicine</param>
        /// <param name="activeIngredients">the NONPROPRIETARYNAME of the medicine </param>
        /// <param name="dosageFormName">the DOSAGEFORMNAME of the medicine</param>
        /// <param name="active_numerator_strength">the ACTIVE_NUMERATOR_STRENGTH of the medicine</param>
        /// <param name="active_ingred_unit">the ACTIVE_INGRED_UNIT of the medicine</param>
        /// <param name="productNdc">the ndc of the medicine</param>
        /// <param name="image"> the image of the medicine</param>
        /// <returns>string that tells if this function seccessed to edit the medicine</returns>
        public string EditMedicine(string name, string producer, string genericName, string activeIngredients, string dosageFormName, string active_numerator_strength,
        string active_ingred_unit, string productNdc, string image)
        {
            Medicine medicine = new Medicine()
            {
                Name = name,
                Producer = producer,
                GenericName = genericName,
                ActiveIngredients = activeIngredients,
                DosageFormName = dosageFormName,
                ACTIVE_NUMERATOR_STRENGTH = active_numerator_strength,
                ACTIVE_INGRED_UNIT = active_ingred_unit,
                Image = image,
                PRODUCTNDC = productNdc
            };

            MedicineBL bl = new MedicineBL();
            return bl.EditMedicine(medicine);
        }

        /// <summary>
        /// this function edits the image of a specific medicine
        /// </summary>
        /// <param name="_PRODUCTNDC">the ndc of the medicine that this function edits</param>
        /// <param name="file">the file of the image that the function will connect with the specific medicine</param>
        /// <returns>string that tells if this function seccessed to edit the image of the medicine</returns>
        public string EditImage(string _PRODUCTNDC, HttpPostedFileBase file)
        {
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
             Path.GetFileName(file.FileName));
            file.SaveAs(path);
            MedicineBL medicineBL = new MedicineBL();

            return medicineBL.EditImage(_PRODUCTNDC, file);

        }

        /// <summary>
        /// The function imports medicine details from the excel to the data base
        /// </summary>
        public void ImportDataFromExcel()
        {
            MedicineBL bl = new MedicineBL();
            bl.ImportDataFromExcel();
        }
    }
}
