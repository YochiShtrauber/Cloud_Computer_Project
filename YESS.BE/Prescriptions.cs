using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE
{
    /// <summary>
    /// Prescriptions is a class of the prescriptions
    /// </summary>
    public class Prescriptions
    {
        public int Id { get; set; }
        [DisplayName("תעודת זהות רופא")]
        public string ID_TZ_Doctor { get; set; }//the id of doctor
        [DisplayName("תעודת זהות מטופל")]
        public string ID_TZ_Patient { get; set; }//the id of patient
        [DisplayName("התרופה NDC מזהה")]
        public string NDC { get; set; }//the ndc of medicine
        [DisplayName("תאריך התחלת לקיחת התרופה")]

        public DateTime Start { get; set; }//the start date
        [DisplayName("תאריך סיום לקיחת התרופה")]
        public DateTime Last { get; set; }//the end date
        [DisplayName("מספר פעמים לנטילת התרופה")]
        public string TimesToTake { get; set; }// how many time and when to take the prescription
        [DisplayName("כמות מנה")]
        public string Ammount { get; set; }// the amount of the drug to take every time

    }
}
