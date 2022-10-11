using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pharmacy.WebApi.Common.Exceptions;
using Pharmacy.WebApi.Common.Extensions;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.Repositories;
using Pharmacy.WebApi.ViewModels.Drugs;
using Pharmacy.WebApi.ViewModels.Orders;
using System.Linq.Expressions;
using System.Net;

namespace Pharmacy.WebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, AppDbContext dbContext, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderViewModel> CreateAsync(OrderCreateModel orderCreate)
        {
            var entity = _mapper.Map<Order>(orderCreate);
            var order = await _orderRepository.CreateAsync(entity);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<OrderViewModel>(entity);
        }

        public async Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var orders = _orderRepository.GetAll(expression);

            if (!orders.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found Drud!");
            _orderRepository.DeleteRange(orders);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null, 
            PaginationParams? @params = null)
        {
            var orders = _orderRepository.GetAll(expression).ToPagedAsEnumerable(@params);
            var orderViews = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                var ord = _mapper.Map<OrderViewModel>(order);
                orderViews.Add(ord);
            }
            return orderViews;
        }

        public async Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var order = await _orderRepository.GetAsync(expression);
            if (order is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found Drug !");
            var orderView = _mapper.Map<OrderViewModel>(order);

            return orderView;
        }

        public Task<bool> UpdateAsync(long id, OrderCreateModel orderCreate)
        {
            throw new NotImplementedException();
        }
    }
}
