namespace Pharmacy.WebApi.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile image);
        Task<bool> DeleteImageAsync(string relativeImagePath);
    }
}
