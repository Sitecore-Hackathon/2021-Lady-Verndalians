using Sitecore.Mvc.Controllers;
using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers.HomePage
{
    public class HomeController : SitecoreController
    {
        public ActionResult HomePage()
        {
            var articleModel = new HomePageModel(Sitecore.Context.Item);
            return View("~/Views/HomePage/HomePage.cshtml", articleModel);
        }
    }
}