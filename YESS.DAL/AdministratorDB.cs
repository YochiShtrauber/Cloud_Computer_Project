using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;

namespace YESS.DAL
{
    public class AdministratorDB : DbContext

    {
        public AdministratorDB() : base("AdministratorDB") { }

        public DbSet<Administrator> Administrators { get; set; }
    }
}
