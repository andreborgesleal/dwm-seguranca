using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Seguranca
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("__MyAppSession", string.Empty);
        }

        public void Session_End(object sender, EventArgs e)
        {
            EmpresaSecurity login = new EmpresaSecurity();
            if (System.Web.HttpContext.Current != null)
                login.EncerrarSessao(System.Web.HttpContext.Current.Session.SessionID);
        }

        protected void Application_End()
        {
            EmpresaSecurity login = new EmpresaSecurity();
            if (System.Web.HttpContext.Current != null)
                login.EncerrarSessao(System.Web.HttpContext.Current.Session.SessionID);
        }

    }
}