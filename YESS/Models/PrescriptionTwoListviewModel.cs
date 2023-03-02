using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YESS.Models
{
    /// <summary>
    /// PrescriptionTwoListviewModel is class of two prescriptions
    /// </summary>
    public class PrescriptionTwoListviewModel
    {

        public List<int> resultMale = new List<int>();//list of male prescription
        public List<int> resultFemale = new List<int>();//list of female prescription
        /// <summary>
        /// constractor of PrescriptionTwoListviewModel class
        /// </summary>
        /// <param name="year"> a year</param>
        /// <param name="ndc">ndc of medicine</param>
        public PrescriptionTwoListviewModel(int year, string ndc)
        {
            PrescriptionsModel prescriptionsModel = new PrescriptionsModel();
           
        }
    }
}