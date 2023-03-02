using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YESS.BE;

using YESS.Models;
using YESS.Models.ViewModel;

namespace YESS.Controllers
{
    public class PrescriptionsController : Controller
    {
        //return the view that ask for id in order to show its prescriptions
        public ActionResult DetailsAskForID()
        {
            CreatePrescriptionViewModel p = new CreatePrescriptionViewModel();
            return View("DetailsAskForID", p);
        }
        //recieves collection that contains id and send to function details
        [HttpPost]
        public ActionResult DetailsAskForID(FormCollection collection)
        {
            var _ID_TZ_Patient = collection["prescriptions.ID_TZ_Patient"];

            return Details(_ID_TZ_Patient);
        }

        // GET: Prescriptions/Details/5
        //recieves id and return the view with all patient prescriptions
        public ActionResult Details(string id)
        {
            PrescriptionsModel model = new PrescriptionsModel();
            PatientModel model2 = new PatientModel();
            var p = model2.GetPatientById(id);
            RouteConfig.pvm = p;
            var prescriptions = model.GetPrescriptionsByIdPatient(id);
            return View("Details", prescriptions);
        }
        // GET: Prescriptions/Create
        //return view that asks for id and ndc
        public ActionResult Create1()
        {

            CreatePrescriptionViewModel createPrescriptionViewModel = new CreatePrescriptionViewModel();
            return View("Create1", createPrescriptionViewModel);
        }

