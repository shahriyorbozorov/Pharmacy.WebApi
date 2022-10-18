﻿namespace Pharmacy.WebApi.Interfaces
{
    public interface IFileService
    {
        public string ImageFolderName { get; }
        Task<string> SaveImageAsync(IFormFile image);
        Task<bool> DeleteImageAsync(string relativeImagePath);
    }
}
