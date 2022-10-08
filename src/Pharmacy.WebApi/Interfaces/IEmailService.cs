namespace Pharmacy.WebApi.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(string email, string message);
    }
}
