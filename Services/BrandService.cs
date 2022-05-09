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
    public class BrandService : IBrandService
    {
        private readonly DataContext _context;
        private readonly BrandValidator _validator;
        private static int _count;
        public BrandService(DataContext context, BrandValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int GetBrandsCountAsync()
        {
            return _count;
        }
        public async Task CreateBrandAsync(BrandDto brandDto)
        {

            _validator.ValidateAndThrow(brandDto);

            Brand brandEntity = brandDto.BrandDtoToBrand();


            await _context.AddAsync(brandEntity);

            await SaveChangesAsync();


        }


        public async Task DeleteBrandAsync(int brandId)
        {
            var brand = await GetBrandByIdHelperAsync(brandId);

            if (brand == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(brand);
            await SaveChangesAsync();
        }

        public async Task<PagedList<BrandDto>> GetAllBrandsAsync(BrandParameters parameters)
        {
            var brands = await _context.Brands.ToListAsync();
            _count = brands.Count;

            if (brands == null || brands.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }
            List<BrandDto> brandDtos = new List<BrandDto>();

            foreach (var brand in brands)
            {
                BrandDto brandDto = brand.BrandToDto();
                brandDtos.Add(brandDto);
            }

            IQueryable<BrandDto> queryable = brandDtos.AsQueryable();

            return PagedList<BrandDto>.ToPagedList(queryable, parameters._pageNumber, parameters.PageSize);

        }
        //public async Task<PagedList<BrandDto>> GetBrandsByName(string brandName)
        //{
        //    var brand = await _context.Brands.FirstOrDefaultAsync(e => e.BrandName == brandName);
        //    if (brand == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    }
        //    var brandDto = brand.BrandToDto();
        //    return brandDto;
        //}
        public async Task<BrandDto> GetBrandByIdAysnc(int brandId)
        {

            var brand = await _context.Brands.FirstOrDefaultAsync(e => e.BrandId == brandId);
            if (brand == null)
            {

                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var brandDto = brand.BrandToDto();
            return brandDto;



        }

        public async Task<BrandDto> UpdateBrandAsync(int brandId, BrandDto brandDto)
        {
            var oldBrandDto = await GetBrandByIdHelperAsync(brandId);
            if (oldBrandDto == null)
            {
                await CreateBrandAsync(brandDto);
                return oldBrandDto.BrandToDto();
            }
            else
            {
                Brand brand = brandDto.BrandDtoToBrand();
                oldBrandDto.BrandName = brand.BrandName;

                await SaveChangesAsync();
                if (oldBrandDto.BrandToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldBrandDto.BrandToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<Brand> GetBrandByIdHelperAsync(int brandId)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(e => e.BrandId == brandId);

            if (brand == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return brand;
        }


    }
}
