using Pharmacy.WebApi.Helpers;
using Pharmacy.WebApi.Interfaces;

namespace Pharmacy.WebApi.Services
{
    public class FileService : IFileService

    {
        private readonly string _basePath = string.Empty;
        private const string _folderName = "images";

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _basePath = webHostEnvironment.WebRootPath;
        }
        public async Task<string> SaveImageAsync(IFormFile image)
        {
            string fileName = ImageHelper.MakeImageName(image.FileName);
            string partPath = Path.Combine(_folderName, fileName);
            string path = Path.Combine(_basePath, partPath);

            var stream = File.Create(path);
            await image.CopyToAsync(stream);
            stream.Close();

            return partPath;
        }

        public Task<bool> DeleteImageAsync(string relativeImagePath)
        {
            string absoluteFilePath = Path.Combine(_basePath, relativeImagePath);

            if (!File.Exists(absoluteFilePath)) return Task.FromResult(false);

            try
            {
                File.Delete(absoluteFilePath);
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }
    }
}
