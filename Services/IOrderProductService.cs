using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IOrderProductService
    {
        Task CreateOrderProductAsync(OrderProductDto orderProductDto);

        Task<List<OrderProductDto>> GetAllOrderProductsAsync();

        Task<OrderProductDto> GetOrderProductByIdAysnc(int orderId, int productId);

        Task<OrderProductDto> UpdateOrderProductAsync(int orderId, int productId, OrderProductDto orderProductDto);

        Task DeleteOrderProductAsync(int orderId, int productId);
    }
}
