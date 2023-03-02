using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YESS.BE;
using YESS.Models;

using YESS.BL;




namespace YESS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //returns home page
        public ActionResult Index()
        {
            return View();
        }
        // GET: About
       // return the view with information about the company
        public ActionResult about()
        {
            return View();
        }
        //returns the doctors page
        public ActionResult Doctor()
        {  
            return View();
        }
        //returns the pageEdit page- the options to edit
        public ActionResult pageEdit()
        {
            return View();
        }
        //returns the ForDoctor page- all the options for doctor
        public ActionResult ForDoctor()
        {
            return View();
        }
        //returns the Patient page
        public ActionResult Patient()
        {
            return View();
        }
        //returns the Medicine page
        public ActionResult Medicine()
        {
            return View();
        }
        //returns the Contact page
        public ActionResult Contact()
        {
            return View();
        }
        //return the view of sign in page
        public ActionResult Mypass()
        {
            MYUSER u = new MYUSER();
            return View("Mypass", u);
        }
        // POST: Doctor/Create
        //recieves the password and username and allow to in the web site according to given details
        [HttpPost]
        public ActionResult Mypass(FormCollection collection)
        {
            // the next code we need only for the first time - to make admin
            /*
            AdministratorBL admin = new AdministratorBL();
            admin.AdministratorAdd();
            */

            try
            {
                DoctorModel model = new DoctorModel();
                PatientModel model2 = new PatientModel();
                string patientGender = Request["patientGender"];
                var password = collection["Password"];
                var userName = collection["UserName"];
                MYUSER y = model2.AdminisAuthentication(userName, password);
                if (y != null)
                {
                    RouteConfig.isUser = true;
                    RouteConfig.WhichUser = 1;
                    RouteConfig.user = y;
                }
                else
                {
                    MYUSER x = model.DoctorAuthentication(userName, password);
                    if (x != null)
                    {
                        RouteConfig.isUser = true;
                        RouteConfig.WhichUser = 2;
                        RouteConfig.user = x;

                    }
                    else
                    {
                        ViewBag.pass1 = "המשתמש לא אומת";
                        ViewBag.pass2 = " נסה שנית";
                        return View();
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.nosuccess = "המטופל לא נכנס למאגר";
                return View();
            }
        }
        //sign out the user
        public ActionResult OUTMyUser()
        {
            RouteConfig.isUser = false;
            RouteConfig.WhichUser = 0;
            return RedirectToAction("Index", "Home");
        }
        //return the view of all the graghs
        public ActionResult midgam()
        {
            return View();
        }
    }
}