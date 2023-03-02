using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YESS.BE;

namespace YESS.Models.ViewModel
{
    public class PatientViewModel
    {
		//class of patient view model
		private Patient current;

		public int Id
		{
			get { return current.Id; }
			set { current.Id = value; }
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
		[DisplayName("תעודת זהות")]

		public string ID_TZ
		{
			get { return current.ID_TZ; }
			set { current.ID_TZ = value; }
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

		
		[DisplayName("רקע רפואי")]

		public string Hmo
		{
			get { return current.Hmo; }
			set { current.Hmo = value; }
		}
		[DisplayName("אלרגיה")]

		public string Allergy
		{
			get { return current.Allergy; }
			set { current.Allergy = value; }
		}
		[DisplayName("אופן רכישה")]

		public string Prescription
		{
			get { return current.Prescription; }
			set { current.Prescription = value; }
		}
		/*	[DisplayName("מרשמים")]

			public List<Medicine> Medicines
			{
				get { return current.Medicines; }
				set { current.Medicines = value; }
			}
	*/
		public PatientViewModel()
		{
		}

		public PatientViewModel(Patient p)
		{
			this.current = p;
		}
	}
}
