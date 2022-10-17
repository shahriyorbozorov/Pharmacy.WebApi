using Pharmacy.WebApi.Common.Attributes;
using Pharmacy.WebApi.Enums;
using System.ComponentModel.DataAnnotations;
using MaxFileSizeAttribute = Pharmacy.WebApi.Common.Attributes.MaxFileSizeAttribute;

namespace Pharmacy.WebApi.ViewModels.Users
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50), MinLength(2)]
        [RegularExpression(@"^(?=.{1,40}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$",
                    ErrorMessage = "Please enter valid first name. " +
                    "First name must be contains only letters or ' character")]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(50), MinLength(2)]
        [RegularExpression(@"^(?=.{1,40}$)[a-zA-Z]+(?:[-'\s][a-zA-Z]+)*$",
            ErrorMessage = "Please enter valid last name. " +
            "Last name must be contains only letters or ' character")]
        public string LastName { get; set; } = string.Empty;


        //[Required(ErrorMessage = "Image is required")]
        //[DataType(DataType.Upload)]
       // [MaxFileSize(3)]
        //[AllowedFileExtension(new string[] { ".jpg", ".png", ".ico" })]
        public IFormFile Image { get; set; } = null!;


        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please enter valid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StrongPassword]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$",
            ErrorMessage = "Please enter valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public Role Role { get; set; }
    }
}
