using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;

namespace YESS.DAL
{
    public class PatientDB: DbContext
    {
        /// <summary>
        /// patient data base
        /// </summary>
        public PatientDB() : base("PatientDB") { }

        public DbSet<Patient> Patients { get; set; }
     
    }
}
