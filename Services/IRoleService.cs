using MusicShopBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Services
{
    public interface IRoleService
    {
        Task CreateRoleAsync(RoleDto role);
        Task<List<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(int roleId);
        string GetRoleByRoleId(int roleId);
        Task<RoleDto> UpdateRoleAsync(int roleId, RoleDto role);
        Task DeleteRoleAsync(int roleId);
    }
}
