using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;
using YESS.DAL;

namespace YESS.BL
{
    public class PrescriptionsBL
    {
        /// <summary>
        /// The function adds prescription to the data base
        /// </summary>
        /// <param name="prescriptions">an object of prescription</param>
        /// <returns>string that describe if the function add prescription to the data base</returns>
        public List<string> AddPrescriptions(Prescriptions prescriptions)
        {
            List<string> answer = new List<string>();
            answer.Add("");
            answer.Add("");
            answer.Add("");
            answer.Add("");

            bool flag = true;
            if (prescriptions.Last < prescriptions.Start)
            {
                flag = false;
                answer[2] = "לא ניתן להנפיק את המרשם, כי תאריך ההתחלה של לקיחת המרשם צריך להיות קטן או שווה לתאריך הסיום";
            }

            if (flag == true)
            {
                PrescriptionsDAL dal = new PrescriptionsDAL();
                answer[3] = dal.AddPrescriptions(prescriptions);
            }
            return answer;
        }

        /// <summary>
        /// The function gets prescription from db
        /// </summary>
        /// <returns> a list of all the prescriptions </returns>
        public List<Prescriptions> GetPrescriptions()
        {
            PrescriptionsDAL dal = new PrescriptionsDAL();
            return dal.GetPrescriptions();
        }
        /// <summary>
        /// The function gets all the prescription that belong to the id patient
        /// </summary>
        /// <param name="id_tz">id of patient</param>
        /// <returns> a list of the prescription that belong to the id patient</returns>
        public List<Prescriptions> GetPrescriptionsByIdPatient(string id_tz)
        {
            PrescriptionsDAL prescriptionsDAL = new PrescriptionsDAL();
            return prescriptionsDAL.GetPrescriptionsByIdPatient(id_tz);
        }
        /// <summary>
        /// The function deletes all the prescriptions of speceific ndc and that belong to the id patient 
        /// </summary>
        /// <param name="id_tz_Patient">the id of patient</param>
        /// <param name="_ndc"> ndc of medicine</param>
        /// <returns> string that describe if the function deletes all the prescriptions of speceific ndc and that belong to the id patient </returns>
        public string DeletePrescriptionsByIdPatient(string id_tz_Patient, string _ndc)
        {
            //פה יש לבדוק האם בכלל קיים במאגר מרשם כזה.
            PrescriptionsDAL dal = new PrescriptionsDAL();
            return dal.DeletePrescriptionsByIdPatient(id_tz_Patient, _ndc);
        }


        /// <summary>
        /// the function deletes all the prescription that belong to the id patient
        /// </summary>
        /// <param name="id_tz_Patient">the id of patient</param>
        /// <returns>a string that describes if the function deletes all the prescription that belong to the id patient </returns>
        public string DeleteALLPrescOfSpechificPatient(string id_tz_Patient)
        {
            PrescriptionsDAL dal = new PrescriptionsDAL();
            return dal.DeleteALLPrescOfSpechificPatient(id_tz_Patient);
        }
        /// <summary>
        /// The function gets all the prescriptions of specific NDC that belong to the id patient
        /// </summary>
        /// <param name="_NDC">a ndc of medicine </param>
        /// <param name="id_tz_Patient">id of patient</param>
        /// <returns>an object of a prescription of specific NDC that belong to the id patient </returns>
        public Prescriptions GetPrescriptionsByNDCAndPatientID(string _NDC, string id_tz_Patient)
        {
            PrescriptionsDAL dal = new PrescriptionsDAL();
            return dal.GetPrescriptionsByNDCAndPatientID(_NDC, id_tz_Patient);
        }
        /// <summary>
        ///the function  get patient by ndc of medicine
        /// </summary>
        /// <param name="ndc">ndc of medicine</param>
        /// <returns> a list of patient by ndc of medicine</returns>
        public List<Patient> GetpatientsByNDC(string _NDC)
        {
            PrescriptionsDAL dal = new PrescriptionsDAL();
            return dal.GetpatientsByNDC(_NDC);

        }

        /// <summary>
        /// The function gets the description of add prescription
        /// </summary>
        /// <param name="myNDC">ndc of medicine</param>
        /// <param name="id_TZ_Patient">id of patient</param>
        /// <returns>list that describe the allow of add prescription</returns>
        public List<string> getPrescriptionsAllow(string myNDC, string id_TZ_Patient, DateTime start, DateTime last) //פונקציה שמחזירה את התיאור של ההתנגשויות שיש להציג על המסך
        {
            // List<Prescriptions> patientPrescriptions = GetPrescriptionsByIdPatient(id_TZ_Patient);
            List<Prescriptions> patientPrescriptions = GetPrescriptionsByIdPatientAndDate(id_TZ_Patient, start, last);
            List<string> ndcNames = new List<string>();
            ndcNames.Add(myNDC);
            foreach (var item in patientPrescriptions)
            {
                ndcNames.Add(item.NDC);

            }
            List<string> rxNames;
            PrescriptionsDAL prescriptionDAL = new PrescriptionsDAL();

            rxNames = prescriptionDAL.getRxForListPre(ndcNames);
            if (rxNames == null)
            {
                List<string> result = new List<string>();
                result.Add("RX" + " לא ניתן לבדוק התנגשות בין המרשמים, כיוון שאחד המרשמים לא קיים");
                return result;
            }
            List<string> result2 = checkInteractionDrugsByServer(rxNames);
            if (result2 == null)
            {
                result2 = new List<string>();
                result2.Add("לא קיימות התנגשויות בין המרשמים עבור פציינט זה");

            }
            return result2;
        }

