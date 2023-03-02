using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YESS.BE;

namespace YESS.Models
{
    public class CreatePrescriptionViewModel
    {
        public List<string> ndcs = new List<string>();//list of all ndc
        public List<string> tz = new List<string>();//list of all patient id
        public List<string> tzDoctors = new List<string>();//list of all doctors id
        public Medicine medicine = new Medicine();//empty medicine
        public Prescriptions prescriptions = new Prescriptions();//empty prescriptions
        public Patient patient = new Patient();//empty patient
        public string id = "";
        public DateTime startdate = new DateTime();//start  of presprection
        public DateTime lastdate = new DateTime();//end  of presprection

        public CreatePrescriptionViewModel()//constructor
        {
            MedicineModel model = new MedicineModel();
            ndcs = model.GetNDC();
            PatientModel model1 = new PatientModel();
            tz = model1.GetPatientsID();
            DoctorModel model2 = new DoctorModel();
            tzDoctors=model2.GetDoctorsID();
        }
    }
}