using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;

namespace YESS.DAL
{
    public class PatientDAL
    {
        /// <summary>
        /// the function adds patient to the data base
        /// </summary>
        /// <param name="patient">an object of patient</param>
        /// <returns>string that describe the add of patient</returns>
        public string AddPatient(Patient patient)
        {
            using (var ctx = new PatientDB())
            {
                ctx.Patients.Add(patient);
                ctx.SaveChanges();
            }
            return "הלקוח התווסף בהצלחה למאגר הלקוחות";


        }
        /// <summary>
        /// The function deletes patient
        /// </summary>
        /// <param name="id">id of patient</param>
        /// <returns>string that describe if the function deletes patient</returns>
        public string DeletePatient(string id)
        {
            using (var ctx = new PatientDB())
            {
                var patient = ctx.Patients.Where(s => s.ID_TZ == id).FirstOrDefault();
                ctx.Patients.Remove(patient);
                ctx.SaveChanges();
            }
            return "הלקוח נמחק בהצלחה ממאגר הלקוחות";
        }
        /// <summary>
        /// The function gets all the patient
        /// </summary>
        /// <returns>list of all the patient</returns>
        public List<Patient> GetPatients()
        {
            var ctx = new PatientDB();
            return ctx.Patients.ToList();
        }
        /// <summary>
        /// The function get patient by ndc
        /// </summary>
        /// <param name="id">id of patient</param>
        /// <returns>an object of patient</returns>
        public Patient GetPatientByID(string id)
        {
            using (var ctx = new PatientDB())
            {
                var patient = ctx.Patients.Where(s => s.ID_TZ == id).FirstOrDefault();
                return patient;
            }
        }
        /// <summary>
        /// The function gets all the patient by age
        /// </summary>
        /// <returns>list of sum of patient order by age</returns>
        public List<int> AgeOfThePatients()
        {
            List<int> ageList = new List<int>() { 0, 0, 0, 0, 0 };
            using (var ctx = new PatientDB())
            {
                var babyPatient = ctx.Patients.Where(s => s.BirthDate.Year >= DateTime.Now.Year - 2).ToList();
                var childPatient = ctx.Patients.Where(s => s.BirthDate.Year >= DateTime.Now.Year - 18 && s.BirthDate.Year < DateTime.Now.Year - 2).ToList();
                var adult = ctx.Patients.Where(s => s.BirthDate.Year >= DateTime.Now.Year - 45 && s.BirthDate.Year < DateTime.Now.Year - 18).ToList();
                var middle_age = ctx.Patients.Where(s => s.BirthDate.Year >= DateTime.Now.Year - 65 && s.BirthDate.Year < DateTime.Now.Year - 45).ToList();
                var old = ctx.Patients.Where(s => s.BirthDate.Year < DateTime.Now.Year - 65).ToList();


                ageList[0] = babyPatient.Count();
                ageList[1] = childPatient.Count();
                ageList[2] = adult.Count();
                ageList[3] = middle_age.Count();
                ageList[4] = old.Count();

                return ageList;


            }
        }


    }
}
