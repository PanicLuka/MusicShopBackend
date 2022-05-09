using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using MusicShopBackend.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace MusicShopBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly OrderValidator _validator;
        private static int _count;

        public OrderService(DataContext context, OrderValidator validator)
        {
            _context = context;
            _validator = validator;
        }
        public int GetOrdersCount()
        {
            return _count;
        }

        public async Task CreateOrderAsync(OrderDto orderDto)
        {
            _validator.ValidateAndThrow(orderDto);

            Order orderEntity = orderDto.OrderDtoToOrder();

            await _context.AddAsync(orderEntity);

            await SaveChangesAsync();
        }


        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await GetOrderByIdHelperAsync(orderId);

            if (order == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(order);
            await SaveChangesAsync();
        }

        public async Task<PagedList<OrderDto>> GetAllOrdersAsync(OrderParameters parameters)
        {
            var orders = await _context.Orders.ToListAsync();

            _count = orders.Count;

            if (orders == null || orders.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<OrderDto> orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                OrderDto orderDto = order.OrderToDto();
                orderDtos.Add(orderDto);
            }

            IQueryable<OrderDto> queryable = orderDtos.AsQueryable();

            return PagedList<OrderDto>.ToPagedList(queryable, parameters._pageNumber, parameters.PageSize);
        }

        public async Task<OrderDto> GetOrderByIdAysnc(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(e => e.OrderId == orderId);
            if (order == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var orderDto = order.OrderToDto();
            return orderDto;
        }

        public async Task<OrderDto> UpdateOrderAsync(int orderId, OrderDto orderDto)
        {
            var oldOrderDto = await GetOrderByIdHelperAsync(orderId);
            if (oldOrderDto == null)
            {
                await CreateOrderAsync(orderDto);
                return oldOrderDto.OrderToDto();
            }
            else
            {
                Order order = orderDto.OrderDtoToOrder();
                oldOrderDto.OrderArrival = order.OrderArrival;
                oldOrderDto.PaymentType = order.PaymentType;
                oldOrderDto.OrderStatus = order.OrderStatus;
                oldOrderDto.UserId = order.UserId;
                oldOrderDto.CreditCardId = order.CreditCardId;
                oldOrderDto.DestinationAddressId = order.DestinationAddressId;

                await SaveChangesAsync();
                if (oldOrderDto.OrderToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldOrderDto.OrderToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<Order> GetOrderByIdHelperAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(e => e.OrderId == orderId);

            if (order == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return order;
        }
    }
}
