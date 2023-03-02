using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;
using YESS.DAL;

namespace YESS.BL
{
    public class PatientBL
    {
        /// <summary>
        /// the function adds patient to the data base
        /// </summary>
        /// <param name="patient">an object of patient</param>
        /// <returns>string that describe the add of patient</returns>
        public string AddPatient(Patient patient)
        {
            if (PatientExistent(patient.ID_TZ) != true)
            {
                PatientDAL dal = new PatientDAL();
                return dal.AddPatient(patient);
            }
            else
                return "הלקוח כבר קיים במערכת ";


        }
        /// <summary>
        /// The function gets all the patient
        /// </summary>
        /// <returns>list of all the patient</returns>
        public List<Patient> GetPatients()
        {
            PatientDAL dal = new PatientDAL();
            return dal.GetPatients();
        }


        /// <summary>
        /// The function deletes patient
        /// </summary>
        /// <param name="id">id of patient</param>
        /// <returns>string that describe if the function deletes patient</returns>
        public string DeletePatient(string id_tz)
        {
            if (PatientExistent(id_tz) == true)
            {
                PatientDAL dal = new PatientDAL();
                PrescriptionsBL prescriptionsBL = new PrescriptionsBL();
                prescriptionsBL.DeleteALLPrescOfSpechificPatient(id_tz);
                return dal.DeletePatient(id_tz);
            }
            else
                return "הלקוח לא קיים במאגר הלקוחות";
        }
        /// <summary>
        /// The function get patient by ndc
        /// </summary>
        /// <param name="id">id of patient</param>
        /// <returns>an object of patient</returns>
        public Patient GetPatientById(string id)
        {
            PatientDAL patientDAL = new PatientDAL();
            return patientDAL.GetPatientByID(id);
        }

        /// <summary>
        /// The function return all the patient by age
        /// </summary>
        /// <returns>list of sum of patient order by age</returns>

        public List<int> AgeOfThePatients()
        {
            PatientDAL patientDAL = new PatientDAL();
            return patientDAL.AgeOfThePatients();
        }


        /// <summary>
        /// The function check if yhe patient exist
        /// </summary>
        /// <param name="id_tz_Patient">id of patient</param>
        /// <returns>true if the patient exist else false</returns>
        public bool PatientExistent(string id_tz_Patient)
        {
            var checkExistent = GetPatientById(id_tz_Patient);
                if(checkExistent != null)
            {
                return true;
            }
                else
                return false;
        }
        /// <summary>
        /// The function edit the patient
        /// </summary>
        /// <param name="patient">an object of [atient</param>
        /// <returns>string that describe if the function edit the patient</returns>
        public string EditPatient(Patient patient)
        {
            var deleteAnswer = DeletePatient(patient.ID_TZ);

            if (deleteAnswer != "הלקוח נמחק בהצלחה ממאגר הלקוחות")
            {
                return "הלקוח הנדרש לעידכון אינו קיים במאגר הלקוחות";
            }
            else
            {
                var addAnswer = AddPatient(patient);
                if (addAnswer != "הלקוח התווסף בהצלחה למאגר הלקוחות")
                {
                    return "פרטי הלקוח לא עודכנו בהצלחה";
                }
                else
                    return "פרטי הלקוח עודכנו בהצלחה";
            }


        }
    }
}
