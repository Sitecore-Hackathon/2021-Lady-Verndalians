using Feature.AltTextUpdate.Extensions;
using Sitecore.SecurityModel;
using Sitecore.Shell.Framework.Commands;


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
                var text = MediaItemExtensions.GetAltTextFromAI(mediaItem);

                using (new SecurityDisabler())
                {

                    using (new Sitecore.Data.Items.EditContext(mediaItem))
                    {
                        mediaItem.InnerItem.Fields["Alt"].Value = text;
                        Sitecore.Context.ClientPage.ClientResponse.Alert($"Alt text returned from Azure:<br> {text}.");
                    }
                }
            }
        }
    }
}
