
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;

namespace YESS.DAL
{
   
    public class PrescriptionsDB : DbContext
    {
        /// <summary>
        /// Prescriptions Data Base
        /// </summary>
        public PrescriptionsDB() : base("PrescriptionsDB") { }

            public DbSet<Prescriptions> Prescriptions { get; set; }
    }
}
