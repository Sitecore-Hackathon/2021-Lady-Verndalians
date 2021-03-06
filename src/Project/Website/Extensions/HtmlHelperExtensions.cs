using Feature.Articles.Models;
using Sitecore.Data.Fields;
using System.Web;
using System.Web.Mvc;
using Sitecore.Data.Items;

namespace Website.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString HumanizedImage(this HtmlHelper<ArticleModel> helper, ImageField imageField)
        {

            if (imageField?.MediaItem == null) return new HtmlString("");

            var src = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(imageField.MediaItem));

            var alt =  $" alt={imageField.Alt}";

            var regularItem = Sitecore.Context.Item.Database.GetItem(imageField.MediaID);

            var crowdsourcedtext = regularItem.Fields["CrowdSourced Alt Text"]?.Value;

            var img = $"<img style='max-width:980px' src='{src}' {alt} />";

            if (string.IsNullOrEmpty(crowdsourcedtext))
                return new HtmlString(img);
            
            var wrapper = $"<div style='display:block; max-width:980px'>{img}<a href =\"#ex1\" rel=\"modal:open\" style=\"float:right\">Humanize</a></div>";

            return new HtmlString(wrapper);
        }
    }
}