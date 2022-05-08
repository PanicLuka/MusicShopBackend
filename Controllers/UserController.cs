using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShopBackend.Models;
using MusicShopBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicShopBackend.Controllers
{

    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //[Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync()
        {
            var userDtos = await _userService.GetAllUsersAsync();

            return Ok(userDtos);
        }

        //[Authorize(Roles = "Admin, Employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(int userId)
        {
            var userDto = await _userService.GetUserByIdAysnc(userId);

            return Ok(userDto);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUserAsync([FromBody] UserDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userService.CreateUserAsync(userDto);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //[Authorize(Roles = "User")]
        [HttpPut("{userId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> UpdateUserAsync(int userId, [FromBody] UserDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = await _userService.UpdateUserAsync(userId, userDto);
                    return Ok(newUser);
                }
                else
                {
                    return BadRequest();
                }
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        //[Authorize(Roles = "Admin, Employee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

            }
        }

    }
}
