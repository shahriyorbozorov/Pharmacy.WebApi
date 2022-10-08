using Pharmacy.WebApi.Common;
using Pharmacy.WebApi.Enums;

namespace Pharmacy.WebApi.Models
{
    public class User : Auditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; } = false;
        public string PhoneNumber { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Admin;
    }
}
