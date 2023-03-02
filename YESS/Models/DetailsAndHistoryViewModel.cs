using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YESS.BE;
using YESS.Models.ViewModel;

namespace YESS.Models
{

    public class DetailsAndHistoryViewModel
    {

        public PatientViewModel patient = new PatientViewModel();//empty patient
        public List<PrescriptionsViewModel> prescriptions = new List<PrescriptionsViewModel>();//list of prescpriction
        public Prescriptions p = new Prescriptions();//empty prescription

    }
}