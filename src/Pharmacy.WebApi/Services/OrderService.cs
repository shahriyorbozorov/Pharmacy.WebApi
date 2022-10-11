using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.ViewModels.Orders;
using System.Linq.Expressions;

namespace Pharmacy.WebApi.Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderViewModel> CreateAsync(OrderCreateModel orderCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null, PaginationParams? parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(long id, OrderCreateModel orderCreate)
        {
            throw new NotImplementedException();
        }
    }
}
