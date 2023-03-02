using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;
using YESS.DAL;

namespace YESS.BL
{
    /// <summary>
    /// DoctorBL is a class of the doctors that do the logic things
    /// </summary>
    public class DoctorBL
    {

        /// <summary>
        /// this function adds a new doctor to the doctor db
        /// </summary>
        /// <param name="doctor"> an object of a doctor </param>
        /// <returns> list of string that describe if the function successed to add the new doctor to the db </returns>
        public List<string> AddDoctor(Doctor doctor)
        {   

            List<string> answer = new List<string>();
            answer.Add("");
            answer.Add("");
            answer.Add("");
            answer.Add("");
            bool flag = true;

            AdministratorBL administratorBL = new AdministratorBL();
            bool checkUserNameAdminis = administratorBL.CheckUserNameAdministrator(doctor.UserName);
            if (DoctorUserNameExistent(doctor.UserName) == true || checkUserNameAdminis == true)
            {
                flag = false;
                answer[0] = " שם המשתמש כבר קיים במערכת ";
            }

            if (DoctorExistent(doctor.ID_TZ) == true)
            {
                flag = false;
                answer[1] = " מספר ת.ז. זה  כבר קיים במערכת ";
            }

            if (DoctorLicenseExistent(doctor.LicenseNumber) == true)
            {
                flag = false;
                answer[2] = " מספר רישוי זה כבר קיים במערכת";
            }

            if (flag == true)
            {
                DoctorDAL dal = new DoctorDAL();
                answer[3] = dal.AddDoctor(doctor);
            }

                return answer;
        }


        /// <summary>
        /// this function checks if there is a doctor with the same licenseNumber, like in the parameter, in the doctor db
        /// </summary>
        /// <param name="licenseNumber">the licenseNumber that belongs to the doctor that the function has to answer if there is a doctor with the same licenseNumber, like in the parameter, in the doctor db</param>
        /// <returns>true if there is a doctor with the same licenseNumber, like in the parameter, in the doctor db. and false if there isn`t </returns>
        public bool DoctorLicenseExistent(string licenseNumber)
        {
            var checkExistent = GetDoctorByLicense(licenseNumber);
            if (checkExistent != null)
            {
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// this function gets from the doctor db the doctor with the same licenseNumber like in the licenseNumber parameter
        /// </summary>
        /// <param name="licenseNumber">the licenseNumber that belongs to the doctor that the function has to get from the db</param>
        /// <returns>the doctor with the same licenseNumber like in the licenseNumber parameter</returns>
        public Doctor GetDoctorByLicense(string licenseNumber)
        {
            DoctorDAL dal = new DoctorDAL();
            return dal.GetDoctorByLicense(licenseNumber);

        }

        /// <summary>
        ///  this function gets all the doctors from the doctor db
        /// </summary>
        /// <returns>list of the all doctors that are in the doctor db</returns>
        public List<Doctor> GetDoctors()
        {
            DoctorDAL dal = new DoctorDAL();
            return dal.GetDoctors();
        }



        /// <summary>
        /// this function deletes a doctor, with the same id like in the parameter, from the doctor db
        /// </summary>
        /// <param name="id_tz">id of the doctor that the function will delete</param>
        /// <returns>string that describe if the function successed to delete the doctor from the db</returns>
        public string DeleteDoctor(string id_tz)
        {
            if (DoctorExistent(id_tz) == true)
            {
                DoctorDAL dal = new DoctorDAL();
                return dal.DeleteDoctor(id_tz);
            }
            else
                return "הרופא לא קיים במאגר הרופאים";
        }


        /// <summary>
        /// this function gets from the doctor db the doctor with the same id like in the id parameter
        /// </summary>
        /// <param name="id"> the id that belongs to the doctor that the function has to get from the db</param>
        /// <returns>the doctor with the same id like in the id parameter</returns>
        public Doctor GetDoctorByID(string id)
        {
            DoctorDAL doctorDAL = new DoctorDAL();
            return doctorDAL.GetDoctorByID(id);
        }

      
        /// <summary>
        /// this function checks if there is a doctor in the doctor db with the same userName and password like the parametrers
        /// </summary>
        /// <param name="userName">the userName of the doctor that lets him to use inside the app</param>
        /// <param name="password">the password of the doctor that lets him to use inside the app<</param>
        /// <returns>an object that belong to the MYUSER, so it is user that has agreement to use the app </returns>
        public MYUSER DoctorAuthentication(string userName, string password)
        {
            DoctorDAL doctorDAL = new DoctorDAL();
            return doctorDAL.DoctorAuthentication(userName, password);
        }


        /// <summary>
        /// this function counts the amount of doctors in every discipline that are in the doctor db 
        /// </summary>
        /// <returns>list of the amount of doctor that are in every discipline</returns>
        public List<int> FieldDisciplineOfTheDoctor()
        {
            DoctorDAL doctorDAL = new DoctorDAL();
            return doctorDAL.FieldDisciplineOfTheDoctor();
        }

        /// <summary>
        /// this functoins checks and tells if there is a doctor with the specific id in the doctor db
        /// </summary>
        /// <param name="id_Tz"> the id that the function tells if there is a doctor with the specific id in the doctor db </param>
        /// <returns> true if there is a doctor with the specific id in the doctor db and false if there isn`t</returns>
        public bool DoctorExistent(string id_Tz)
        {
            var checkExistent = GetDoctorByID(id_Tz);
            if (checkExistent != null)
            {
                return true;
            }
            else
                return false;
        }



        public List<string> EditDoctor(Doctor doctor)
        {
            var oldDoctor = GetDoctorByID(doctor.ID_TZ);
            List<string> addAnswer = new List<string>();
            addAnswer.Add("");
            addAnswer.Add("");
            addAnswer.Add("");
            addAnswer.Add("");
            var deleteAnswer = DeleteDoctor(doctor.ID_TZ);

            if (deleteAnswer != "הרופא הוסר בהצלחה ממאגר הרופאים")
            {
                addAnswer[0] = "הרופא הנדרש לעידכון אינו קיים במאגר הרופאים";
                return addAnswer;
            }
            else
            {
                addAnswer = AddDoctor(doctor);
                if (addAnswer[3] != "הרופא התווסף בהצלחה למאגר הרופאים")
                {
                    addAnswer[0] = " פרטי הרופא לא עודכנו בהצלחה, כי " + addAnswer[0];
                    AddDoctor(oldDoctor);

                    return addAnswer;
                }
                else
                    addAnswer[3] = "פרטי הרופא עודכנו בהצלחה";
                return addAnswer;
            }
        }

        /// <summary>
        /// this function checks if there is a doctor with the same userName, like in the parameter, in the doctor db
        /// </summary>
        /// <param name="userName">the userName that belongs to the doctor that the function has to answer if there is a doctor with the same userName, like in the parameter, in the doctor db </param>
        /// <returns>true if there is a doctor with the same userName, like in the parameter, in the doctor db. and false if there isn`t </returns>
        public bool DoctorUserNameExistent(string userName)
        {
            var checkExistent = GetDoctorByUserName(userName);
            if (checkExistent != null)
            {
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// this function gets from the doctor db the doctor with the same userName like in the userName parameter
        /// </summary>
        /// <param name="userName">the userName that belongs to the doctor that the function has to get from the db</param>
        /// <returns>the doctor with the same userName like in the userName parameter</returns>
        public Doctor GetDoctorByUserName(string userName)
        {
            DoctorDAL doctorDAL = new DoctorDAL();
            return doctorDAL.GetDoctorByUserName(userName);
        }
    }
}

