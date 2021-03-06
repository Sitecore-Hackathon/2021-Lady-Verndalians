using Feature.Articles.Models;
using Sitecore.Data.Fields;
using System.Web;
using System.Web.Mvc;

namespace Website.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString HumanizedImage(this HtmlHelper<ArticleModel> helper, ImageField imageField)
        {

            if (imageField == null || imageField.MediaItem == null) return new HtmlString("");

            Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(imageField.MediaItem);

            string src = Sitecore.StringUtil.EnsurePrefix('/', Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));

            var alt =  $" alt={imageField.Alt}";

            var crowdsourcedtext = image.InnerItem.Fields["CrowdSourced Alt Text"]?.Value;

            var img = $"<img src='{src}' {alt} />";

            if (string.IsNullOrEmpty(crowdsourcedtext))
                return new HtmlString(img);
            
            var wrapper = $"<div>{img}<a href =\"#ex1\" rel=\"modal:open\" style=\"float:right\">Humanize</a></ div>";

            return new HtmlString(wrapper);
        }
    }
}