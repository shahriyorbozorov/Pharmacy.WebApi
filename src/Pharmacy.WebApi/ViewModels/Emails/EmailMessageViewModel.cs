using System.Text.Json.Serialization;

namespace Pharmacy.WebApi.ViewModels.Emails
{
    public class EmailMessageViewModel
    {
        [JsonPropertyName("to")]
        public string To { get; set; } = string.Empty;

        [JsonPropertyName("body")]
        public int Body { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = string.Empty;
    }
}
