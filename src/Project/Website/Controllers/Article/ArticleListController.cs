using Feature.Articles.Models;
using Sitecore.Mvc.Controllers;
using System.Web.Mvc;

namespace Website.Controllers.Article
{
    public class ArticleListController : SitecoreController
    {
        public ActionResult Articles()
        {
            var articleList = new ArticleListModel();

            return View("~/Views/Article/ArticleList.cshtml", articleList);
        }
    }
}