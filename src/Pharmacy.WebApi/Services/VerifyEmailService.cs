using Microsoft.Extensions.Caching.Memory;
using Pharmacy.WebApi.Common.Exceptions;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.ViewModels.Emails;
using Pharmacy.WebApi.ViewModels.Users;
using System.Net;

namespace Pharmacy.WebApi.Services
{
    public class VerifyEmailService : IVerifyEmailService
    {
        private readonly IMemoryCache _cache;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _repository;
        private readonly AppDbContext _dbContext;

        public VerifyEmailService(IMemoryCache cache, IEmailService email,
                                  IUserRepository repository, AppDbContext appContext)
        {
            _cache = cache;
            _emailService = email;
            _repository = repository;
            _dbContext = appContext;
        }

        public async Task SendCodeAsync(SendCodeToEmailViewModel email)
        {
            int code = new Random().Next(1000, 9999);

            _cache.Set(email.Email, code, TimeSpan.FromMinutes(2));

            var message = new EmailMessageViewModel()
            {
                To = email.Email,
                Subject = "Verification code",
                Body = code,
            };

            await _emailService.SendAsync(message);
        }

        public async Task<bool> VerifyEmail(EmailVerifyViewModel emailVerify)
        {
            var entity = await _repository.GetAsync(user => user.Email == emailVerify.Email);

            if (entity == null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "User not found!");

            if (_cache.TryGetValue(emailVerify.Email, out int exceptedCode))
            {
                if (exceptedCode != emailVerify.Code)
                    throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Code is wrong!");

                entity.EmailConfirmed = true;

                await _repository.UpdateAsync(entity);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            else
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Code is expired");
        }

        public Task<bool> VerifyPasswordAsync(UserResetPasswordViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
