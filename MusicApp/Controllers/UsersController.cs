using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;
using MusicApp.Models.Enums;
using MusicApp.Repositories.Interfaces;
using System.Text.RegularExpressions;

namespace MusicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _userRepository.GetAllUsers();
                var usersDTO = _mapper.Map<List<UserDTO>>(users);

                return Ok(usersDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var user = _userRepository.FindById(id);

                if (user is null)
                {
                    return NotFound(); //404
                }

                var newUserDTO = _mapper.Map<UserDTO>(user);
          
                return Ok(newUserDTO);

            }
            catch (Exception Ex)
            {
                return StatusCode(500, Ex.Message);
            }
        }

        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            try
            {
                string email = User.FindFirst("User") != null ? User.FindFirst("User").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Unauthorized();
                }

                User user = _userRepository.FindByEmail(email);

                if (user is null)
                {
                    return Unauthorized();
                }
                var newUserDTO = _mapper.Map<UserDTO>(user);

                return Ok(newUserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

       

    }
}
