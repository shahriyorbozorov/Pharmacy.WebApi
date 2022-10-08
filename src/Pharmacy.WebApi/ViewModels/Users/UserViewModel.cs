using Pharmacy.WebApi.Enums;

namespace Pharmacy.WebApi.ViewModels.Users
{
    public class UserViewModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; } = false;
        public string PhoneNumber { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
