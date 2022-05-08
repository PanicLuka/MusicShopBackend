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
using BC = BCrypt.Net.BCrypt;


namespace MusicShopBackend.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly UserValidator _validator;

        public UserService(DataContext context, UserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            _validator.ValidateAndThrow(userDto);

            User userEntity = userDto.UserDtoToUser();

            userEntity.Password = BC.HashPassword(userEntity.Password);

            await _context.AddAsync(userEntity);

            await SaveChangesAsync();
        }


        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdHelperAsync(userId);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(user);
            await SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            if (users == null || users.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            List<UserDto> userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                UserDto userDto = user.UserToDto();
                userDtos.Add(userDto);
            }

            return userDtos;

        }

        public async Task<UserDto> GetUserByIdAysnc(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserId == userId);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var userDto = user.UserToDto();
            return userDto;
        }

        public async Task<UserDto> UpdateUserAsync(int userId, UserDto userDto)
        {
            var oldUserDto = await GetUserByIdHelperAsync(userId);
            if (oldUserDto == null)
            {
                await CreateUserAsync(userDto);
                return oldUserDto.UserToDto();
            }
            else
            {
                User user = userDto.UserDtoToUser();
                oldUserDto.FirstName = user.FirstName;
                oldUserDto.LastName = user.LastName;
                oldUserDto.Email = user.Email;
                oldUserDto.Password = user.Password;
                


                await SaveChangesAsync();
                if (oldUserDto.UserToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                }
                return oldUserDto.UserToDto();
            }
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        private async Task<User> GetUserByIdHelperAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserId == userId);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user;
        }

        public  int GetRoleIdByEmail(string email)
        {
            var user =  _context.Users.FirstOrDefault(e => e.Email == email);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            int roleId = user.RoleId;

            return roleId;
        }

        public UserDto GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email == email);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var userDto = user.UserToDto();

            return userDto;
        }
    }
}
