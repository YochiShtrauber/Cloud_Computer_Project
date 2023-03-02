using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YESS.BE;
using YESS.Models;

namespace YESS.Controllers
{
    public class MedicineController : Controller
    {
        // GET: Medicine
        public ActionResult Index()
        {
            return View();
        }
        // GET: Medicine/Details/5
        //return the view of list of all medicines 
        public ActionResult DetailsForDocto()
        {
            MedicineModel model = new MedicineModel();
          var medicines = model.GetMedicinesWithImage();
            return View(medicines);
        }
        // GET: Medicine/Details/5
        //return the view of list of all medicines 
        public ActionResult Details()
        {
            MedicineModel model = new  MedicineModel();
            var medicines = model.GetMedicinesWithImage();
            return View(medicines);
        }
        #region C E D for image  

        // GET: Medicine/Create
        //return the view in order to recieve the ndc
        public ActionResult Create()
        {
            CreateMedicineViewModel createMedicineViewModel = new CreateMedicineViewModel(); 
            return View(createMedicineViewModel);
        }

        // POST: Medicine/Create
        //recieves collection that contains the ndc
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                MedicineModel model = new MedicineModel();
                var _PRODUCTNDC = collection["medicine.PRODUCTNDC"];
                return Create2(_PRODUCTNDC);
                //not suit the drug. if it true so it is ok and than the picture will add to the medicine in the db.
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        //recieves ndc and return the view to add image to the medicine
        public ActionResult Create2(string ndc)
        {
            RouteConfig.helper = ndc;
            RouteConfig.helper2 = 1;
            return View("Create2");
        }
        //recieves the file and upload it to google drive and saves the name of it in data base 
        [HttpPost]
        public ActionResult Create2(HttpPostedFileBase file)
        {
            RouteConfig.helper2 = 0;
            MedicineModel model = new MedicineModel();
            string result = model.AddImage(RouteConfig.helper, file);
            ViewBag.succeessOrNot = result;
            RouteConfig.helper = "";
            return View();
        }
        //return the view to recieve ndc
        public ActionResult Edit1()
        {
            CreateMedicineViewModel createMedicineViewModel = new CreateMedicineViewModel();
            return View(createMedicineViewModel);
        }
        // POST: Doctor/Edit1/5
        //recieves collection that contain ndc and send it to edit function
        [HttpPost]
        public ActionResult Edit1(FormCollection collection)
        {
            try
            {
                MedicineModel model = new MedicineModel();
                var NDC = collection["medicine.PRODUCTNDC"];
                 return Edit(NDC);
            }
            catch
            {
                return View();
            }
        }
      

        // GET: Medicine/Edit/5
        //recieves ndc and return the view in order to update the image
        public ActionResult Edit(string ndc)
        {
            MedicineModel model = new MedicineModel();
            var medicin = model.GetMedicineById(ndc);
            RouteConfig.helper = ndc;
            RouteConfig.helper2 = 1;
          if (medicin.Image == "")
            {
              return Create2(ndc);
            }
            return View("Edit", medicin);
        }
        

