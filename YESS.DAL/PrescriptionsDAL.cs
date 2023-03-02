using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using YESS.BE;

namespace YESS.DAL
{
    public class PrescriptionsDAL
    {
        /// <summary>
        /// The function adds prescription to the data base
        /// </summary>
        /// <param name="prescriptions">an object of prescription</param>
        /// <returns>string that describe if the function add prescription to the data base</returns>
        public string AddPrescriptions(Prescriptions prescriptions)
        {
            using (var ctx = new PrescriptionsDB())
            {
                ctx.Prescriptions.Add(prescriptions);
                ctx.SaveChanges();

            }
            return "המרשם נקלט בהצלחה במאגר המרשמים";
        }
        /// <summary>
        /// The function gets prescription from db
        /// </summary>
        /// <returns> a list of all the prescriptions </returns>
        public List<Prescriptions> GetPrescriptions()
        {
            var ctx = new PrescriptionsDB();
            return ctx.Prescriptions.ToList();
        }
        /// <summary>
        /// The function gets all the prescription that belong to the id patient
        /// </summary>
        /// <param name="id_tz">id of patient</param>
        /// <returns> a list of the prescription that belong to the id patient</returns>
        public List<Prescriptions> GetPrescriptionsByIdPatient(string id_tz)
        {
            using (var ctx = new PrescriptionsDB())
            {
                var prescriptions = ctx.Prescriptions.Where(s => s.ID_TZ_Patient == id_tz).ToList();
                return prescriptions;
            }
        }
        /// <summary>
        /// The function deletes all the prescriptions of speceific ndc and that belong to the id patient 
        /// </summary>
        /// <param name="id_tz_Patient">the id of patient</param>
        /// <param name="_ndc"> ndc of medicine</param>
        /// <returns> string that describe if the function deletes all the prescriptions of speceific ndc and that belong to the id patient </returns>
        public string DeletePrescriptionsByIdPatient(string id_tz_Patient, string _ndc)
        {
            using (var ctx = new PrescriptionsDB())
            {
                List<Prescriptions> prescripationPatientList = ctx.Prescriptions.Where(s => s.ID_TZ_Patient == id_tz_Patient && s.NDC == _ndc).ToList();
                Prescriptions prescriptionToDelete = prescripationPatientList[0];
                DateTime maxDate = prescripationPatientList[0].Start;
                foreach (var item in prescripationPatientList)
                {
                    if(item.Start > maxDate)
                    {
                        maxDate = item.Start;
                        prescriptionToDelete = item;
                    }
                }

                ctx.Prescriptions.Remove(prescriptionToDelete);

                ctx.SaveChanges();
            }
            return "המרשם נמחק בהצלחה ממאגר המרשמים";
        }

        /// <summary>
        /// the function deletes all the prescription that belong to the id patient
        /// </summary>
        /// <param name="id_tz_Patient">the id of patient</param>
        /// <returns>a string that describes if the function deletes all the prescription that belong to the id patient </returns>
        public string DeleteALLPrescOfSpechificPatient(string id_tz_Patient)
        {
            using (var ctx = new PrescriptionsDB())
            {
                var prescriptions = ctx.Prescriptions.Where(s => s.ID_TZ_Patient == id_tz_Patient).ToList();
                foreach(var item in prescriptions)
                {
                    ctx.Prescriptions.Remove(item);
                }

                ctx.SaveChanges();
            }
            return "כל המרשמים של הלקוח נמחקו בהצלחה ממאגר המרשמים";
        }
        /// <summary>
        /// The function gets all the prescriptions of specific NDC that belong to the id patient
        /// </summary>
        /// <param name="_NDC">a ndc of medicine </param>
        /// <param name="id_tz_Patient">id of patient</param>
        /// <returns>an object of a prescription of specific NDC that belong to the id patient </returns>
        public Prescriptions GetPrescriptionsByNDCAndPatientID(string _NDC,string id_tz_Patient)
        {
            PatientDAL patientDAL = new PatientDAL();
            using (var ctx = new PrescriptionsDB())
            {
                var prescriptions = ctx.Prescriptions.Where(s => s.NDC == _NDC&&s.ID_TZ_Patient== id_tz_Patient).FirstOrDefault(); 
              
                return prescriptions;
            }
        }
        /// <summary>
        ///the function  get patient by ndc of medicine
        /// </summary>
        /// <param name="ndc">ndc of medicine</param>
        /// <returns> a list of patient by ndc of medicine</returns>
        public List<Patient> GetpatientsByNDC(string ndc)
        {
            PatientDAL patientDAL = new PatientDAL();
            
                var prescriptions = GetPrescriptionsByNdc(ndc); // פה זה רשימה של המרשמים של אותו NDC
                List<Patient> patientsNDCList = new List<Patient>();
                foreach(var item in prescriptions)
                {

                    var patientID = patientDAL.GetPatientByID(item.ID_TZ_Patient);
                    patientsNDCList.Add(patientID);
                }

                return patientsNDCList;
            
        }

