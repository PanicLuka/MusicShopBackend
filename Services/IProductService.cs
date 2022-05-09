using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductDto productDto);

        Task<PagedList<ProductDto>> GetAllProductsAsync(ProductParameters parameters);

        Task<ProductDto> GetProductByIdAysnc(int productId);

        Task<ProductDto> UpdateProductAsync(int productId, ProductDto productDto);

        Task DeleteProductAsync(int productId);
    }
}
