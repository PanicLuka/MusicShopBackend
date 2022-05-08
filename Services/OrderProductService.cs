using Microsoft.EntityFrameworkCore;
using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace MusicShopBackend.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly DataContext _context;

        public async Task CreateOrderProductAsync(OrderProductDto orderProductDto)
        {
            OrderProduct orderProductEntity = orderProductDto.OrderProductDtoToOrderProduct();

            await _context.AddAsync(orderProductEntity);

            await SaveChangesAsync();
        }


        public async Task DeleteOrderProductAsync(int orderId, int productId)
        {
            var orderProduct = await GetOrderProductByIdAysnc(orderId, productId);

            if (orderProduct == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(orderProduct);
            await SaveChangesAsync();
        }

        public async Task<List<OrderProductDto>> GetAllOrderProductsAsync()
        {
            var orderProducts = await _context.OrderProducts.ToListAsync();
            if (orderProducts == null || orderProducts.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<OrderProductDto> orderProductDtos = new List<OrderProductDto>();

            foreach (var orderProduct in orderProducts)
            {
                OrderProductDto orderProductDto = orderProduct.OrderProductToDto();
                orderProductDtos.Add(orderProductDto);
            }

            return orderProductDtos;

        }

        public async Task<OrderProductDto> GetOrderProductByIdAysnc(int orderId, int productId)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(e => e.OrderId == orderId && e.ProductId == productId);
            if (orderProduct == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var orderProductDto = orderProduct.OrderProductToDto();
            return orderProductDto;
        }

        public async Task<OrderProductDto> UpdateOrderProductAsync(int orderId, int productId, OrderProductDto orderProductDto)
        {
            var oldOrderProductDto = await GetOrderProductByIdHelperAsync(orderId, productId);
            if (oldOrderProductDto == null)
            {
                await CreateOrderProductAsync(orderProductDto);
                return oldOrderProductDto.OrderProductToDto();
            }
            else
            {
                OrderProduct orderProduct = orderProductDto.OrderProductDtoToOrderProduct();
                oldOrderProductDto.ProductId = orderProduct.ProductId;
                oldOrderProductDto.OrderId = orderProduct.OrderId;
                oldOrderProductDto.OrderQuantity = orderProduct.OrderQuantity;
               

                await SaveChangesAsync();
                if (oldOrderProductDto.OrderProductToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldOrderProductDto.OrderProductToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<OrderProduct> GetOrderProductByIdHelperAsync(int orderId, int productId)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(e => e.OrderId == orderId && e.ProductId == productId);

            if (orderProduct == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return orderProduct;
        }
    }
}
