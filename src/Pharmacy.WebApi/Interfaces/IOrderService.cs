using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.ViewModels.Orders;
using System.Linq.Expressions;

namespace Pharmacy.WebApi.Interfaces
{
    public interface IOrderService
    {
        Task<OrderViewModel> CreateAsync(OrderCreateModel orderCreate);

        Task<bool> UpdateAsync(long id, OrderCreateModel orderCreate);

        Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression);

        Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> expression);

        Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null,
            PaginationParams? @params = null);
    }
}
