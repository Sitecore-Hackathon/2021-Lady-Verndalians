using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Mvc.Controllers;
using Sitecore.Mvc.Extensions;
using Sitecore.SecurityModel;

namespace Website.Controllers.Ajax
{
    public class AjaxController : SitecoreController
    {

        [System.Web.Http.HttpPost]
        public JsonResult AddCrowdSourceChoice(string option, string id)
        {
            if (id == null) return Json(null);

            var database = Factory.GetDatabase("master");

            var item = database.GetItem(new ID(id));

            var mediaItem = (Sitecore.Data.Items.MediaItem)item;

            if (mediaItem == null) return Json(null);

            if (string.IsNullOrEmpty(option)) return Json(null);

            var rand = new Random();

            var randNumber = rand.Next(99999);

            var crowdSourcedAltText = new NameValueCollection { { $"Human{randNumber}", HttpUtility.UrlEncode(option) } };

            var currentValue = mediaItem.InnerItem["CrowdSourced Alt Text"];

            var newValue = $"{currentValue}&{StringUtil.NameValuesToString(crowdSourcedAltText, "&")}";

            using (new SecurityDisabler())
            {
                mediaItem.BeginEdit();
                
                try
                {
                    mediaItem.InnerItem["CrowdSourced Alt Text"] = newValue;
                }
                finally
                {
                    mediaItem.EndEdit();
                }
            }

            return Json("success");
        }
    }
}
