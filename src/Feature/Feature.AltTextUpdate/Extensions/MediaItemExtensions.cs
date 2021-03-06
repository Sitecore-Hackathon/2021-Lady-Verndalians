using Feature.AltTextUpdate.Repositories;
using Sitecore.Data.Items;


namespace Feature.AltTextUpdate.Extensions
{
    public class MediaItemExtensions
    {        
        public static string GetAltTextFromAI(MediaItem mediaItem)
        {
            var imageStream = mediaItem.GetMediaStream();

            // Get the connection details
            var azureAltTextRepository = new AzureAltTextRepository();
            var altTextResult = azureAltTextRepository.GetImageDescription(imageStream, mediaItem.InnerItem.Language.Name);

            return altTextResult.Description != null ? altTextResult.Description : string.Empty;
            
            //return "test-AltText";
        }
    }
}
