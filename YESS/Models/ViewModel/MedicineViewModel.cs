using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using YESS.BE;

namespace YESS.Models
{
    public class MedicineViewModel
    {
		//class of medicine view model
        private Medicine current;

		public int Id
		{
			get { return current.Id; }
			set { current.Id = value; }
		}
		[DisplayName("שם")]
		public string Name
		{
			get { return current.Name; }
			set { current.Name = value; }
		}
		[DisplayName("יצרן")]
		public string Producer
		{
			get { return current.Producer; }
			set { current.Producer = value; }
		}
		[DisplayName("שם גנרי")]
		public string GenericName
		{
			get { return current.GenericName; }
			set { current.GenericName = value; }
		}
		[DisplayName("מרכיבים פעילים")]
		public string ActiveIngredients
		{
			get { return current.ActiveIngredients; }
			set { current.ActiveIngredients = value; }
		}
		[DisplayName("תצורת התרופה")]
		public string DosageFormName
		{
			get { return current.DosageFormName; }
			set { current.DosageFormName = value; }
		}
		[DisplayName("כמות חומר פעיל")]
		public string ACTIVE_NUMERATOR_STRENGTH
		{
			get { return current.ACTIVE_NUMERATOR_STRENGTH; }
			set { current.ACTIVE_NUMERATOR_STRENGTH = value; }
		}
		[DisplayName("יחידות החומר הפעיל")]
		public string ACTIVE_INGRED_UNIT
		{
			get { return current.ACTIVE_INGRED_UNIT; }
			set { current.ACTIVE_INGRED_UNIT = value; }
		}
		[DisplayName("תמונה")]
		public string Image
		{
			get { return current.Image; }
			set { current.Image = value; }
		}
	
		[DisplayName("NDC")]
		public string PRODUCTNDC
		{
			get { return current.PRODUCTNDC; }
			set { current.PRODUCTNDC = value; }
		}


		public MedicineViewModel (Medicine m)
		{
			this.current = m;
		}
	}
}



