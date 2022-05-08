using MusicShopBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(UserDto userDto);

        Task<List<UserDto>> GetAllUsersAsync();

        Task<UserDto> GetUserByIdAysnc(int userId);


        Task<UserDto> UpdateUserAsync(int userId, UserDto userDto);

        Task DeleteUserAsync(int userId);
        int GetRoleIdByEmail(string email);

        UserDto GetUserByEmail(string email);

    }
}
