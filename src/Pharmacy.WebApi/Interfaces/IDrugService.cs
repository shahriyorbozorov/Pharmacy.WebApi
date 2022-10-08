using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.ViewModels.Drugs;
using System.Linq.Expressions;

namespace Pharmacy.WebApi.Interfaces
{
    public interface IDrugService
    {
        Task<DrugViewModel> CreateAsync(DrugCreateModel drugCreate);

        Task<bool> UpdateAsync(long id, DrugCreateModel drugCreate);

        Task<bool> DeleteAsync(Expression<Func<Drug, bool>> expression);

        Task<DrugViewModel?> GetAsync(Expression<Func<Drug, bool>> expression);
        Task<IEnumerable<DrugViewModel>> GetAllAsync(PaginationParams @params,
            Expression<Func<Drug, bool>>? expression = null);

    }
}
