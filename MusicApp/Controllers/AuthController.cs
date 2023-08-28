using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Repositories.Interfaces;
using System.Security.Claims;
using MusicApp.Models.Entities;
using MusicApp.Models.DTOs;
using MusicApp.Services;

namespace MusicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;
        private ITokenServices _tokenServices;

        public AuthController(IUserRepository userRepository, ITokenServices tokenServices)
        {
            _userRepository = userRepository;
            _tokenServices = tokenServices;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO User)
        {
            try
            {
                User userLogin = _userRepository.FindByEmail(User.Email);
                if (userLogin == null || !String.Equals(userLogin.Password, User.Password))
                    return Unauthorized();

                string jwtToken = _tokenServices.GenerateToken(User.Email);

                var claims = new List<Claim>
                {
                    new Claim("User", userLogin.Email),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return Ok(new { token = jwtToken });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
