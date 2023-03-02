using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YESS.BE;

namespace YESS.Models
{
    /// <summary>
    /// ChartsViewModel is class of the chart view model
    /// </summary>
    public class ChartsViewModel
    {
        public List<string> ndcs = new List<string>();
        public List<string> years = new List<string>();
        public string MYyears = "";

        public Medicine medicine = new Medicine();
        /// <summary>
        /// constructor
        /// </summary>
        public ChartsViewModel()
        {
            MedicineModel model = new MedicineModel();
            ndcs = model.GetNDC();
            for (int i = Convert.ToInt32(DateTime.Now.Year); i >1979 ; i--)
            {
                years.Add(i.ToString());
            }
        }
    }

}