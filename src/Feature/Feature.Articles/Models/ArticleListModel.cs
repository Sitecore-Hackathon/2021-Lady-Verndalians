using Sitecore.Data.Items;
using System;
using System.Collections.Generic;

namespace Feature.Articles.Models
{
    public class ArticleListModel
    {
        public const string ArticlesFolderId = "{A9197317-53E1-4CEA-A615-D4D05ECFC501}";        

        public List<ArticleModel> Articles
        {
            get
            {
                var articles = new List<ArticleModel>();
                var articleFolderItem = Sitecore.Context.Database.GetItem(ArticlesFolderId);
                if (articleFolderItem != null && articleFolderItem.Children.Count > 0)
                {
                    foreach (Item item in articleFolderItem.Children)
                    {
                        var article = new ArticleModel(item);
                        articles.Add(article);
                    }
                }

                return articles;
            }
        }
    }
}
