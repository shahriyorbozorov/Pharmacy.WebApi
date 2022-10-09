﻿using Pharmacy.WebApi.ViewModels.Emails;
using Pharmacy.WebApi.ViewModels.Users;

namespace Pharmacy.WebApi.Interfaces

{
    public interface IAccountService
    {
        Task<bool> RegistrAsync(UserCreateModel userCreateModel);
        Task<string?> LoginAsync(UserLoginViewModel userLoginViewModel);
        Task<string> EmailVerify(EmailAddress emailAddress);
    }
}