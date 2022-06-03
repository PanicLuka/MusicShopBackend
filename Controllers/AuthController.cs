using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicShopBackend.Models;
using MusicShopBackend.Services;

namespace MusicShopBackend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/login")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticateService _authenticateService;

        public AuthController(IUserService userService, IAuthenticateService authenticateService)
        {
            _userService = userService;
            _authenticateService = authenticateService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UserLogin user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var savedUser =  _userService.GetUserByEmail(user.Email);

            bool passwordVerified = _authenticateService.VerifiedPassword(user);

            if (user.Email == savedUser.Email && passwordVerified)
            {

                var tokenString = _authenticateService.GenerateToken(user);
                return Ok(tokenString);
            }

            return Unauthorized();

        }

    }
}
