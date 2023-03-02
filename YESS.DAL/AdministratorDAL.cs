using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YESS.BE;

namespace YESS.DAL
{
    public class AdministratorDAL
    {

        public MYUSER AdministratorAuthentication(string userName, string password)
        {
            {

                using (var ctx = new AdministratorDB())
                {
                    var administrator = ctx.Administrators.Where(s => s.UserName == userName && s.Password == password).FirstOrDefault();
                    if (administrator != null)
                    {
                        MYUSER U = new MYUSER();
                        U.FirstName = administrator.FirstName;
                        U.LasttName = administrator.LasttName;
                        U.ID_TZ = administrator.ID_TZ;
                        U.UserName = administrator.UserName;
                        U.Password = administrator.Password;
                        return U;
                    }
                    else
                        return null;
                }
            }
        }

        /// <summary>
        /// the function check if the user name is of the administrator
        /// </summary>
        /// <param name="userName"a string of >user name</param>
        /// <returns>true if the user name is of the admin else false</returns>
        public bool CheckUserNameAdministrator(string userName)
        {
            using (var ctx = new AdministratorDB())
            {
                var administrator = ctx.Administrators.Where(s => s.UserName == userName).FirstOrDefault();
                if (administrator != null)
                    return true;
                else
                    return false;
            }

        }


        // the next code we need only for the first timr - to make admin
        public void AdministratorAdd()
        {
            using (var ctx = new AdministratorDB())
            {
                Administrator admin = new Administrator();
                admin.FirstName = "Yolo";
                admin.LasttName = "Cohen";
                admin.ID_TZ = "4444";
                admin.UserName = "Yok";
                admin.Password = "9911";

                var administrator = ctx.Administrators.Add(admin);
                ctx.SaveChanges();

            }
        }
    }
}

