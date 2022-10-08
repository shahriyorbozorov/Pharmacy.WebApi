using Microsoft.Extensions.Caching.Memory;
using Pharmacy.WebApi.Common.Exceptions;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.Interfaces.Managers;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.ViewModels.Emails;
using Pharmacy.WebApi.ViewModels.Users;
using System.Net;

namespace Pharmacy.WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly IAuthManager _authManager;
        private readonly IMemoryCache _cache;

        public AccountService(IGenericRepository<User> repository,
            IFileService fileService,
            IAuthManager authManager,
            IEmailService emailService,
            IMemoryCache memoryCache)
        {
            _userRepository = repository;
            _fileService = fileService;
            _authManager = authManager;
            _emailService = emailService;
            _cache = memoryCache;
        }
        public async Task<string> EmailVerify(EmailAddress emailAddress)
        {
            var user = await _userRepository.GetAsync(a => a.Email == emailAddress.Email);

            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Email not valid!");

            if (_cache.TryGetValue(emailAddress.Email, out var exceptedCode))
            {
                if (exceptedCode.Equals(emailAddress.Code))
                    return _authManager.GeneratedToken(user);

                throw new StatusCodeException(HttpStatusCode.BadRequest, "Code is not valid.");
            }
            else
                throw new Exception("Code is expired.");
        }

        public Task<string?> LoginAsync(UserLoginViewModel userLoginViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegistrAsync(UserCreateModel userCreateModel)
        {
            throw new NotImplementedException();
        }
    }
}
