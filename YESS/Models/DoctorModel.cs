using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;
using YESS.BE;
using YESS.BL;

namespace YESS.Models
{
    /// <summary>
    /// DoctorModel is a class of the doctors 
    /// </summary>
    public class DoctorModel
    {

        /// <summary>
        ///  this function gets all the doctors from the doctor db
        /// </summary>
        /// <returns>list of the all doctors that are in the doctor db</returns>
        public List<DoctorViewModel> GetDoctors()
        {
            DoctorBL doctorBL = new DoctorBL();
            List<Doctor> doctors = doctorBL.GetDoctors(); 
            List<DoctorViewModel> result = new List<DoctorViewModel>();
            DoctorViewModel dvm = null;
            foreach(var item in doctors)
            {
                dvm = new DoctorViewModel(item);
                result.Add(dvm);
            }
            return result;
        }

        /// <summary>
        /// this function gets from the doctor db the doctor with the same id like in the id parameter
        /// </summary>
        /// <param name="id"> the id that belongs to the doctor that the function has to get from the db</param>
        /// <returns>the doctor with the same id like in the id parameter</returns>
        public DoctorViewModel GetDoctorById(string id)
        {
           DoctorBL doctorBL = new DoctorBL();
           var doctor =  doctorBL.GetDoctorByID(id);
           DoctorViewModel result = new DoctorViewModel(doctor);
           return result;
          
        }

        /// <summary>
        /// this function adds a new doctor to the doctor db
        /// </summary>
        /// <param name="password">the password of the doctor to the application</param>
        /// <param name="userName">the userName of the doctor to the application</param>
        /// <param name="licenseNumber">the licenseNumber of the doctor that improve the degree</param>
        /// <param name="typeSpecial"> the area of expertise</param>
        /// <param name="firstName">the first name of the doctor</param>
        /// <param name="lasttName">the last name of the doctor</param>
        /// <param name="gender">the gender of the doctor</param>
        /// <param name="address">the address of the doctor`s home</param>
        /// <param name="phone">the phone number of the doctor</param>
        /// <param name="mail">the mail of the doctor</param>
        /// <param name="birthDate">the birthDate of the doctor</param>
        /// <param name="_ID_TZ">the id of the doctor</param>
        /// <param name="englishName">the english name of the doctor</param>
        /// <returns> list of string that describe if the function successed to add the new doctor to the db</returns>
        public List<string> AddDoctor(string password, string userName,string licenseNumber, string typeSpecial, string firstName, string lasttName, string gender, 
            string address, string phone, string mail, DateTime birthDate, string _ID_TZ,string englishName)
        {
            Doctor doctor = new Doctor()
            {
               Password = password, UserName = userName,  TypeSpecial = typeSpecial,  FirstName = firstName,  LasttName = lasttName, 
                Gender = gender,  Address = address,
                LicenseNumber = licenseNumber,
                Phone = phone,  Mail = mail,  BirthDate = birthDate,  ID_TZ = _ID_TZ,
                EnglishName=englishName
               
            };
            DoctorBL bl = new DoctorBL();
            return bl.AddDoctor(doctor);
            
        }

        /// <summary>
        /// his function deletes a doctor, with the same id like in the parameter, from the doctor db
        /// </summary>
        /// <param name="id">id of the doctor that the function will delete</param>
        /// <returns>string that describe if the function successed to delete the doctor from the db</returns>
        public string DeleteDoctor(string id)
        {
            DoctorBL bl = new DoctorBL();
            return bl.DeleteDoctor(id);
        }


        /// <summary>
        /// this function checks if there is a doctor in the doctor db with the same userName and password like the parametrers
        /// </summary>
        /// <param name="userName">the userName of the doctor that lets him to use inside the app</param>
        /// <param name="password">the password of the doctor that lets him to use inside the app</param>
        /// <returns>an object that belong to the MYUSER, so it is user that has agreement to use the app</returns>
        public MYUSER DoctorAuthentication(string userName, string password)
        {
            DoctorBL doctorbl = new DoctorBL();
            return doctorbl.DoctorAuthentication(userName, password);
        }

        /// <summary>
        /// this function counts the amount of doctors in every discipline that are in the doctor db 
        /// </summary>
        /// <returns>list of the amount of doctor that are in every discipline</returns>
        public List<int> FieldDisciplineOfTheDoctor()
        {
            DoctorBL doctorBL = new DoctorBL();
            return doctorBL.FieldDisciplineOfTheDoctor();
        }

        /// <summary>
        /// this functoins checks and tells if there is a doctor with the specific id in the doctor db
        /// </summary>
        /// <param name="id_Tz"> the id that the function tells if there is a doctor with the specific id in the doctor db </param>
        /// <returns> true if there is a doctor with the specific id in the doctor db and false if there isn`t</returns>
        public bool DoctorExistent(string id_Tz)
        {
            DoctorBL doctorBL = new DoctorBL();
            return doctorBL.DoctorExistent(id_Tz);
        }

        /// <summary>
        /// this function gets all the ID of the doctors
        /// </summary>
        /// <returns>list of the id of the all doctors</returns>
        public List<string> GetDoctorsID()
        {
            List<DoctorViewModel> doctors = GetDoctors();
            List<string> result = new List<string>();
            string id;
            foreach (var item in doctors)
            {
                id = item.ID_TZ;
                result.Add(id);
            }
            return result;
        }


        /// <summary>
        /// this function edit the details of a specific doctor
        /// </summary>
        /// <param name="password">the password of the doctor to the application</param>
        /// <param name="userName">the userName of the doctor to the application</param>
        /// <param name="_licenceNumber">the licenseNumber of the doctor that improve the degree</param>
        /// <param name="typeSpecial"> the area of expertise</param>
        /// <param name="firstName">the first name of the doctor</param>
        /// <param name="lasttName">the last name of the doctor</param>
        /// <param name="gender">the gender of the doctor</param>
        /// <param name="address">the address of the doctor`s home</param>
        /// <param name="phone">the phone number of the doctor</param>
        /// <param name="mail">the mail of the doctor</param>
        /// <param name="birthDate">the birthDate of the doctor</param>
        /// <param name="_ID_TZ">the id of the doctor</param>
        /// <param name="englishName">the english name of the doctor</param>
        /// <returns> list of string that describe if the function successed to add the new doctor to the db</returns>
        public List<string> EditDoctor(string password, string userName,string _licenceNumber, string typeSpecial, string firstName, string lasttName, string gender,
            string address, string phone, string mail, DateTime birthDate, string _ID_TZ,string englishName)
        {
            Doctor doctor = new Doctor()
            {
                LicenseNumber = _licenceNumber,
                Password = password,
                UserName = userName,
                TypeSpecial = typeSpecial,
                FirstName = firstName,
                LasttName = lasttName,
                Gender = gender,
                Address = address,
                Phone = phone,
                Mail = mail,
                BirthDate = birthDate,
                ID_TZ = _ID_TZ,
                EnglishName = englishName
            };
            DoctorBL bl = new DoctorBL();
            return bl.EditDoctor(doctor);

        }
    }
}

