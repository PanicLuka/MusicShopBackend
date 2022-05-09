using MusicShopBackend.Entities;
using MusicShopBackend.Helpers;
using MusicShopBackend.Models;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDto userDto);

        Task<PagedList<UserDto>> GetAllUsersAsync(UserParameters parameters);

        Task<UserDto> GetUserByIdAysnc(int userId);


        Task<UserDto> UpdateUserAsync(int userId, UserDto userDto);

        Task DeleteUserAsync(int userId);
        int GetRoleIdByEmail(string email);

        UserDto GetUserByEmail(string email);

    }
}
