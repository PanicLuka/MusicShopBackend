using Microsoft.AspNetCore.Mvc;
using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IBrandService
    {
        Task CreateBrandAsync(BrandDto brandDto);

        Task<PagedList<BrandDto>> GetAllBrandsAsync(BrandParameters parameters);

        Task<int> GetBrandIdByBrandName(string brandName);

        //Task<PagedList<BrandDto>> GetBrandsByName(string brandName);
        Task<BrandDto> GetBrandByIdAysnc(int brandId);

        Task<BrandDto> UpdateBrandAsync(int brandId, BrandDto brandDto);

        Task DeleteBrandAsync(int brandId);

    }
}
