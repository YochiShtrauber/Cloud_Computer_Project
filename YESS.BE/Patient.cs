using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE
{
    /// <summary>
    /// Patient is a class of the patient
    /// </summary>
    public class Patient 
    {
        public int Id { get; set; }
        [DisplayName("שם פרטי")]
        public string FirstName { get; set; }//the first name of patient
        [DisplayName("שם משפחה")]
        public string LasttName { get; set; }//the last name of patient
        [DisplayName("שם באנגלית")]
        public string EnglishName { get; set; }//the english name of patient
        [DisplayName("מין")]
        public string Gender { get; set; }//the gender of patient
        [DisplayName("כתובת")]
        public string Address { get; set; }//the address of patient
        [DisplayName("מספר טלפון")]
        public string Phone { get; set; }//the phone of patient
        [DisplayName("כתובת מייל")]
        public string Mail { get; set; }//the mail of patient
        [DisplayName("תאריך לידה")]
        public DateTime BirthDate { get; set; }//the birth date of patient
        [DisplayName("תעודת זהות")]
        public string ID_TZ { get; set; } // the tehudat zehut of patient
        [DisplayName("רקע רפואי")] // this is with the drugs and the minun and the dates to take them
        public string Hmo { get; set; }//the name of your health found of patient
        [DisplayName("אלרגיה")]
        public string Allergy { get; set; }//the allergy of patient
        [DisplayName("אופן רכישה")]
        public string Prescription { get; set; }//the way the patient want to get his prescription
      

    }
}