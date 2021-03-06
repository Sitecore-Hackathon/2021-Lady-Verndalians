using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Articles.Models
{
    public class ArticleListModel
    {
        public ArticleListModel()
        {
            Articles = new List<ArticleModel>();
        }

        public List<ArticleModel> Articles { get; set; }

    }
}
