using Pharmacy.WebApi.Models;

namespace Pharmacy.WebApi.Interfaces.Managers
{
    public interface IAuthManager
    {
        string GeneratedToken(User user);
    }
}