        // POST: Prescriptions/Create
        //recieves collection that contains id of patient and ndc and sent to function create 
        //checks the allow for prescription
        [HttpPost]
        public ActionResult Create1(FormCollection collection)
        {
            try
            {
                PrescriptionsModel model = new PrescriptionsModel();
                var _ID_TZ_Patient = collection["prescriptions.ID_TZ_Patient"];
                var _NDC = collection["medicine.PRODUCTNDC"];
                var start = Convert.ToDateTime( collection["Start"]);
                var last = Convert.ToDateTime( collection["Last"]);
              
                
                string x = model.getPrescriptionsAllow(_NDC, _ID_TZ_Patient,start, last);
                return Create(_ID_TZ_Patient, _NDC, x,start,last);

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Prescriptions/Create
        //recieves id of patient and ndc it shows the string recieved
        //return the view to create prescription
        public ActionResult Create(string _ID_TZ_Patient, string NDC, string x, DateTime _startdate, DateTime _lastdate)
        {
            CreatePrescriptionViewModel createPrescriptionViewModel = new CreatePrescriptionViewModel();
            createPrescriptionViewModel.id = _ID_TZ_Patient;
            createPrescriptionViewModel.lastdate = _lastdate;
            createPrescriptionViewModel.startdate = _startdate;
            
            createPrescriptionViewModel.medicine.PRODUCTNDC = NDC;
            ViewBag.prescriptionsAllow1 = " תוצאות בדיקת ההתנגשויות בין המרשמים למטופל, ת.ז";
            ViewBag.prescriptionsAllow2 = _ID_TZ_Patient;
            ViewBag.prescriptionsAllow = x;
            RouteConfig.helper2 = 1;
            return View("Create", createPrescriptionViewModel);
        }

        // POST: Prescriptions/Create
        //recieve collection with all details and create prescription
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                PrescriptionsModel model = new PrescriptionsModel();
                DoctorModel model2 = new DoctorModel();
                var _ID_TZ_Doctor = RouteConfig.user.ID_TZ;
                var _ID_TZ_Patient = collection["id"];
                var _NDC = collection["medicine.PRODUCTNDC"];
                var start = collection["startdate"];
                var last = collection["lastdate"];
                var timesToTake = collection["TimesToTake"];
                var ammount = collection["Ammount"];
                RouteConfig.helper2 = 0;
                //בדיקות
              /*  if (_ID_TZ_Patient == "" || _NDC == "" || start == "" || last == "" || timesToTake == "" || ammount == "")
                {
                    RouteConfig.helper2 = 0;
                    ViewBag.succeessOrNot = "חסר נתונים, נסה שנית";
                    return View();

                }*/
               
                if (model2.DoctorExistent(_ID_TZ_Doctor))//_ID_TZ_Doctor פונקצי שבודקת קיימות של רופא לפי תז 

                {
                    List<string> answer = model.AddPrescriptions(_ID_TZ_Doctor, _ID_TZ_Patient, _NDC, Convert.ToDateTime(start), Convert.ToDateTime(last), timesToTake,
                                     ammount);
                    if (answer[3]== "המרשם נקלט בהצלחה במאגר המרשמים")
                    {
                        RouteConfig.pre = new Prescriptions();
                        RouteConfig.pre.Start =Convert.ToDateTime(start);
                        RouteConfig.pre.Last = Convert.ToDateTime(last);
                        RouteConfig.pre.TimesToTake = timesToTake;
                        RouteConfig.pre.Ammount = ammount;
                        RouteConfig.pre.NDC = _NDC;
                        RouteConfig.pre.ID_TZ_Doctor = _ID_TZ_Doctor ;
                        RouteConfig.pre.ID_TZ_Patient = _ID_TZ_Patient;


                    }
                    ViewBag.succeessOrNot1 = answer[0];
                    ViewBag.succeessOrNot2 = answer[1];
                    ViewBag.succeessOrNot3 = answer[2];
                    ViewBag.succeessOrNot4 = answer[3];
                    return View();
                }
                else
                {
                    Prescriptions p = new Prescriptions()
                    {
                        ID_TZ_Doctor = _ID_TZ_Doctor,
                        ID_TZ_Patient = _ID_TZ_Patient,
                        NDC = _NDC,
                        Start = Convert.ToDateTime(start),
                        Last = Convert.ToDateTime(last),
                        TimesToTake = timesToTake,
                        Ammount = ammount
                    };
                    return NotExist(p);

                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        //return the view to show errors
        public ActionResult NotExist(Prescriptions p)
        {
            return View("NotExist", p);
        }
        //return the home page view
        [HttpPost]
        public ActionResult NotExist()
        {
            return RedirectToAction("Index", "Home");
        }
        //return the view to show errors
        public ActionResult NotExist2()
        {
            return View("NotExist2");
        }
        
       //return view to ask for ndc
        public ActionResult DetailsAskForNCD()
        {
            CreatePrescriptionViewModel p = new CreatePrescriptionViewModel();
            return View("DetailsAskForNCD", p);
        }
        [HttpPost]
        //recieves collection that contains ndc and send to function DetailsNCD
        public ActionResult DetailsAskForNCD(FormCollection collection)
        {
            var _ndc = collection["prescriptions.NDC"];
            return DetailsNCD(_ndc);
        }

        // GET: Prescriptions/Details/5
        //recieves ndc and return the view with all prescriptions with this ndc
        public ActionResult DetailsNCD(string ndc)
        {
            PrescriptionsModel model = new PrescriptionsModel();
            List<Prescriptions> p = model.GetPrescriptionsByNdc(ndc);
            List<PrescriptionsViewModel> p2 = new List<PrescriptionsViewModel>();
            foreach (var item in p)
            {
                p2.Add(new PrescriptionsViewModel(item));
            }

            return View("DetailsNCD", p2);
        }
       



        public void CreateDocument()
        {
           

            DoctorModel md = new DoctorModel();
            var doc = md.GetDoctorById(RouteConfig.pre.ID_TZ_Doctor);
            PatientModel mp = new PatientModel();
            var pati = mp.GetPatientById(RouteConfig.pre.ID_TZ_Patient);
            MedicineModel m = new MedicineModel();
            var medicin = m.GetMedicineById(RouteConfig.pre.NDC);



            //Create an instance of PdfDocument.
            using (PdfDocument document = new PdfDocument())
            {
                //Add a page to the document
                PdfPage page = document.Pages.Add();

                //Create PDF graphics for the page
                PdfGraphics graphics = page.Graphics;
                string d = Convert.ToString(DateTime.Now);
                //Set the standard font
                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 10);
                PdfFont font1 = new PdfStandardFont(PdfFontFamily.Helvetica, 8);
                PdfFont font2 = new PdfStandardFont(PdfFontFamily.Helvetica, 11);
                PdfFont font3 = new PdfStandardFont(PdfFontFamily.Helvetica, 15);

                //Load the image from the disk.
                PdfBitmap image = new PdfBitmap(Server.MapPath("~/img/logo3.png"));
                //Draw the image
                graphics.DrawImage(image, 0, 0);
                // Open the document in browser after saving it
                //Close the document.
                //Draw the text
                graphics.DrawString("Address: Aluf-David 187 Ramat-Gan, Israel", font, PdfBrushes.Black, new PointF(5, 45));
                graphics.DrawString("Tel: +972 111111111", font, PdfBrushes.Black, new PointF(5, 60));
                graphics.DrawString("Mail: ysseproject @gmail.com", font, PdfBrushes.Black, new PointF(5, 75));
                graphics.DrawString(d, font, PdfBrushes.Black, new PointF(420, 0));


                graphics.DrawString("Patient's details:", font2, PdfBrushes.Black, new PointF(250, 105));
                graphics.DrawString("Patient's name:", font2, PdfBrushes.Black, new PointF(250, 120));
                graphics.DrawString(pati.EnglishName, font2, PdfBrushes.Black, new PointF(400, 120));
                graphics.DrawString("Patient's ID:", font2, PdfBrushes.Black, new PointF(250, 135));
                graphics.DrawString(pati.ID_TZ, font2, PdfBrushes.Black, new PointF(400, 135));
                graphics.DrawString("Patient's phone number:", font2, PdfBrushes.Black, new PointF(250, 150));
                graphics.DrawString(pati.Phone, font2, PdfBrushes.Black, new PointF(400, 150));

                graphics.DrawString("------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------", font2, PdfBrushes.Black, new PointF(0, 175));

                graphics.DrawString("Prescription for the drug:", font2, PdfBrushes.Black, new PointF(0, 200));
                graphics.DrawString("Name:", font2, PdfBrushes.Black, new PointF(0, 215));
                graphics.DrawString(medicin.Name, font2, PdfBrushes.Black, new PointF(250, 215));
                graphics.DrawString("NDC:", font2, PdfBrushes.Black, new PointF(0, 230));
                graphics.DrawString(medicin.PRODUCTNDC, font2, PdfBrushes.Black, new PointF(250, 230));
                graphics.DrawString("Producer:", font2, PdfBrushes.Black, new PointF(0, 245));
                graphics.DrawString(medicin.Producer, font2, PdfBrushes.Black, new PointF(250, 245));
                graphics.DrawString("Start date of taking the drug:", font2, PdfBrushes.Black, new PointF(0, 260));
                graphics.DrawString(Convert.ToString(RouteConfig.pre.Start), font2, PdfBrushes.Black, new PointF(250, 260));
                graphics.DrawString("End date of taking the drug:", font2, PdfBrushes.Black, new PointF(0, 275));
                graphics.DrawString(Convert.ToString(RouteConfig.pre.Last), font2, PdfBrushes.Black, new PointF(250, 275));
                graphics.DrawString("The number of times to take the drug:", font2, PdfBrushes.Black, new PointF(0, 290));
                graphics.DrawString(RouteConfig.pre.TimesToTake, font2, PdfBrushes.Black, new PointF(250, 290));
                graphics.DrawString("The amount to take the drug:", font2, PdfBrushes.Black, new PointF(0, 305));
                graphics.DrawString(RouteConfig.pre.Ammount, font2, PdfBrushes.Black, new PointF(250, 305));
                graphics.DrawString("The prescription is valid for 100 days", font, PdfBrushes.Blue, new PointF(120, 330));
                string daynow = Convert.ToString(DateTime.Now);
                string day100 = Convert.ToString(DateTime.Now.AddDays(100));

                graphics.DrawString(daynow + "-" + day100, font, PdfBrushes.Blue, new PointF(120, 345));


                graphics.DrawString("Doctor's name:", font3, PdfBrushes.Black, new PointF(210, 405));
                graphics.DrawString(doc.EnglishName, font3, PdfBrushes.Black, new PointF(400, 405));
                graphics.DrawString("Doctor's license number:", font3, PdfBrushes.Black, new PointF(210, 425));
                graphics.DrawString(doc.LicenseNumber, font3, PdfBrushes.Black, new PointF(400, 425));
                graphics.DrawString("____________________________", font3, PdfBrushes.Black, new PointF(230, 495));
                graphics.DrawString("Signature and Stamp", font3, PdfBrushes.Black, new PointF(262, 515));
                string helpr = "Please notice, this prescription is available in your membership card.";
                string helpr1 = "You able to watch it at any time at our website YESS.";
                string helpr2 = "In case of swelling, redness, rash or other signs of sensitivity or allergy,";
                string helpr3 = "stop taking that medicine and see a doctor as soon as possible.";
                string helpr4 = "In urgent cases you may call us, our phone number is +972 111111111";
                string helpr5 = "If you have covid19 symptoms as: high fever, cough, sneezing, loss of smell and taste.";
                string helpr6 = "Stay home and call to 101.";
                string helpr7 = "On your visit at the pharmacy disinfect your hands and ware face mask.";
                graphics.DrawString(helpr, font1, PdfBrushes.Blue, new PointF(0, 645));
                graphics.DrawString(helpr1, font1, PdfBrushes.Blue, new PointF(0, 660));
                graphics.DrawString(helpr2, font1, PdfBrushes.Blue, new PointF(0, 675));
                graphics.DrawString(helpr3, font1, PdfBrushes.Blue, new PointF(0, 690));
                graphics.DrawString(helpr4, font1, PdfBrushes.Blue, new PointF(0, 705));
                graphics.DrawString(helpr5, font1, PdfBrushes.Blue, new PointF(0, 720));
                graphics.DrawString(helpr6, font1, PdfBrushes.Blue, new PointF(0, 735));
                graphics.DrawString(helpr7, font1, PdfBrushes.Blue, new PointF(0, 750));

                var h = pati.ID_TZ + "_Prescription.pdf";
                // Open the document in browser after saving it
                document.Save(h, HttpContext.ApplicationInstance.Response, HttpReadType.Save);
            }
            // return View();
        }
    }
}
