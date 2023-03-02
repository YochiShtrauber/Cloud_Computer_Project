using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;

namespace YESS.DAL
{
    public class DoctorDB : DbContext
    {
        public DoctorDB():base ("DoctorDB") { } 

       public DbSet <Doctor> Doctors { get; set; }
    }

}
