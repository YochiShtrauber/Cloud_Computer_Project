using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YESS.Models;

namespace YESS.Controllers
{
    public class ShowChartController : Controller
    {
        //return view that asks for ndc and year
        public ActionResult BeforeIndex()
        {
            ChartsViewModel x = new ChartsViewModel();
            return View(x);
        }
        // POST: Doctor/Edit/5
        //recieves collection that contains the year and ndc and send to function index
        [HttpPost]
        public ActionResult BeforeIndex(FormCollection collection)
        {
            try
            {
                DoctorModel model = new DoctorModel();
                var myYear = Convert.ToInt32(collection["MYyears"]);
                var ndc = collection["medicine.PRODUCTNDC"];
                return Index(ndc, myYear);
            }
            catch
            {
                return View();
            }
        }
        // GET: ShowChart
        //recieves ndc and year and show the Incidence of taking a medicine per year 
        public ActionResult Index(string ndc,int myYear)//שנה ותרופה ומציגה את השכיחות של לקיחת תרופה בשנה זאת לכלל הפציאנטים 
        {//רופא ואדמניסטרטור
            PrescriptionsModel prescriptionsModel = new PrescriptionsModel();
            var result = prescriptionsModel.ChartPrescriptionInOneYear(myYear, ndc);// לשנות פה כך שנקבל את השדה שהמשתמש הכניס
            return View("Index", result);

        }
        //recieves collection that contains the year and ndc
        public ActionResult BeforeIndex1()
        {
            ChartsViewModel x = new ChartsViewModel();
            return View(x);
        }


        // POST: Doctor/Edit/5
        //recieves collection that contains the year and ndc and send to function index1
        [HttpPost]
        public ActionResult BeforeIndex1(FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                DoctorModel model = new DoctorModel();
                var myYear = Convert.ToInt32(collection["MYyears"]);
                var ndc = collection["medicine.PRODUCTNDC"];
                return Index1(ndc, myYear);
            }
            catch
            {
                return View();
            }
        }
        //recieves ndc and year and show the prevalence of prescribing a drug per year 
        public ActionResult Index1(string ndc,int myYear)
        {
            PrescriptionsModel prescriptionsModel = new PrescriptionsModel();
            var result = prescriptionsModel.ChartStartPrescriptionInOneYear(myYear, ndc);// לשנות פה כך שנקבל את השדה שהמשתמש הכניס
            return View("Index1", result);

        }
        //recieves collection that contains the year and ndc
        public ActionResult BeforeIndex2()
        {
            ChartsViewModel x = new ChartsViewModel();
            return View(x);
        }


        // POST: Doctor/Edit/5
        //recieves collection that contains the year and ndc and send to function index2
        [HttpPost]
        public ActionResult BeforeIndex2(FormCollection collection)
        {
            try
            {
                DoctorModel model = new DoctorModel();
                var myYear = Convert.ToInt32(collection["MYyears"]);
                var ndc = collection["medicine.PRODUCTNDC"];
                return Index2(ndc, myYear);

            }
            catch
            {
                return View();
            }
        }
        //recieves ndc and year and show the incidence of taking a medicine in the selected year for all patients according to gender
        public ActionResult Index2(string ndc,int myYear)
        {
            PrescriptionsModel prescriptionsModel = new PrescriptionsModel();
            PrescriptionTwoListviewModel prescriptionTwoListviewModel = new PrescriptionTwoListviewModel(myYear, ndc);
            prescriptionTwoListviewModel.resultMale = prescriptionsModel.ChartPrescriptionInOneYear(myYear, ndc, "זכר");
            prescriptionTwoListviewModel.resultFemale = prescriptionsModel.ChartPrescriptionInOneYear(myYear, ndc, "נקבה");
            return View("Index2", prescriptionTwoListviewModel);
        }
        //for pie chart
        //return the view that shows the Distribution of patients by age
        public ActionResult Index3()
        {

            PatientModel patientModel = new PatientModel();
            var pieList = patientModel.AgeOfThePatients();
            return View("Index3", pieList);

        }

        //return the view that shows the distribution of doctors by field of specialization
        public ActionResult Index4()
        {

            DoctorModel doctorModel = new DoctorModel();
            var pieList = doctorModel.FieldDisciplineOfTheDoctor();
            return View("Index4", pieList);

        }


    }
}
