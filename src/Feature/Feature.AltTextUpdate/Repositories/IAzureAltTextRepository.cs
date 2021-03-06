using Feature.AltTextUpdate.Models;


namespace Feature.AltTextUpdate.Repositories
{
    public interface IAzureAltTextRepository
    {
        AltTextResult GetImageDescription(System.IO.Stream image, string descriptionLanguage);
    }
}
