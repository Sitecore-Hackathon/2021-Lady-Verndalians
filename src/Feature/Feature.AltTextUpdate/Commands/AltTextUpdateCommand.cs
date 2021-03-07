using Feature.AltTextUpdate.Repositories;
using Sitecore;
using Sitecore.SecurityModel;
using Sitecore.Shell.Framework.Commands;
using System.Collections.Specialized;
using System.Configuration;
using System.Web;

namespace Feature.AltTextUpdate.Commands
{
    public class AltTextUpdateCommand : Command
    {
        public override void Execute(CommandContext context)
        {
            var sitecoreItem = context.Items[0];

            if(sitecoreItem != null)
            {
                var mediaItem = (Sitecore.Data.Items.MediaItem)sitecoreItem;               

                var imageStream = mediaItem.GetMediaStream();
                IAzureAltTextRepository azureAltTextRepository = new AzureAltTextRepository();
                var altTextResult = azureAltTextRepository.GetImageDescription(imageStream, mediaItem.InnerItem.Language.Name);

                var altText = altTextResult.Description != null ? altTextResult.Description : string.Empty;

                if (!string.IsNullOrEmpty(altText))
                {
                    using (new SecurityDisabler())
                    {
                        mediaItem.BeginEdit();
                        var key = ConfigurationManager.AppSettings["croudSourceFieldAzureKey"];
                        var croudSourcedAltText = new NameValueCollection();
                        croudSourcedAltText.Add(key, HttpUtility.UrlEncode(altText));
                        try
                        {
                            mediaItem.InnerItem["CrowdSourced Alt Text"] = StringUtil.NameValuesToString(croudSourcedAltText, "&");
                        }
                        finally
                        {
                            mediaItem.EndEdit();
                            Context.ClientPage.ClientResponse.Alert($"Alt text returned from Azure:<br> {altText}.");
                        }                                           
                    }
                }
            }
        }
    }
}
