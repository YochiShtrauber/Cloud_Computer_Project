using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;

namespace YESS.DAL
{
    public class MedicineDB : DbContext
    {
        /// <summary>
        /// medicine Data Base
        /// </summary>
        public MedicineDB() : base("MedicineDB") { }

        public DbSet<Medicine> Medicines { get; set; }
    }
}
