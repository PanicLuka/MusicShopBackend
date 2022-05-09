using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDto orderDto);

        Task<PagedList<OrderDto>> GetAllOrdersAsync(OrderParameters parameters);

        Task<OrderDto> GetOrderByIdAysnc(int orderId);

        Task<OrderDto> UpdateOrderAsync(int orderId, OrderDto orderDto);

        Task DeleteOrderAsync(int orderId);
    }
}
