using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string HeroImage
        {
            get
            {
                return ArticleItem.Fields["Hero Image"].Value;
            }
        } 

        public string ArticleContent
        {
            get
            {
                return ArticleItem.Fields["Article Content"].Value;
            }
        }
    }
}
