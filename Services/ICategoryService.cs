using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryDto categoryDto);

        Task<PagedList<CategoryDto>> GetAllCategoriesAsync(CategoryParameters parameteres);

        Task<int> GetCategoryIdByCategoryName(string categoryName);


        Task<CategoryDto> GetCategoryByIdAysnc(int categoryId);

        Task<CategoryDto> UpdateCategoryAsync(int ctegoryId, CategoryDto categoryDto);

        Task DeleteCategoryAsync(int categoryId);
    }
}
