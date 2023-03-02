using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YESS.BE;

namespace YESS.Models
{
    public class DoctorViewModel
    {// class of doctor view model
		private Doctor current;

		public int Id
		{
			get { return current.Id; }
			set { current.Id = value; }
		}
		[DisplayName("מספר רישוי")]
		public string LicenseNumber
		{
			get { return current.LicenseNumber; }
			set { current.LicenseNumber = value; }
		}
		[DisplayName("שם פרטי")]
		public string FirstName
		{
			get { return current.FirstName; }
			set { current.FirstName = value; }
		}
		[DisplayName("שם משפחה")]
		public string LasttName
		{
			get { return current.LasttName; }
			set { current.LasttName = value; }
		}
		[DisplayName("שם באנגלית")]
		public string EnglishName
		{
			get { return current.EnglishName; }
			set { current.EnglishName = value; }
		}
		[DisplayName("מין")]
		public string Gender
		{
			get { return current.Gender; }
			set { current.Gender = value; }
		}
		[DisplayName("כתובת")]
		public string Address
		{
			get { return current.Address; }
			set { current.Address = value; }
		}
		[DisplayName("מספר טלפון")]
		public string Phone
		{
			get { return current.Phone; }
			set { current.Phone = value; }
		}
		[DisplayName("כתובת מייל")]
		public string Mail
		{
			get { return current.Mail; }
			set { current.Mail = value; }
		}
		[DisplayName("תאריך לידה")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime BirthDate
		{
			get { return current.BirthDate; }
			set { current.BirthDate = value; }
		}
		[DisplayName("תעודת זהות")]

		public string ID_TZ
		{
			get { return current.ID_TZ; }
			set { current.ID_TZ = value; } // the tehudat zehut 
		}
		[DisplayName("סיסמה")]
		public string Password
		{
			get { return current.Password; }
			set { current.Password = value; }
		}
		[DisplayName("שם משתמש")]

		public string UserName
		{
			get { return current.UserName; }
			set { current.UserName = value; }
		}
		[DisplayName("תחום התמחות")]

		public string TypeSpecial
		{
			get { return current.TypeSpecial; }
			set { current.TypeSpecial = value; }
		}

		public DoctorViewModel(Doctor p)
		{
			this.current = p;
		}
	}
}