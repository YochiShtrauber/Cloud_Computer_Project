using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YESS.BE;
using YESS.Models.ViewModel;

namespace YESS
{
    public class RouteConfig
    {
        #region helper User
        public static Prescriptions pre = new Prescriptions();
        public static bool isUser = false;
        public static MYUSER user = new MYUSER();
        public static int WhichUser = 0;//1:Administrator,2:Doctor,0:Nobody
        public static PatientViewModel pvm = new PatientViewModel();
        public static string helper = "";
        public static DateTime t = DateTime.Now;
        public static int helper2 = 0;
        #endregion
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
