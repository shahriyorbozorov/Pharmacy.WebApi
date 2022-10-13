using Pharmacy.WebApi.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pharmacy.WebApi.ViewModels.Users
{
    public class UserImageUpdateViewModel
    {
        [Required(ErrorMessage = "Image is required")]
        [DataType(DataType.Upload)]
        [MaxFileSize(1)]
        [AllowedFileExtensionAttribute(new string[] { ".jpg", ".png" })]
        [JsonPropertyName("image")]
        public IFormFile Image { get; set; } = null!;
    }
}
