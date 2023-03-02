using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YESS.BE;

namespace YESS.Models.ViewModel
{
	public class PrescriptionsViewModel
	{
		//class of patient view model
		private Prescriptions current;

		public int Id
		{
			get { return current.Id; }
			set { current.Id = value; }
		}

		[DisplayName("תעודת זהות רופא")]
		public string ID_TZ_Doctor
		{
			get { return current.ID_TZ_Doctor; }
			set { current.ID_TZ_Doctor = value; }
		}
		[DisplayName("תעודת זהות פציינט")]
		public string ID_TZ_Patient
		{
			get { return current.ID_TZ_Patient; }
			set { current.ID_TZ_Patient = value; }
		}
		[DisplayName("התרופה NDC מזהה")]
		public string NDC
		{
			get { return current.NDC; }
			set { current.NDC = value; }
		}
		[DisplayName("תאריך התחלת לקיחת התרופה")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime Start
		{
			get { return current.Start; }
			set { current.Start = value; }
		}
		[DisplayName("תאריך סיום לקיחת התרופה")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime Last
		{
			get { return current.Last; }
			set { current.Last = value; }
		}
		[DisplayName("מספר פעמים לנטילת התרופה")]
		public string TimesToTake
		{
			get { return current.TimesToTake; }
			set { current.TimesToTake = value; }
		}// how many time and when to take the prescription
		[DisplayName("כמות מנה")]
		public string Ammount
		{
			get { return current.Ammount; }
			set { current.Ammount = value; }
		}// the amount of the drug to take every time

		public PrescriptionsViewModel(Prescriptions p)
		{
			this.current = p;
		}
	}
}