        // POST: Medicine/Edit/5
        //recieves the image and update the medicine image 
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file)
        {
            try
            {
                RouteConfig.helper2 = 0;
               
                MedicineModel model = new MedicineModel();
                string answer = model.EditImage(RouteConfig.helper, file);
                RouteConfig.helper = "";
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        //return help view to show error
        public ActionResult NotExist2()
        {
            return View("NotExist2");
        }

        // get: medicine/Delete1/5
        //return view to recieve ndc to delete image
        [HttpGet]
        public ActionResult Delete1()
        {
            CreateMedicineViewModel createMedicineViewModel = new CreateMedicineViewModel();
            return View(createMedicineViewModel);

        }
        // POST: medicine/Delete1/5
        //recieves collection that contains ndc and sends to delete impage
        [HttpPost]
        public ActionResult Delete1(FormCollection collection)
        {
            try
            {
                MedicineModel model = new MedicineModel();
                var ndc = collection["medicine.PRODUCTNDC"];
                return Delete(ndc);
            }
            catch
            {
                return View();
            }
        }


        // GET: Medicine/Delete/5
        //recieves ndc and reurn the view to delete image - ask for being sure to delete
        public ActionResult Delete(string ndc)
        {
            RouteConfig.helper2 = 1;
            MedicineModel model = new MedicineModel();
            var medicine = model.GetMedicineById(ndc);
            if (medicine.Image =="")
            {
               return NotExist2();
            }
            return View("Delete", medicine);
        }

        // POST: Medicine/Delete/5
        //recieves collection that contains ndc in order to delete image- delete it from drive and data base
        [HttpPost]
        public ActionResult Delete( FormCollection collection)
        {
            try
            {
                MedicineModel model = new MedicineModel();
                RouteConfig.helper2 = 0;
                var ndc = collection["PRODUCTNDC"];
                string answer = model.DeleteImage(ndc);
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        #endregion

        #region full medicine


        // GET: Doctor/Create
        //return view to create medicine
        public ActionResult CreateMedicine()
        {
            Medicine m = new Medicine();
            RouteConfig.helper2 = 1;
            return View(m);
        }

        // POST: Doctor/Create
        //after submit create the medicine
        [HttpPost]
        public ActionResult CreateMedicine(FormCollection collection)
        {
            try
            {
                RouteConfig.helper2 = 0;
                MedicineModel model = new MedicineModel();
                var name = collection["Name"];
                var producer = collection["Producer"];
                var genericName = collection["GenericName"];
                var activeIngredients = collection["ActiveIngredients"];
                var dosageFormName = collection["DosageFormName"];
                var activeNumeratorStrength = collection["ACTIVE_NUMERATOR_STRENGTH"];
                var activeIngredUnit = collection["ACTIVE_INGRED_UNIT"];
                var ndc = collection["PRODUCTNDC"];
             /*   if (name==""|| producer == "" || genericName == "" || activeIngredients == "" || dosageFormName == "" || activeNumeratorStrength == "" || activeIngredUnit == "" || ndc == "" )
                {
                    RouteConfig.helper2 = 1;
                    ViewBag.succeessOrNot = "חסר נתונים, נסה שנית";
                    return View();
                }*/
                string answer = model.AddMedicine(name, producer, genericName, activeIngredients, dosageFormName,
                  activeNumeratorStrength, activeIngredUnit, ndc);
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.succeessOrNot = "המטופל לא נכנס למאגר";
                return View();
            }
        }
        //return the view to ask for ndc for medicine to delete
        public ActionResult Delete1Medicine()
        {
            CreateMedicineViewModel createMedicineViewModel = new CreateMedicineViewModel();
            return View(createMedicineViewModel);

        }

        // POST: Medicine/Delete/5
        //recives ndc and send to delete the medicine
        [HttpPost]
        public ActionResult Delete1Medicine(FormCollection collection)
        {
            try
            {
                MedicineModel model = new MedicineModel();
                var ndc = collection["medicine.PRODUCTNDC"];
                return DeleteMedicine(ndc);

            }
            catch
            {
                return View();
            }
        }
        //recieves ndc and return the view with medicine details
        public ActionResult DeleteMedicine(string ndc)
        {
            RouteConfig.helper2 = 1;
            MedicineModel model = new MedicineModel();
            var m = model.GetMedicineById(ndc);
            return View("DeleteMedicine", m);
        }
        //recieves collection that contains ndc and delete medicine
        [HttpPost]
        public ActionResult DeleteMedicine(FormCollection collection)
        {
            try
            {
                RouteConfig.helper2 =0;
                MedicineModel model = new MedicineModel();
                var ndc = collection["PRODUCTNDC"];
                string answer = model.DeleteMedicine(ndc);
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        //return the view to ask for ndc for medicine to edit
        public ActionResult Edit1Medicine()
        {
            CreateMedicineViewModel createMedicineViewModel = new CreateMedicineViewModel();
            return View(createMedicineViewModel);

        }
        // POST: Medicine/Edit/5
        //recives ndc and send to edit the medicine
        [HttpPost]
        public ActionResult Edit1Medicine(FormCollection collection)
        {
            try
            {
                MedicineModel model = new MedicineModel();
                var ndc = collection["medicine.PRODUCTNDC"];
                return EditMedicine(ndc);
            }
            catch
            {
                return View();
            }
        }
        //recieves ndc and return the view with medicine details to update
        public ActionResult EditMedicine(string ndc)
        {
            RouteConfig.helper2 = 1;
            MedicineModel model = new MedicineModel();
            var m = model.GetMedicineById(ndc);
            return View("EditMedicine", m);
        }
        //recives details and update the medicine
        [HttpPost]
        public ActionResult EditMedicine(FormCollection collection)
        {
            try
            {
                RouteConfig.helper2 = 0;

                MedicineModel model = new MedicineModel();
                var name = collection["Name"];
                var producer = collection["Producer"];
                var genericName = collection["GenericName"];
                var activeIngredients = collection["ActiveIngredients"];
                var dosageFormName = collection["DosageFormName"];
                var activeNumeratorStrength = collection["ACTIVE_NUMERATOR_STRENGTH"];
                var activeIngredUnit = collection["ACTIVE_INGRED_UNIT"];
                var ndc = collection["PRODUCTNDC"];
                var img = collection["Image"];
               /* if (name == "" || producer == "" || genericName == "" || activeIngredients == "" || dosageFormName == "" || activeNumeratorStrength == "" || activeIngredUnit == "" || ndc == "")
                {
                    ViewBag.succeessOrNot = "חסר נתונים, נסה שנית";
                    return View();
                }*/
                string answer = model.EditMedicine(name, producer, genericName, activeIngredients, dosageFormName,
                  activeNumeratorStrength, activeIngredUnit, ndc, img);
                if(answer==null)
                {
                    answer = "התרופה לא עודכנה, אנא נסה שוב";
                }
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        #endregion
    }
}