        /// <summary>
        /// The function checks the interaction detween drugs by server
        /// </summary>
        /// <param name="rxNames">list of rxui</param>
        /// <returns>a list that describe the interaction between drugs</returns>
        public List<string> checkInteractionDrugsByServer(List<string> rxNames)
        {
            PrescriptionsDAL dal = new PrescriptionsDAL();
            return dal.checkInteractionDrugsByServer(rxNames);

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
            PrescriptionsDAL prescriptionDal = new PrescriptionsDAL();
            return prescriptionDal.ChartPrescriptionInOneYear(year, ndc, gender);
        }

        /// <summary>
        /// The function gets the ndc and year and gets the numbers of beginning take the prescription in the specific month
        /// </summary>
        /// <param name="year">a year</param>
        /// <param name="ndc">ndc </param>
        /// <returns> a list the numbers of beginning take the prescription in the specific month </returns>
        public List<int> ChartStartPrescriptionInOneYear(int year, string ndc)
        {
            PrescriptionsDAL prescriptionsDAL = new PrescriptionsDAL();
            return prescriptionsDAL.ChartStartPrescriptionInOneYear(year, ndc);
        }
        /// <summary>
        /// The function edit prescription
        /// </summary>
        /// <param name="prescription">an object of prescription</param>
        /// <returns>list that describes the edit prescription </returns>
        public List<string> EditPrescription(Prescriptions prescription)
        {
            List<string> addAnswer = new List<string>();
            addAnswer.Add("");
            addAnswer.Add("");
            addAnswer.Add("");
            addAnswer.Add("");
            var deleteAnswer = DeletePrescriptionsByIdPatient(prescription.ID_TZ_Patient, prescription.NDC);

            if (deleteAnswer != "המרשם נמחק בהצלחה ממאגר המרשמים")
            {
                addAnswer[0] = "המרשם הנדרש לעידכון אינו קיים במאגר המרשמים";
            }
            else
            {
                addAnswer = AddPrescriptions(prescription);
                if (addAnswer[3] != "המרשם נקלט בהצלחה במאגר המרשמים")
                {
                    if (addAnswer[2] == "לא ניתן להנפיק את המרשם, כי תאריך ההתחלה של לקיחת המרשם צריך להיות קטן או שווה לתאריך הסיום")
                    {
                        addAnswer[2] = "לא ניתן לעדכן את המרשם, כי תאריך ההתחלה של לקיחת המרשם צריך להיות קטן או שווה לתאריך הסיום";
                    }

                    if (addAnswer[0] == "לא ניתן להנפיק את המרשם כי הכמות הנדרשת של התרופה צריכה להיות גדולה מאפס")
                    {
                        addAnswer[0] = "לא ניתן לעדכן את המרשם כי הכמות הנדרשת של התרופה צריכה להיות גדולה מאפס";
                    }

                    if (addAnswer[1] == "לא ניתן להנפיק את המרשם כי מספר הפעמים שיש ליטול את התרופה צריך להיות גדול מאפס")
                    {
                        addAnswer[1] = "לא ניתן לעדכן את המרשם כי מספר הפעמים שיש ליטול את התרופה צריך להיות גדול מאפס";
                    }

                    addAnswer[3] = "המרשם לא עודכן בהצלחה";
                }
                else
                    addAnswer[3] = "המרשם עודכן בהצלחה ";
            }

            return addAnswer;
        }


        /// <summary>
        /// the function get prescription byndc
        /// </summary>
        /// <param name="ndc">ndc of medicine</param>
        /// <returns>a list of prescription by ndc</returns>
        public List<Prescriptions> GetPrescriptionsByNdc(string ndc)
        {
            PrescriptionsDAL prescriptionsDAL = new PrescriptionsDAL();
            return prescriptionsDAL.GetPrescriptionsByNdc(ndc);

        }
        /// <summary>
        /// The function get prescription by id of patient and date
        /// </summary>
        /// <param name="id_TZ_Patient">id of patient</param>
        /// <param name="start">start</param>
        /// <param name="last">end date</param>
        /// <returns>a list of prescriptions by patient and a date</returns>
        public List<Prescriptions> GetPrescriptionsByIdPatientAndDate(string id_TZ_Patient, DateTime start, DateTime last)
        {
            PrescriptionsDAL prescriptionsDAL = new PrescriptionsDAL();
            return prescriptionsDAL.GetPrescriptionsByIdPatientAndDate(id_TZ_Patient, start, last);
        }
    }
}
