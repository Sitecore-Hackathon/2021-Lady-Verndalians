using Feature.Articles.Models;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Website.Controllers.Article
{
    public class ArticleController : SitecoreController
    {
        public ActionResult Article()
        {
            var articleModel = new ArticleModel(Sitecore.Context.Item);
            return View("~/Views/Article/ArticlePage.cshtml", articleModel);
        }
    }
}