using AutoMapper;
using Pharmacy.WebApi.Common.Exceptions;
using Pharmacy.WebApi.Common.Extensions;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.Repositories;
using Pharmacy.WebApi.ViewModels.Drugs;
using System.Linq.Expressions;
using System.Net;

namespace Pharmacy.WebApi.Services
{
    public class DrugService : IDrugService
    {
        private readonly IDrugRepository _drugRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public DrugService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _drugRepository = new DrugRepository(_dbContext);
            _mapper = mapper;
        }
        public async Task<DrugViewModel> CreateAsync(DrugCreateModel drugCreate)
        {
            var entity = _mapper.Map<Drug>(drugCreate);
            entity.CreateDate = DateTime.UtcNow;

            var drug = await _drugRepository.CreateAsync(entity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<DrugViewModel>(entity);
        }

        public async Task<bool> DeleteAsync(Expression<Func<Drug, bool>> expression)
        {
            var drugs = _drugRepository.GetAll(expression);

            if (!drugs.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found Drud!");
            _drugRepository.DeleteRange(drugs);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<DrugViewModel>> GetAllAsync(PaginationParams @params,
            Expression<Func<Drug, bool>>? expression = null)
        {


            var drugs = _drugRepository.GetAll(expression).ToPagedAsEnumerable(@params);
            var drugViews = new List<DrugViewModel>();

            foreach (var drug in drugs)
            {
                var drg = _mapper.Map<DrugViewModel>(drug);
                drugViews.Add(drg);
            }
            return drugViews;
        }

        public async Task<DrugViewModel?> GetAsync(Expression<Func<Drug, bool>> expression)
        {
            var drug = await _drugRepository.GetAsync(expression);
            if (drug is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found Drug !");
            var drugView = _mapper.Map<DrugViewModel>(drug);

            return drugView;
        }

        public async Task<bool> UpdateAsync(long id, DrugCreateModel drugCreate)
        {
            var drug = await _drugRepository.GetAsync(d => d.Id == id);

            if (drug is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found Drug!");
            var drugMap = _mapper.Map<Drug>(drugCreate);

            drugMap.Id = drug.Id;
            drugMap.UpdateDate = DateTime.UtcNow;
            drugMap.CreateDate = drug.CreateDate;

            await _drugRepository.UpdateAsync(drugMap);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
