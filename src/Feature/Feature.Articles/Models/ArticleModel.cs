using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;
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

        public string ArticleLink
        {
            get
            {
                return LinkManager.GetItemUrl(ArticleItem);
            }
        }
    }
}
