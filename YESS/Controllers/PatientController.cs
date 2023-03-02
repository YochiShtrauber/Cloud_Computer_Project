using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using YESS.BE;
using YESS.Models;

namespace YESS.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient/Details/5
        //return the view of all patients details to the doctor
        public ActionResult DetailsForDoctor()
        {
            PatientModel model = new PatientModel();
            var patients = model.GetPatients();
            return View("DetailsForDoctor", patients);
        }
        // GET: Patient/Details/5
        //return the view of all patients details
        public ActionResult Details()
        {
            PatientModel model = new PatientModel();
            var patients = model.GetPatients();
            return View("Details", patients);
        }
        // GET: Patient/Create
        // return the view to create new patient
        public ActionResult Create()
        {
            RouteConfig.helper2 = 1;
            Patient patient = new Patient();

            return View(patient);
        }

        // POST: Patient/Create
        //recieves collection with all patien informaion
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                RouteConfig.helper2 = 0;
                PatientModel model = new PatientModel();
                string patientGender = Request["patientGender"];
                string patientPrescription = Request["patientPrescription"];
                var hmo = collection["Hmo"];
                var allergy = collection["Allergy"];
                var firstName = collection["FirstName"];
                var lasttName = collection["LasttName"];
                var address = collection["Address"];
                var phone = collection["Phone"];
                var mail = collection["Mail"];
                var birthDate = collection["BirthDate"];
                var _ID_TZ = collection["ID_TZ"];
                var englishName = collection["EnglishName"];
               /* if (hmo == "" || allergy == "" || firstName == "" || lasttName == "" || englishName == "" || address == "" || phone == "" || mail == "" || birthDate == "" || _ID_TZ == "")
                {
                    RouteConfig.helper2 = 1;
                    ViewBag.succeessOrNot = "חסר נתונים, נסה שנית";
                    return View();

                }*/
                    string answer = model.AddPatient(hmo, allergy, patientPrescription, firstName, lasttName, patientGender,
                address, phone, mail, Convert.ToDateTime(birthDate), _ID_TZ, englishName);
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.nosuccess = "המטופל לא נכנס למאגר";

                return View();
            }
        }
        //return view to recieve id to edit patient
        public ActionResult Edit1()
        {
            CreatePrescriptionViewModel createPrescriptionViewModel = new CreatePrescriptionViewModel();
            return View(createPrescriptionViewModel);
        }
        // POST: Doctor/Edit/5
        //recieves collection that contains id and send to edit patient
        [HttpPost]
        public ActionResult Edit1(FormCollection collection)
        {
            try
            {
                PatientModel model = new PatientModel();
                var _ID_TZ = collection["prescriptions.ID_TZ_Patient"];
                return Edit(_ID_TZ);
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Edit/5s
        //recieves id and return the view with details to edit patient
        public ActionResult Edit(string id)
        {
            RouteConfig.helper2 = 1;
            PatientModel model = new PatientModel();
            var patient = model.GetPatientById(id);
            return View("Edit", patient);

        }
        // POST: Patient/Edit/5
        //recieves collection with details and update the data base
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                RouteConfig.helper2 = 0;
                PatientModel model = new PatientModel();
                string patientGender = Request["patientGender"];
                var gender = collection["Gender"];
                string OURgender = patientGender;
                if (patientGender == "אם הינך רוצה לעדכן את סוג המין ☝🏻 שנה פה")
                {
                    OURgender = gender;
                }

                string prescription = Request["patientPrescription"];
                var prescriptionWay = collection["Prescription"];
                string OURType = prescription;
                if (prescription == "אם הינך רוצה לעדכן את אופן הרכישה ☝🏻 שנה פה")
                {
                    OURType = prescriptionWay;
                }

                var hmo = collection["Hmo"];
                var allergy = collection["Allergy"];
                var firstName = collection["FirstName"];
                var lasttName = collection["LasttName"];
                var address = collection["Address"];
                var phone = collection["Phone"];
                var mail = collection["Mail"];
                var birthDate = collection["BirthDate"];
                var _ID_TZ = collection["ID_TZ"];
                var englishName = collection["EnglishName"];
               /* if (hmo == "" || allergy == "" || firstName == "" || lasttName == "" || englishName == "" || address == "" || phone == "" || mail == "" || birthDate == "" || _ID_TZ == "")
                {
                    ViewBag.succeessOrNot = "חסר נתונים, נסה שנית";
                    return View();

                }*/
                string answer = model.EditPatient(hmo, allergy, OURType, firstName, lasttName, OURgender,
               address, phone, mail, Convert.ToDateTime(birthDate), _ID_TZ, englishName);
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.nosuccess = "המטופל לא עודכן";
                return View();
            }
        }

        //return view that asks for id to delete patient 
        public ActionResult Delete1()
        {
            CreatePrescriptionViewModel createPrescriptionViewModel = new CreatePrescriptionViewModel();
            return View(createPrescriptionViewModel);
        }


        // POST: Doctor/Edit/5
        //recieves collection that contains id and sent to function to delete patient 
        [HttpPost]
        public ActionResult Delete1(FormCollection collection)
        {
            try
            {
                PatientModel model = new PatientModel();
                var _ID_TZ = collection["prescriptions.ID_TZ_Patient"];
                return Delete(_ID_TZ);
            }
            catch
            {
                return View();
            }
        }


        // GET: Patient/Delete/5
        //recieves id and return the view with patient details before delete 
        public ActionResult Delete(string id)
        {
            RouteConfig.helper2 = 1;
            PatientModel model = new PatientModel();
            var patient = model.GetPatientById(id);
            return View("Delete", patient);
        }

        // POST: Patient/Delete/5
        //recieves collection that contains id and delete the patient from data base
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                RouteConfig.helper2 = 0;
                PatientModel model = new PatientModel();
                var _ID_TZ = collection["ID_TZ"];
                string answer = model.DeletePatient(_ID_TZ);
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditPatientHis(string id)
        {
            CreatePrescriptionViewModel cpvm = new CreatePrescriptionViewModel();
            return View(cpvm);
        }

        // POST: Patient/Delete/5
        //recieves collection that contains id and delete the patient from data base
        [HttpPost]
        public ActionResult EditPatientHis(FormCollection collection)
        {
            try
            {
                PatientModel model = new PatientModel();
                var _ID_TZ = collection["prescriptions.ID_TZ_Patient"];
                return EditPatientHis2(_ID_TZ);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult EditPatientHis2(string id)
        {
            RouteConfig.helper2 = 1;
            PatientModel model = new PatientModel();
            var patient = model.GetPatientById(id);
            return View("EditPatientHis2", patient);

        }
        // POST: Patient/Edit/5
        //recieves collection with details and update the data base
        [HttpPost]
        public ActionResult EditPatientHis2(FormCollection collection)
        {
            try
            {
                RouteConfig.helper2 = 0;
                PatientModel model = new PatientModel();
                string patientGender = Request["patientGender"];
                var gender = collection["Gender"];
                string OURgender = patientGender;
                if (patientGender == "אם הינך רוצה לעדכן את סוג המין ☝🏻 שנה פה")
                {
                    OURgender = gender;
                }

                string prescription = Request["patientPrescription"];
                var prescriptionWay = collection["Prescription"];
                string OURType = prescription;
                if (prescription == "אם הינך רוצה לעדכן את אופן הרכישה ☝🏻 שנה פה")
                {
                    OURType = prescriptionWay;
                }

                var hmo = collection["Hmo"];
                var allergy = collection["Allergy"];
                var firstName = collection["FirstName"];
                var lasttName = collection["LasttName"];
                var address = collection["Address"];
                var phone = collection["Phone"];
                var mail = collection["Mail"];
                var birthDate = collection["BirthDate"];
                var _ID_TZ = collection["ID_TZ"];
                var englishName = collection["EnglishName"];
                if (hmo == "" || allergy == "" || firstName == "" || lasttName == "" || englishName == "" || address == "" || phone == "" || mail == "" || birthDate == "" || _ID_TZ == "")
                {
                    ViewBag.succeessOrNot = "חסר נתונים, נסה שנית";
                    return View();

                }
                string answer = model.EditPatient(hmo, allergy, OURType, firstName, lasttName, OURgender,
               address, phone, mail, Convert.ToDateTime(birthDate), _ID_TZ, englishName);
                ViewBag.succeessOrNot = answer;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.nosuccess = "המטופל לא עודכן";
                return View();
            }
        }

    }
}