        /// <summary>
        /// the function get list of rxui from list of ndc
        /// </summary>
        /// <param name="ndcNames">ndc of medicine</param>
        /// <returns> list of rxui</returns>
        public List<string> getRxForListPre(List<string> ndcNames) 
        {

            List<string> rxuList = new List<string>();
            foreach (var itemNdc in ndcNames)
            {

                var httpRequest = (HttpWebRequest)WebRequest.Create("https://rxnav.nlm.nih.gov/REST/rxcui?idtype=NDC&id=" + itemNdc);

                var response = (HttpWebResponse)httpRequest.GetResponse();
                var recieveString = response.GetResponseStream();
                var mySourceDoc = new XmlDocument();
                mySourceDoc.Load(recieveString);
                recieveString.Close();
                XmlNodeList elemList = mySourceDoc.GetElementsByTagName("rxnormId");
                if (elemList.Count == 0)
                    return null;

                var innerText = elemList.Item(0).InnerText;
                rxuList.Add(innerText);

            }

            return rxuList;


        }
        /// <summary>
        /// The function checks the interaction detween drugs by server
        /// </summary>
        /// <param name="rxNames">list of rxui</param>
        /// <returns>a list that describe the interaction between drugs</returns>
        public List<string> checkInteractionDrugsByServer(List<string> rxNames)
        {
            string rxToServerInteraction = "";
            for(int i = 0; i < rxNames.Count; i++)
            {
                rxToServerInteraction += rxNames[i];
                if(i< rxNames.Count - 1)
                {
                    rxToServerInteraction += "+";
                }
            }

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("https://rxnav.nlm.nih.gov/REST/interaction/list?rxcuis=" + rxToServerInteraction);

            HttpWebResponse serverResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream recieveString = serverResponse.GetResponseStream();
            var mySourceDoc = new XmlDocument();
            mySourceDoc.Load(recieveString);
            recieveString.Close();
            XmlNodeList elemListSeverity = mySourceDoc.GetElementsByTagName("severity");
            XmlNodeList elemListDescription = mySourceDoc.GetElementsByTagName("description");
            if (elemListDescription.Count == 0)
                return null;
            List<string> resultList = new List<string>();
            for (int i = 0; i < elemListSeverity.Count; i++)
            {
                string severity = elemListSeverity.Item(i).InnerText;
                string description = elemListDescription.Item(i).InnerText;
                resultList.Add(severity+ " " + description);
            }
            return resultList;
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
            List<int> monthNDCFrequency = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var prescriptions = GetPrescriptionsByNdc(ndc);
            var yearList = prescriptions.Where(s => s.Start.Year <= year && s.Last.Year >= year).ToList();
            if (gender != "everyThing")
            {
                PatientDAL patientDAL = new PatientDAL();
                yearList = yearList.Where(s => patientDAL.GetPatientByID(s.ID_TZ_Patient).Gender == gender).ToList();
            }
            foreach (var item in yearList)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (item.Start.Year == year)
                    {
                        if (item.Start.Month <= i + 1)
                        {
                            if (item.Last.Year > year)
                            {
                                monthNDCFrequency[i] += 1;
                            }
                            else
                            {
                                if (item.Last.Month >= i + 1)
                                {
                                    monthNDCFrequency[i] += 1;
                                }
                            }
                        }
                    }
                    else // the start year differ from the given year
                    {
                        if (item.Start.Year < year)
                        {
                            if (item.Last.Year >= year)
                            {
                                if (item.Last.Year > year)
                                {
                                    monthNDCFrequency[i] += 1;
                                }
                                else
                                {
                                    if (item.Last.Month >= i + 1)
                                    {
                                        monthNDCFrequency[i] += 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return monthNDCFrequency;
        }


                /// <summary>
                /// The function gets the ndc and year and gets the numbers of beginning take the prescription in the specific month
                /// </summary>
                /// <param name="year">a year</param>
                /// <param name="ndc">ndc </param>
                /// <returns> a list the numbers of beginning take the prescription in the specific month </returns>
        public List<int> ChartStartPrescriptionInOneYear(int year, string ndc)
        {
            List<int> monthNDCFrequency = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var prescriptions = GetPrescriptionsByNdc(ndc);
                var yearList = prescriptions.Where(s => s.Start.Year == year).ToList();
                foreach (var item in yearList)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (item.Start.Month == i + 1)
                        {
                            monthNDCFrequency[i] += 1;
                        }
                    }  
                }
            return monthNDCFrequency;
        }

        /// <summary>
        /// the function get prescription byndc
        /// </summary>
        /// <param name="ndc">ndc of medicine</param>
        /// <returns>a list of prescription by ndc</returns>
        public List<Prescriptions> GetPrescriptionsByNdc(string ndc)
        {

            using (var ctx = new PrescriptionsDB())
            {
                var prescriptions = ctx.Prescriptions.Where(s => s.NDC == ndc).ToList();
                return prescriptions;
            }
        
        }
        /// <summary>
        /// The function get prescription by patient and a date
        /// </summary>
        /// <param name="id_TZ_Patient">id of patient</param>
        /// <param name="start">start</param>
        /// <param name="last">end date</param>
        /// <returns>a list of prescriptions by patient and a date</returns>
        public List<Prescriptions> GetPrescriptionsByIdPatientAndDate(string id_TZ_Patient,DateTime start,DateTime last)
        {
            using (var ctx = new PrescriptionsDB())
            {
                var prescriptions = ctx.Prescriptions.Where(s => s.ID_TZ_Patient == id_TZ_Patient && ((s.Last >= start && s.Start <= last) || (s.Start > start && s.Start <= last ) 
                || (s.Start <= start && s.Last < last&&s.Last>start))).ToList();
                return prescriptions;
            }
        }
    }
    }
