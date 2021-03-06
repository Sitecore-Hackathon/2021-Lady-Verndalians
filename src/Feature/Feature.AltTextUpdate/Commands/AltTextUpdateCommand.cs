using Feature.AltTextUpdate.Repositories;
using Sitecore;
using Sitecore.Diagnostics;
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
                var azureAltTextRepository = new AzureAltTextRepository();
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
                            Sitecore.Context.ClientPage.ClientResponse.Alert($"Alt text returned from Azure:<br> {altText}.");
                        }                                           
                    }
                }
            }
        }


        public override CommandState QueryState(CommandContext context)
        {
            Error.AssertObject((object)context, "context");

            if (context.Items.Length == 0)
            {
                return CommandState.Disabled;
            }

            if (context.Items[0].TemplateID.ToString() != "{DC9E710B-590E-491C-8D38-64157C181BF4}")
            {
                return CommandState.Hidden;
            }

            return base.QueryState(context);
        }
    }
}
