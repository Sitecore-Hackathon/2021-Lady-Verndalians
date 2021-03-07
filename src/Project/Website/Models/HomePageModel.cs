using Sitecore.Data.Fields;
using System;

namespace Website.Models
{
    public class HomePageModel
    {

        public Sitecore.Data.Items.Item HomePageItem
        {
            get; private set;
        }

        public HomePageModel(Sitecore.Data.Items.Item item)
        {
            HomePageItem = item;
        }

        public ImageField HeroImage
        {
            get
            {
                return HomePageItem.Fields["Hero Image"];
            }
        }

        public string HomePageContent
        {
            get
            {
                var htmlConent = HomePageItem["Copy"];
                return htmlConent;
            }
        }


        public string HeroImageSrc
        {
            get
            {
                if (HeroImage != null && HeroImage.MediaItem != null)
                {
                    Sitecore.Data.Items.MediaItem image = new Sitecore.Data.Items.MediaItem(HeroImage.MediaItem);

                    string src = Sitecore.StringUtil.EnsurePrefix('/',

                    Sitecore.Resources.Media.MediaManager.GetMediaUrl(image));

                    return src;
                }

                return string.Empty;

            }
        }
    }
}