using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YESS.BE;
using YESS.Models;

namespace YESS.Controllers
{
    public class DoctorController : Controller
    {

        // GET: Doctor/Details/5
       //return the view of the list of all doctors
        public ActionResult Details()
        {
            DoctorModel model = new DoctorModel();
            var doctor = model.GetDoctors();
            return View("Details", doctor);
        }

        // GET: Doctor/Create
        //return the view of create new doctor
        public ActionResult Create()
        {
            Doctor doctor = new Doctor();
            RouteConfig.helper2 = 1;
            return View(doctor);
        }

        // POST: Doctor/Create
        //recieves the details of new doctor and add it to the data base 
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                RouteConfig.helper2 = 0;
                DoctorModel model = new DoctorModel();
                string doctorGender = Request["doctorGender"];
                string typeSpecial = Request["TypeSpecial"];
                var password = collection["Password"];
                var userName = collection["UserName"];
                var firstName = collection["FirstName"];
                var lasttName = collection["LasttName"];
                var address = collection["Address"];
                var phone = collection["Phone"];
                var mail = collection["Mail"];
                var birthDate = collection["BirthDate"];
                var _ID_TZ = collection["ID_TZ"];
                var _licenseNumber = collection["LicenseNumber"];
                var englishName = collection["EnglishName"];
                //checks if at least one of the fields is empty
               /* if ( password == "" || userName == "" || firstName == "" || lasttName == "" || englishName == "" || address == "" || phone == "" || mail == ""|| _ID_TZ == "" || _licenseNumber == ""|| birthDate=="")
                {
                    RouteConfig.helper2 = 1;
                    ViewBag.succeessOrNot1 = "חסר נתונים, נסה שנית";
                    return View();
                }*/
                List<string> answer= model.AddDoctor( password,  userName, _licenseNumber,  typeSpecial,  firstName,  lasttName, doctorGender,
                address,  phone,  mail,  Convert.ToDateTime(birthDate),  _ID_TZ, englishName);
                ViewBag.succeessOrNot1 = answer[0];
                ViewBag.succeessOrNot2 = answer[1];
                ViewBag.succeessOrNot3 = answer[2];
                ViewBag.succeessOrNot4 = answer[3];
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.succeessOrNot = "הרופא לא נכנס למאגר";
                return View();
            }
        }
        //return the view that allow to recieve id in order to edit doctor
        public ActionResult Edit1()
        {
            CreatePrescriptionViewModel createPrescriptionViewModel = new CreatePrescriptionViewModel();
            return View(createPrescriptionViewModel);
        }


        // POST: Doctor/Edit1/5
        //recieves collection that contains id and send to function to update the doctor  
        [HttpPost]
        public ActionResult Edit1(FormCollection collection)
        {
            try
            {
                DoctorModel model = new DoctorModel();
                var _ID_TZ = collection["prescriptions.ID_TZ_Doctor"];
                return Edit(_ID_TZ);

            }
            catch
            {
                return View();
            }
        }
        // GET: Doctor/Edit/5
        //recieves id and returns the view to edit the doctor
        [HttpGet]
        public ActionResult Edit(string id)
        {
            RouteConfig.helper2 =1;
            DoctorModel model = new DoctorModel();
            var doctor = model.GetDoctorById(id);
            return View("Edit",doctor);
        }
        //recieves the details after the update and update the data base  
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
           {
                RouteConfig.helper2 = 0;

                DoctorModel model = new DoctorModel();
                string patientGender = Request["patientGender"];
                var gender = collection["Gender"];
                string OURgender= patientGender;
                if(patientGender== "אם הינך רוצה לעדכן את סוג המין ☝🏻 שנה פה")
                {
                    OURgender = gender;
                }
                string typeSpecial = Request["typeS"];
                var type = collection["TypeSpecial"];
                string OURType = typeSpecial;
                if (typeSpecial == "אם הינך רוצה לעדכן את תחום ההתמחות ☝🏻 שנה פה")
                {
                    OURType = type;
                }
                var englishName = collection["EnglishName"];
                var password = collection["Password"];
                var userName = collection["UserName"];
                var firstName = collection["FirstName"];
                var lasttName = collection["LasttName"];
                var address = collection["Address"];
                var phone = collection["Phone"];
                var mail = collection["Mail"];
                var birthDate = collection["BirthDate"];
                var _ID_TZ = collection["ID_TZ"];
                var _licenceNumber = collection["LicenseNumber"];
              /*  if (password == "" || userName == "" || firstName == "" || lasttName == "" || englishName == "" || address == "" || phone == "" || mail == "" || _ID_TZ == "" || _licenceNumber == "" || birthDate == "")
                {
                    ViewBag.succeessOrNot = "חסר נתונים, נסה שנית";
                    return View();
                }*/
                List<string> answer = model.EditDoctor(password, userName, _licenceNumber, OURType, firstName, lasttName, OURgender,
                address, phone, mail, Convert.ToDateTime(birthDate), _ID_TZ, englishName);
                ViewBag.succeessOrNot = answer[0];
                ViewBag.succeessOrNot1 = answer[1];
                ViewBag.succeessOrNot2= answer[2];
                ViewBag.succeessOrNot3 = answer[3];
                return View();
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        //return the view in order to recieve the id of doctor to delete
        public ActionResult Delete1()
        {
            CreatePrescriptionViewModel createPrescriptionViewModel = new CreatePrescriptionViewModel();
            return View(createPrescriptionViewModel);
        }



        // POST: Doctor/Edit/5
        //recieves collection that contains id and send to function to delete the doctor  
        [HttpPost]
        public ActionResult Delete1(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                DoctorModel model = new DoctorModel();
                var _ID_TZ = collection["prescriptions.ID_TZ_Doctor"];
                return Delete(_ID_TZ);
            }
            catch
            {
                return View();
            }
        }
          //recieves id and return the view with the details of doctor to delete
          public ActionResult Delete(string id)
          {
            RouteConfig.helper2 = 1;
              DoctorModel model = new DoctorModel();
              var doctor = model.GetDoctorById(id);
              return View("Delete", doctor);
          }
        //recieved the doctors details and delete it from data base
          [HttpPost]
          public ActionResult Delete(FormCollection collection)
          {
              try
              {
                RouteConfig.helper2 = 0;
                DoctorModel model = new DoctorModel();
                var _ID_TZ = collection["ID_TZ"];
               string answer= model.DeleteDoctor(_ID_TZ);
                ViewBag.succeessOrNot = answer;
                return View();
            }
              catch (Exception ex)
              {
                  return View();
              }
          }
    }
}
