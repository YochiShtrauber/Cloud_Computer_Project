using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YESS.BE;
using YESS.BL;
using YESS.Models.ViewModel;

namespace YESS.Models
{
    public class PatientModel
    {
        /// <summary>
        /// The function gets all the patient
        /// </summary>
        /// <returns>list of all the patient</returns>
        public List<PatientViewModel> GetPatients()
        {
            PatientBL patientBL = new PatientBL();
            List<Patient> patients = patientBL.GetPatients();
            List<PatientViewModel> result = new List<PatientViewModel>();
            PatientViewModel dvm = null;
            foreach (var item in patients)
            {
                dvm = new PatientViewModel(item);
                result.Add(dvm);
            }
            return result;
        }
       /// <summary>
       /// the function get patient by id
       /// </summary>
       /// <param name="id">id of patient</param>
       /// <returns>an object of view model patient</returns>
        public PatientViewModel GetPatientById(string id) // האם זה לפי תעודת זהות????????
        {
            PatientBL patientBL = new PatientBL();
            var patient = patientBL.GetPatientById(id);
            PatientViewModel result = new PatientViewModel(patient);
            return result;

        }
        /// <summary>
        /// the function adds patient
        /// </summary>
        /// <param name="hmo">the name of your health found of patient</param>
        /// <param name="allergy">the allergy of patient</param>
        /// <param name="prescription">the way the patient want to get his prescription</param>
        /// <param name="firstName">the first name of patient</param>
        /// <param name="lasttName">the last name of patient</param>
        /// <param name="gender">the gender of patient</param>
        /// <param name="address">the address of patient</param>
        /// <param name="phone">/the phone of patient</param>
        /// <param name="mail">the mail of patient</param>
        /// <param name="birthDate">the birth date of patient</param>
        /// <param name="_ID_TZ">the tehudat zehut of patient</param>
        /// <param name="englishName">the english name</param>
        /// <returns>string that describe the add of patient</returns>
        public string AddPatient(string hmo, string allergy, string prescription,
            string firstName, string lasttName, string gender, string address, string phone, string mail,
            DateTime birthDate, string _ID_TZ ,string englishName)
        {
            Patient patient = new Patient()
            {
                Hmo = hmo,
                Allergy = allergy,
                Prescription = prescription,
                EnglishName=englishName,
                FirstName = firstName,
                LasttName = lasttName,
                Gender = gender,
                Address = address,
                Phone = phone,
                Mail = mail,
                BirthDate = birthDate,
                ID_TZ = _ID_TZ,                
            };
           

            PatientBL bl = new PatientBL();
            return bl.AddPatient(patient);
        }
        /// <summary>
        /// The function deletes patient
        /// </summary>
        /// <param name="id">id of patient</param>
        /// <returns>string that describe if the function deletes patient</returns>
        public string DeletePatient(string id_tz)
        {
            PatientBL bl = new PatientBL();
            return bl.DeletePatient(id_tz);
        }
        /// <summary>
        /// the function admin 
        /// </summary>
        /// <param name="userName">user name</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public MYUSER AdminisAuthentication(string userName, string password)
        {
            AdministratorBL bl = new AdministratorBL();
            return bl.AdministratorblAuthentication(userName, password);
        }
        /// <summary>
        /// the function gets all the patients
        /// </summary>
        /// <returns> a list of patient's id</returns>
        public List<string> GetPatientsID()
        {
            List<PatientViewModel> patients = GetPatients();
            List<string> result = new List<string>();
            string id;
            foreach (var item in patients)
            {
                id = item.ID_TZ;
                result.Add(id);
            }
            return result;
        }
        /// <summary>
        /// The function return all the patient by age
        /// </summary>
        /// <returns>list of sum of patient order by age</returns>

        public List<int> AgeOfThePatients()
        {
            PatientBL patientBL = new PatientBL();
            return patientBL.AgeOfThePatients();
        }


        /// <summary>
        /// The function check if yhe patient exist
        /// </summary>
        /// <param name="id_tz_Patient">id of patient</param>
        /// <returns>true if the patient exist else false</returns>
        public bool PatientExistent(string id_tz_Patient)
        {
            PatientBL patientBL = new PatientBL();
            return patientBL.PatientExistent(id_tz_Patient);
        }
        /// <summary>
        /// the function edit patient
        /// </summary>
        /// <param name="hmo">the name of your health found of patient</param>
        /// <param name="allergy">the allergy of patient</param>
        /// <param name="prescription">the way the patient want to get his prescription</param>
        /// <param name="firstName">the first name of patient</param>
        /// <param name="lasttName">the last name of patient</param>
        /// <param name="gender">the gender of patient</param>
        /// <param name="address">the address of patient</param>
        /// <param name="phone">/the phone of patient</param>
        /// <param name="mail">the mail of patient</param>
        /// <param name="birthDate">the birth date of patient</param>
        /// <param name="_ID_TZ">the tehudat zehut of patient</param>
        /// <param name="englishName">the english name</param>
        /// <returns>string that describe the edit of patient</returns>
        public string EditPatient(string hmo, string allergy, string prescription,
            string firstName, string lasttName, string gender, string address, string phone, string mail, DateTime birthDate, string _ID_TZ, string englishName)
        {
            Patient patient = new Patient()
            {
                Hmo = hmo,
                Allergy = allergy,
                Prescription = prescription,
                EnglishName = englishName,
                FirstName = firstName,
                LasttName = lasttName,
                Gender = gender,
                Address = address,
                Phone = phone,
                Mail = mail,
                BirthDate = birthDate,
                ID_TZ = _ID_TZ,
            };


            PatientBL bl = new PatientBL();
            return bl.EditPatient(patient);
        }


    }

}



   