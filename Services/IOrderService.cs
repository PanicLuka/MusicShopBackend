using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDto orderDto);

        Task<List<OrderDto>> GetAllOrdersAsync();

        Task<OrderDto> GetOrderByIdAysnc(int orderId);

        Task<OrderDto> UpdateOrderAsync(int orderId, OrderDto orderDto);

        Task DeleteOrderAsync(int orderId);
    }
}
