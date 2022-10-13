using Pharmacy.WebApi.ViewModels.Emails;

namespace Pharmacy.WebApi.Interfaces
{
    public interface IEmailService
    {
        public Task SendAsync(EmailMessageViewModel emailMessage);
    }
}
