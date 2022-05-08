using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryDto categoryDto);

        Task<List<CategoryDto>> GetAllCategoriesAsync();

        Task<CategoryDto> GetCategoryByIdAysnc(int categoryId);

        Task<CategoryDto> UpdateCategoryAsync(int ctegoryId, CategoryDto categoryDto);

        Task DeleteCategoryAsync(int categoryId);
    }
}
