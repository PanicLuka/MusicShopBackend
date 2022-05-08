using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IBrandService
    {
        Task CreateBrandAsync(BrandDto brandDto);

        Task<List<BrandDto>> GetAllBrandsAsync();

        Task<BrandDto> GetBrandByIdAysnc(int brandId);

        Task<BrandDto> UpdateBrandAsync(int brandId, BrandDto brandDto);

        Task DeleteBrandAsync(int brandId);

    }
}
