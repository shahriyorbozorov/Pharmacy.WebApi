﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Pharmacy.WebApi.Common.Exceptions;
using Pharmacy.WebApi.Common.Security;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.Interfaces.Managers;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.ViewModels.Emails;
using Pharmacy.WebApi.ViewModels.Users;
using System.Net;
using System.Net.Mail;

namespace Pharmacy.WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _dbContext;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly IAuthManager _authManager;
        private readonly IMemoryCache _cache;

        public AccountService(IGenericRepository<User> repository,
            IFileService fileService,
            IAuthManager authManager,
            IEmailService emailService,
            IMemoryCache memoryCache,
            IMapper mapper,
            AppDbContext dbContext)
        {
            _userRepository = repository;
            _fileService = fileService;
            _authManager = authManager;
            _emailService = emailService;
            _cache = memoryCache;
            _mapper = mapper;
            _dbContext = dbContext;
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

        public async Task<string?> LoginAsync(UserLoginViewModel userLoginViewModel)
        {
            var user = await _userRepository.GetAsync(a => a.Email == userLoginViewModel.Email);

            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Email not valid!");
            else
            {
                var isVerify = PasswordHasher.Verify(userLoginViewModel.Password, user.Salt, user.PasswordHash);

                if (isVerify)
                    return _authManager.GeneratedToken(user);
                else throw new StatusCodeException(HttpStatusCode.NotFound, "Password not valid!");
            }
        }

        public async Task<bool> RegistrAsync(UserCreateModel userCreateModel)
        {
            User user = _mapper.Map<User>(userCreateModel);
            var checkUser = await _userRepository.GetAsync(user => user.Email == userCreateModel.Email);

            if (checkUser is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "This email already exists");

            user.ImagePath = await _fileService.SaveImageAsync(userCreateModel.Image);
            var hasherResult = PasswordHasher.Hash(userCreateModel.Password);
            user.PasswordHash = hasherResult.Hash;
            user.Salt = hasherResult.Salt;

            await _userRepository.CreateAsync(user);
            await _dbContext.SaveChangesAsync();

            var code = GeneratedCode(); /*new Random().Next(1000, 9999).ToString();*/
            _cache.Set(userCreateModel.Email, code, TimeSpan.FromMinutes(10));

            await _emailService.SendAsync(userCreateModel.Email, code);
            return true;
        }

        private string GeneratedCode()
            => new Random().Next(1000, 9999).ToString();
    }
}
