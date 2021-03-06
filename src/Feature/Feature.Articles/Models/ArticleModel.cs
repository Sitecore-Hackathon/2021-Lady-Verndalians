using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;

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
                return ArticleItem.Fields["Article Content"].Value;
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
