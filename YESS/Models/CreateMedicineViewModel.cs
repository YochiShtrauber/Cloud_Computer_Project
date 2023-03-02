using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YESS.BE;

namespace YESS.Models
{

    public class CreateMedicineViewModel
    {
        public List<string> ndcs = new List<string>();//list of all ndc
        public Medicine medicine =new Medicine();//empty medicine
        public  CreateMedicineViewModel()//constructor
        {
            MedicineModel model = new MedicineModel();
            ndcs = model.GetNDC();
        }
    }
}