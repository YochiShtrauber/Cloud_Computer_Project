using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YESS.BE
{
    /// <summary>
    /// Medicine is a class of the medicine
    /// </summary>
    public class Medicine
    {
        public int Id { get; set; }//the id of medicine

        [DisplayName("שם")]
        public string Name { get; set; }//the name of medicinr

        [DisplayName("יצרן")]
        public string Producer { get; set; }//the producer of the medicine

        [DisplayName("שם גנרי")]
        public string GenericName { get; set; }//the generic name of medicine

        [DisplayName("מרכיבים פעילים")] 
        public string ActiveIngredients { get; set; }//the active ingredients

        [DisplayName("תצורת התרופה")] 
        public string DosageFormName { get; set; }//     DOSAGEFORMNAME

        [DisplayName("כמות חומר פעיל")]
        public string ACTIVE_NUMERATOR_STRENGTH { get; set; }//the active numerator strenght

        [DisplayName("יחידות החומר הפעיל")]
        public string ACTIVE_INGRED_UNIT { get; set; }//the active ingredient unit

        [DisplayName("תמונה")]  
        public string Image { get; set; }//the Image of medicine
        
        [DisplayName("NDC")] 
        public string PRODUCTNDC { get; set; }//the PRODUCTNDC of the medicine

    }
}