using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
using System;
using System.Web;

namespace Feature.Articles.Models
{
    public class ArticleModel
    {
        public Item ArticleItem
        {
            get; private set;
        }

        public ArticleModel(Item item)
        {
            ArticleItem = item;
        }

        public ImageField HeroImage
        {
            get
            {
                return ArticleItem.Fields["Hero Image"];
            }
        } 

        public string ArticleContent
        {
            get
            {
                var htmlConent = ArticleItem["Article Content"];
                return htmlConent;
            }
        }

        public string Author
        {
            get
            {
                if(!string.IsNullOrEmpty(ArticleItem["Author"]))
                {
                    return ArticleItem["Author"];
                }

                return string.Empty;
                
            }
        }

        public string Date
        {
            get
            {
                DateField dateTimeField = ArticleItem.Fields["Article Date"];
                string dateTimeString = dateTimeField.Value;

                DateTime dateTimeStruct = Sitecore.DateUtil.IsoDateToDateTime(dateTimeString);

                return dateTimeStruct.ToShortDateString();

            }
        }

        public string ArticleLink
        {
            get
            {
                return LinkManager.GetItemUrl(ArticleItem);
            }
        }
    }
}
