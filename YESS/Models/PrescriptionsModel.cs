using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YESS.BE;
using YESS.BL;
using YESS.Models.ViewModel;

namespace YESS.Models
{
    public class PrescriptionsModel
    {
        /// <summary>
        /// The function gets all the  prescription 
        /// </summary>
        /// <returns> a list of all the prescriptions View model</returns>
        public List<PrescriptionsViewModel> GetPrescriptions()
        {
            PrescriptionsBL prescriptionsBL = new PrescriptionsBL();
            List<Prescriptions> prescriptions = prescriptionsBL.GetPrescriptions();
            List<PrescriptionsViewModel> result = new List<PrescriptionsViewModel>();
            PrescriptionsViewModel dvm = null;
            foreach (var item in prescriptions)
            {
                dvm = new PrescriptionsViewModel(item);
                result.Add(dvm);
            }
            return result;
        }
    
    /// <summary>
    /// The function gets all the prescription that belong to the id patient
    /// </summary>
    /// <param name="id_tz">id of patient</param>
    /// <returns> a list of the prescription that belong to the id patient</returns>
    public List<PrescriptionsViewModel> GetPrescriptionsByIdPatient(string id_tz) 
        {
            PrescriptionsBL prescriptionsBL = new PrescriptionsBL();
            var prescriptions = prescriptionsBL.GetPrescriptionsByIdPatient(id_tz);
            List<PrescriptionsViewModel> result = new List<PrescriptionsViewModel>();
            foreach(var item in prescriptions)
            {
                PrescriptionsViewModel prescription = new PrescriptionsViewModel(item);
                result.Add(prescription);
            }
            
            return result;

        }
    /// <summary>
    /// the function add prescription
    /// </summary>
    /// <param name="_ID_TZ_Doctor">id of doctor</param>
    /// <param name="_ID_TZ_Patient">id of patient</param>
    /// <param name="_NDC">ndc of medicine</param>
    /// <param name="start">start date</param>
    /// <param name="last">end date</param>
    /// <param name="timesToTake">num of times to take the medicine</param>
    /// <param name="ammount">the amount of medicine</param>
    /// <returns></returns>
    public List<string> AddPrescriptions(string _ID_TZ_Doctor, string _ID_TZ_Patient, string _NDC,
                  DateTime start, DateTime last, string timesToTake, string ammount)
        {

            Prescriptions prescriptions = new Prescriptions()
            {
                ID_TZ_Doctor = _ID_TZ_Doctor,
                ID_TZ_Patient = _ID_TZ_Patient,
                NDC = _NDC,
                Start = start,
                Last = last,
                TimesToTake = timesToTake,
                Ammount = ammount,
            };


            PrescriptionsBL bl = new PrescriptionsBL();
            return bl.AddPrescriptions(prescriptions);
        }
        /// <summary>
        /// the function delete prescription by tz of patient
        /// </summary>
        /// <param name="id_tz_Patient">tz of patient</param>
        /// <param name="_ndc">ndc of doctor</param>
        /// <returns> string that describe if the function deletes all the prescriptions of speceific ndc and that belong to the id patient </returns>
        public string DeletePrescriptionsByIdPatient(string id_tz_Patient, string _ndc)
        {
            PrescriptionsBL bl = new PrescriptionsBL();
            return bl.DeletePrescriptionsByIdPatient(id_tz_Patient, _ndc);
        }

        /// <summary>
        /// delete all prescription of specific patient
        /// </summary>
        /// <param name="id_tz_Patient">id of patient</param>
        /// <returns>a string that describes if the function deletes all the prescription that belong to the id patient </returns>
        public string DeleteALLPrescOfSpechificPatient(string id_tz_Patient)
        {
            PrescriptionsBL bl = new PrescriptionsBL();
            return bl.DeleteALLPrescOfSpechificPatient(id_tz_Patient);
        }
        /// <summary>
        /// the function get the clash between prescription
        /// </summary>
        /// <param name="myNDC">ndc of medicine</param>
        /// <param name="id_TZ_Patient">id of patient</param>
        /// <param name="start">the start date</param>
        /// <param name="last">the last date</param>
        /// <returns>string of clash prescription</returns>
        public string getPrescriptionsAllow(string myNDC, string id_TZ_Patient,DateTime start,DateTime last) //פונקציה שמחזירה את התיאור של ההתנגשויות שיש להציג על המסך
        {
            {
                PrescriptionsBL bl = new PrescriptionsBL();
                List<string> x = bl.getPrescriptionsAllow(myNDC, id_TZ_Patient,start,last);

                string r=""; 
                foreach (var item in x)
                {
                    r += " " + item + " " + "\n" + " ";
                }
                return r;
            }
        }
        /// <summary>
        /// The function gets year and the Ndc and get the frequency of the prescription in every month in this year
        /// </summary>
        /// <param name="year">year</param>
        /// <param name="ndc">ndc of medicine</param>
        /// <param name="gender">gender</param>
        /// <returns>the frequency of the prescription in every month in this year</returns>

        public List<int> ChartPrescriptionInOneYear(int year, string ndc, string gender = "everyThing")
        {
            PrescriptionsBL prescriptionsBL = new PrescriptionsBL();
            return prescriptionsBL.ChartPrescriptionInOneYear(year, ndc, gender);
        }
        /// <summary>
        /// The function gets the ndc and year and gets the numbers of beginning take the prescription in the specific month
        /// </summary>
        /// <param name="year">a year</param>
        /// <param name="ndc">ndc </param>
        /// <returns> a list the numbers of beginning take the prescription in the specific month </returns>
        public List<int> ChartStartPrescriptionInOneYear(int year, string ndc)
        {
            PrescriptionsBL prescriptionsBL = new PrescriptionsBL();
            return prescriptionsBL.ChartStartPrescriptionInOneYear(year, ndc);
        }
        /// <summary>
        /// the function edit prescription
        /// </summary>
        /// <param name="_ID_TZ_Doctor">id of doctor</param>
        /// <param name="_ID_TZ_Patient">id of patient</param>
        /// <param name="_NDC">ndc of medicine</param>
        /// <param name="start">start date</param>
        /// <param name="last">end date</param>
        /// <param name="timesToTake">num of times to take the medicine</param>
        /// <param name="ammount">the amount of medicine</param>
        /// <returns></returns>
        public List<string> EditPrescription(string _ID_TZ_Doctor, string _ID_TZ_Patient, string _NDC,
                      DateTime start, DateTime last, string timesToTake, string ammount)
        {
            Prescriptions prescription = new Prescriptions()
            {
                ID_TZ_Doctor = _ID_TZ_Doctor,
                ID_TZ_Patient = _ID_TZ_Patient,
                NDC = _NDC,
                Start = start,
                Last = last,
                TimesToTake = timesToTake,
                Ammount = ammount,
            };

            PrescriptionsBL bl = new PrescriptionsBL();
            return bl.EditPrescription(prescription);
            
        }
        /// <summary>
        /// the function get prescription by ndc
        /// </summary>
        /// <param name="ndc">ndc of medicine</param>
        /// <returns>a list of prescription by ndc</returns>
        public List<Prescriptions> GetPrescriptionsByNdc(string ndc)
        {
            PrescriptionsBL prescriptionsBL = new PrescriptionsBL();
            return prescriptionsBL.GetPrescriptionsByNdc(ndc);
        }
    

    }
}