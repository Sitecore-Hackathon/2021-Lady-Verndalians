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
            var mediaItem = imageField.MediaItem;
            
            var src = Sitecore.Resources.Media.MediaManager.GetMediaUrl(mediaItem);

            var alt =  $" alt={imageField.Alt}";

            var crowdsourcedtext = mediaItem.Fields["CrowdSourced Alt Text"]?.Value;

            var img = $"<img src='{src}' {alt} />";

            if (string.IsNullOrEmpty(crowdsourcedtext))
                return new HtmlString(img);
            
            var wrapper = $"<div>{img}<a href =\"#ex1\" rel=\"modal:open\" style=\"float:right\">Humanize</a></ div>";

            return new HtmlString(wrapper);
        }
    }
}