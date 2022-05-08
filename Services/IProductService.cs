using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductDto productDto);

        Task<List<ProductDto>> GetAllProductsAsync();

        Task<ProductDto> GetProductByIdAysnc(int productId);

        Task<ProductDto> UpdateProductAsync(int productId, ProductDto productDto);

        Task DeleteProductAsync(int productId);
    }
}
