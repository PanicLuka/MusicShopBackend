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
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly CategoryValidator _validator;
        private static int _count;
        public CategoryService(DataContext context, CategoryValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public int GetCategoriesCountAsync()
        {
            return _count;
        }
        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            _validator.ValidateAndThrow(categoryDto);

            Category categoryEntity = categoryDto.CategoryDtoToCategory();

            await _context.AddAsync(categoryEntity);

            await SaveChangesAsync();
        }


        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await GetCategoryByIdHelperAsync(categoryId);

            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(category);
            await SaveChangesAsync();
        }

        public async Task<PagedList<CategoryDto>> GetAllCategoriesAsync(CategoryParameters parameters)
        {
            var categories = await _context.Categories.ToListAsync();

            _count = categories.Count;

            if (categories == null || categories.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                CategoryDto categoryDto = category.CategoryToDto();
                categoryDtos.Add(categoryDto);
            }

            IQueryable<CategoryDto> queryable = categoryDtos.AsQueryable();

            return PagedList<CategoryDto>.ToPagedList(queryable, parameters._pageNumber, parameters.PageSize);
        }

        public async Task<CategoryDto> GetCategoryByIdAysnc(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(e => e.CategoryId == categoryId);
            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var categoryDto = category.CategoryToDto();
            return categoryDto;
        }

        public async Task<int> GetCategoryIdByCategoryName(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(e => e.CategoryName == categoryName);
            int categoryId = category.CategoryId;
            if (categoryId.Equals(null))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return categoryId;
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto)
        {
            var oldCategoryDto = await GetCategoryByIdHelperAsync(categoryId);
            if (oldCategoryDto == null)
            {
                await CreateCategoryAsync(categoryDto);
                return oldCategoryDto.CategoryToDto();
            }
            else
            {
                Category category = categoryDto.CategoryDtoToCategory();
                oldCategoryDto.CategoryName = category.CategoryName;
                oldCategoryDto.CategoryDescription = category.CategoryDescription;

                await SaveChangesAsync();
                if (oldCategoryDto.CategoryToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldCategoryDto.CategoryToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<Category> GetCategoryByIdHelperAsync(int categoryId)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(e => e.CategoryId == categoryId);

            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return category;
        }

        
    }
}
