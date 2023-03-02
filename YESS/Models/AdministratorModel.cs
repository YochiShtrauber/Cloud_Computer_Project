using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YESS.BE;
using YESS.BL;

namespace YESS.Models
{
    public class AdministratorModel
    {
        public MYUSER AdministratorAuthentication(string userName, string password)
        {
            AdministratorBL administratobl = new AdministratorBL();
            return administratobl.AdministratorblAuthentication(userName, password);
        }
    }
}