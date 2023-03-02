using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE
{
    /// <summary>
    /// Doctor class is a class of the doctor
    /// </summary>
    public class Doctor 
    {
        public int Id { get; set; }//the id of doctor
        [DisplayName("שם פרטי")]
        public string FirstName { get; set; }//the first name of person
        [DisplayName("מספר רישוי")]
        public string LicenseNumber { get; set; }//the licence number of doctor
        [DisplayName("שם משפחה")]
        public string LasttName { get; set; }//the last name of person

        [DisplayName("שם באנגלית")]
        public string EnglishName { get; set; }//the english name of person

        [DisplayName("מין")]
        public string Gender { get; set; }//the gender of person
        [DisplayName("כתובת")]
        public string Address { get; set; }//the address of person
        [DisplayName("מספר טלפון")]
        public string Phone { get; set; }//the phone of person
        [DisplayName("כתובת מייל")]
        public string Mail { get; set; }//the mail of person
        [DisplayName("תאריך לידה")]
        public DateTime BirthDate { get; set; }//the birth date of person
        [DisplayName("תעודת זהות")]
        public string ID_TZ { get; set; } // the tehudat zehut 
        [DisplayName("סיסמה")]
        public string Password { get; set; }//the password of doctor
        [DisplayName("שם משתמש")]
        public string UserName { get; set; }//the user name of doctor
        [DisplayName("תחום התמחות")]
        public string TypeSpecial { get; set; }//the type of doctor's specialization
    }
}
