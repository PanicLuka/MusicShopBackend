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
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        private readonly RoleValidator _roleValidator;
        public RoleService(DataContext context, RoleValidator roleValidator)
        {
            _context = context;
            _roleValidator = roleValidator;
        }

        public async Task CreateRoleAsync(RoleDto roleDto)
        {
            _roleValidator.ValidateAndThrow(roleDto);

            Role roleEntity = roleDto.RoleDtoToRole();


            await _context.AddAsync(roleEntity);

            await SaveChangesAsync();


        }


        public async Task DeleteRoleAsync(int roleId)
        {
            var role = await GetRoleByIdHelperAsync(roleId);

            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(role);
            await SaveChangesAsync();
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            if (roles == null || roles.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<RoleDto> roleDtos = new List<RoleDto>();

            foreach (var role in roles)
            {
                RoleDto roleDto = role.RoleToDto();
                roleDtos.Add(roleDto);
            }

            return roleDtos;

        }

        public async Task<RoleDto> GetRoleByIdAsync(int roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(e => e.RoleId == roleId);
            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var roleDto = role.RoleToDto();
            return roleDto;
        }

        public async Task<RoleDto> UpdateRoleAsync(int roleId, RoleDto roleDto)
        {
            var oldRoleDto = await GetRoleByIdHelperAsync(roleId);
            if (oldRoleDto == null)
            {
                await CreateRoleAsync(roleDto);
                return oldRoleDto.RoleToDto();
            }
            else
            {
                Role role = roleDto.RoleDtoToRole();
                oldRoleDto.RoleName = role.RoleName;

                await SaveChangesAsync();
                if (oldRoleDto.RoleToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldRoleDto.RoleToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<Role> GetRoleByIdHelperAsync(int roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(e => e.RoleId == roleId);

            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return role;
        }



        public string GetRoleByRoleId(int roleId)
        {
            var role = _context.Roles.FirstOrDefault(r => r.RoleId == roleId);

            if (role == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            string roleName = role.RoleName;

            return roleName;
        }
    }
}
