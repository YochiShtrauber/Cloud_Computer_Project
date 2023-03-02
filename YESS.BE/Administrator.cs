using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE
{
    /// <summary>
    ///  Administrator is a class of the Administrator
    /// </summary>
    public class Administrator
    {
        public int Id { get; set; }//the id of adminitractor

        [DisplayName("סיסמה")]
        public string Password { get; set; }//the password of doctor
        [DisplayName("שם משתמש")]
        public string UserName { get; set; }//the user name of doctor

        [DisplayName("שם פרטי")]
        public string FirstName { get; set; }//the first name of person
        [DisplayName("שם משפחה")]
        public string LasttName { get; set; }//the last name of person
        [DisplayName("תעודת זהות")]
        public string ID_TZ { get; set; } // the tehudat zehut
    }
}
