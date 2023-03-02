using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;

namespace YESS.DAL
{
    /// <summary>
    /// DoctorDAL is a class of the doctors db
    /// </summary>
    public class DoctorDAL
    {

        /// <summary>
        /// this function adds a new doctor to the doctor db
        /// </summary>
        /// <param name="doctor"> an object of a doctor </param>
        /// <returns> string that describe if the function successed to add the new doctor to the db </returns>
        public string AddDoctor(Doctor doctor)
        {
            using (var ctx = new DoctorDB())
            {
                ctx.Doctors.Add(doctor);
                ctx.SaveChanges();
            }

            return "הרופא התווסף בהצלחה למאגר הרופאים";
        }


        /// <summary>
        /// this function deletes a doctor, with the same id like in the parameter, from the doctor db
        /// </summary>
        /// <param name="id"> id of the doctor that the function will delete</param>
        /// <returns> string that describe if the function successed to delete the doctor from the db</returns>
        public string DeleteDoctor(string id)
        {

            using (var ctx = new DoctorDB())
            {
                var doctor = ctx.Doctors.Where(s => s.ID_TZ == id).FirstOrDefault();
                ctx.Doctors.Remove(doctor);
                ctx.SaveChanges();
            }
            return "הרופא הוסר בהצלחה ממאגר הרופאים";
        }

        /// <summary>
        /// this function gets all the doctors from the doctor db
        /// </summary>
        /// <returns> list of the all doctors that are in the doctor db </returns>
        public List<Doctor> GetDoctors()
        {
            var ctx = new DoctorDB();
            return ctx.Doctors.ToList();
        }

        /// <summary>
        /// this function gets from the doctor db the doctor with the same id like in the id parameter
        /// </summary>
        /// <param name="id"> the id that belongs to the doctor that the function has to get from the db </param>
        /// <returns> the doctor with the same id like in the id parameter </returns>
        public Doctor GetDoctorByID(string id)
        {
            using (var ctx = new DoctorDB())
            {
                var doctor = ctx.Doctors.Where(s => s.ID_TZ == id).FirstOrDefault();
                return doctor;
            }
        }

        /// <summary>
        /// this function checks if there is a doctor in the doctor db with the same userName and password like the parametrers
        /// </summary>
        /// <param name="userName"> the userName of the doctor that lets him to use inside the app</param>
        /// <param name="password"> the password of the doctor that lets him to use inside the app</param>
        /// <returns> an object that belong to the MYUSER, so it is user that has agreement to use the app  </returns>
        public MYUSER DoctorAuthentication(string userName, string password)
        {
            {

                using (var ctx = new DoctorDB())
                {
                    var doctor = ctx.Doctors.Where(s => s.UserName == userName && s.Password == password).FirstOrDefault();
                    if (doctor != null)
                    {
                        MYUSER U = new MYUSER();
                        U.FirstName = doctor.FirstName;
                        U.LasttName = doctor.LasttName;
                        U.ID_TZ = doctor.ID_TZ;
                        U.UserName = doctor.UserName;
                        U.Password = doctor.Password;
                        return U;
                    }
                    else
                        return null;
                }
            }
        }

        /// <summary>
        ///  this function gets from the doctor db the doctor with the same licenseNumber like in the licenseNumber parameter
        /// </summary>
        /// <param name="licenseNumber">the licenseNumber that belongs to the doctor that the function has to get from the db</param>
        /// <returns>the doctor with the same licenseNumber like in the licenseNumber parameter </returns>
        public Doctor GetDoctorByLicense(string licenseNumber)
        {
            using (var ctx = new DoctorDB())
            {
                var doctor = ctx.Doctors.Where(s => s.LicenseNumber == licenseNumber).FirstOrDefault();
                return doctor;
            }
        }

        /// <summary>
        /// this function counts the amount of doctors in every discipline that are in the doctor db 
        /// </summary>
        /// <returns>list of the amount of doctor that are in every discipline</returns>
        public List<int> FieldDisciplineOfTheDoctor()
        {
            List<int> disciplineList = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
            using (var ctx = new DoctorDB())
            {
                
                var childrenDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "ילדים").ToList();
                var familyDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "משפחה").ToList();
                var womenDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "נשים").ToList();
                var skinDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "עור").ToList();
                var orthopedicsDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "אורטופדיה").ToList();
                var otolaryngologistDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "אף אוזן גרון").ToList();
                var internistDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "פנימית").ToList();
                var eyesDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "עיניים").ToList();
                var dentistDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "שיניים").ToList();
                var heartDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "לב").ToList();
                var psychiatryDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "פסיכיאטריה").ToList();
                var neurologyDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "נוירולוגיה").ToList();
                var otherDiscipline = ctx.Doctors.Where(s => s.TypeSpecial == "אחר").ToList();


                disciplineList[0] = childrenDiscipline.Count();
                disciplineList[1] = familyDiscipline.Count();
                disciplineList[2] = womenDiscipline.Count();
                disciplineList[3] = skinDiscipline.Count();
                disciplineList[4] = orthopedicsDiscipline.Count();
                disciplineList[5] = otolaryngologistDiscipline.Count();
                disciplineList[6] = internistDiscipline.Count();
                disciplineList[7] = eyesDiscipline.Count();
                disciplineList[8] = dentistDiscipline.Count();
                disciplineList[9] = heartDiscipline.Count();
                disciplineList[10] = psychiatryDiscipline.Count();
                disciplineList[11] = neurologyDiscipline.Count();
                disciplineList[12] = otherDiscipline.Count();
 
                return disciplineList;


            }
        }

        /// <summary>
        /// this function gets from the doctor db the doctor with the same userName like in the userName parameter
        /// </summary>
        /// <param name="userName">the userName that belongs to the doctor that the function has to get from the db</param>
        /// <returns>the doctor with the same userName like in the userName parameter</returns>
        public Doctor GetDoctorByUserName(string userName)
        {
            using (var ctx = new DoctorDB())
            {
                var doctor = ctx.Doctors.Where(s => s.UserName == userName).FirstOrDefault();
                return doctor;
            }
        }
    }
}


   