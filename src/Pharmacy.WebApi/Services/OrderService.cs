using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pharmacy.WebApi.Common.Exceptions;
using Pharmacy.WebApi.Common.Extensions;
using Pharmacy.WebApi.Common.Utils;
using Pharmacy.WebApi.DbContexts;
using Pharmacy.WebApi.Interfaces;
using Pharmacy.WebApi.IRepositories;
using Pharmacy.WebApi.Models;
using Pharmacy.WebApi.Repositories;
using Pharmacy.WebApi.ViewModels.Orders;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography;

namespace Pharmacy.WebApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDrugRepository _drugRepository;
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, AppDbContext dbContext, IMapper mapper, IDrugRepository drugRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _dbContext = dbContext;
            _mapper = mapper;
            _drugRepository = drugRepository;
            _userRepository = userRepository;
        }

        public async Task<OrderViewModel> CreateAsync(long userId, OrderCreateModel orderCreate)
        {
            var entity = _mapper.Map<Order>(orderCreate);
            entity.UserId = userId;
            var order = await _orderRepository.CreateAsync(entity);
            await _dbContext.SaveChangesAsync();

            var orderView = _mapper.Map<OrderViewModel>(order);
            orderView.DrugName = (await _drugRepository.GetAsync(p => p.Id == order.DrugId))!.Name;
            var user = (await _userRepository.GetAsync(p => p.Id == order.UserId))!;
            orderView.UserFullName = user.FirstName + " " + user.LastName;

            return orderView;
        }

        public async Task<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var orders = _orderRepository.GetAll(expression);

            if (!orders.Any())
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found order!");
            _orderRepository.DeleteRange(orders);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync(Expression<Func<Order, bool>>? expression = null,
            PaginationParams? @params = null)
        {
            var orders = _orderRepository.GetAll(expression).Include(p => p.User).Include(p => p.Drug).ToPagedAsEnumerable(@params);
            var orderViews = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                /*var ord = _mapper.Map<OrderViewModel>(order);
                ord.DrugName = (await _drugRepository.GetAsync(p => p.Id == order.DrugId))!.Name;
                var user = (await _userRepository.GetAsync(p => p.Id == order.UserId))!;
                ord.UserFullName = user.FirstName + " " + user.LastName;

                orderViews.Add(ord);*/

                var item = _mapper.Map<OrderViewModel>(order);
                orderViews.Add(item);
            }
            return orderViews;
        }

        public async Task<OrderViewModel?> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var order = await _orderRepository.GetAsync(expression);
            if (order is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found Order !");
            var orderView = _mapper.Map<OrderViewModel>(order);

            orderView.DrugName = (await _drugRepository.GetAsync(p => p.Id == order.DrugId))!.Name;
            var user = (await _userRepository.GetAsync(p => p.Id == order.UserId))!;
            orderView.UserFullName = user.FirstName + " " + user.LastName;

            return orderView;
        }

        public async Task<bool> UpdateAsync(long id, OrderCreateModel orderCreate)
        {
            var order = await _orderRepository.GetAsync(d => d.Id == id);

            if (order is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Not Found order!");
            var orderMap = _mapper.Map<Order>(orderCreate);

            orderMap.Id = order.Id;
            orderMap.UpdateDate = DateTime.UtcNow;
            orderMap.CreateDate = order.CreateDate;
            orderMap.PaymentType = order.PaymentType;

            await _orderRepository.UpdateAsync(orderMap);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
