using System.Web.Mvc;
using System.Web.Routing;
using Website.App_Start;

namespace Website
{
    public class Global : Sitecore.Web.Application
    {

        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }       
    }
}