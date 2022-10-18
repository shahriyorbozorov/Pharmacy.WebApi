using AutoMapper;
using Pharmacy.WebApi.Common.Exceptions;
using Pharmacy.WebApi.Common.Extensions;
using Pharmacy.WebApi.Common.Security;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Repositories;
using Pharmacy.WebApi.ViewModels.Users;
using System.Linq.Expressions;
using System.Net;

namespace Pharmacy.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UserService(IUserRepository userRepository, AppDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Models.User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            //await _fileService.DeleteImageAsync(user.ImagePath);
            await _userRepository.DeleteAsync(user.Id);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<Models.User, bool>>? expression = null,
            PaginationParams? @params = null)
        {
            var users = _userRepository.GetAll(expression).ToPagedAsEnumerable(@params);
            var userViews = new List<UserViewModel>();

            foreach (var user in users)
            {
                var usr = _mapper.Map<UserViewModel>(user);
                usr.ImageUrl = "https://pharmacy-db-demo.herokuapp.com//" + user.ImagePath;
                userViews.Add(usr);
            }
            return userViews;
        }

        public async Task<UserViewModel?> GetAsync(Expression<Func<Models.User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);
            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found User !");

            var userView = _mapper.Map<UserViewModel>(user);
            userView.ImageUrl = "https://pharmacy-db-demo.herokuapp.com//" + user.ImagePath;
            return userView;
        }

        public async Task<bool> ImageUpdate(long id, UserImageUpdateViewModel model)
        {
            var user = await _userRepository.GetAsync(o => o.Id == id);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "User not found");

            if (model.Image is not null)
                user.ImagePath = await _fileService.SaveImageAsync(model.Image);

            return true;
        }

        public async Task<bool> UpdateAsync(long id, UserCreateModel userCreate)
        {

            var entity = await _userRepository.GetAsync(d => d.Id == id);

            if (entity is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            await _fileService.DeleteImageAsync(entity.ImagePath);
            var imagePath = await _fileService.SaveImageAsync(userCreate.Image);
            var res = PasswordHasher.Hash(userCreate.Password);
            var hash = res.Hash;
            var salt = res.Salt;

            var userMap = _mapper.Map<Models.User>(userCreate);

            userMap.Id = entity.Id;
            userMap.Salt = entity.Salt;
            userMap.ImagePath = imagePath;
            userMap.PasswordHash = hash;
            userMap.UpdateDate = DateTime.UtcNow;
            userMap.CreateDate = entity.CreateDate;

            await _userRepository.UpdateAsync(userMap);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
