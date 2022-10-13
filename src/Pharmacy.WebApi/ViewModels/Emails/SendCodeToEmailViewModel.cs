using Pharmacy.WebApi.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pharmacy.WebApi.ViewModels.Emails
{
    public class SendCodeToEmailViewModel
    {

        [Required, Email]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
    }
}
