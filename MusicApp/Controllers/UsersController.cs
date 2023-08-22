using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;
using MusicApp.Models.Enums;
using MusicApp.Repositories.Interfaces;

namespace MusicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var users = _userRepository.GetAllUsers();

                var usersDTO = new List<UserDTO>();

                foreach (User user in users)
                {
                    var newUserDTO = new UserDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,


                        Posts = user.Posts.Select(p => new PostDTO
                        {
                            Id = p.Id,
                            CreationDate = p.CreationDate,
                            Title = p.Title,
                            Image = p.Image,
                            Text = p.Text,
                            Category = p.Category,
                        }).ToList()

                    };

                    usersDTO.Add(newUserDTO);
                }
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

                UserDTO newUserDTO = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,

                    Posts = user.Posts.Select(p => new PostDTO
                    {
                        Id = p.Id,
                        CreationDate = p.CreationDate,
                        Title = p.Title,
                        Image = p.Image,
                        Text = p.Text,
                        Category = p.Category,
                    }).ToList()

                };

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

                UserDTO newUserDTO = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,

                    Posts = user.Posts.Select(p => new PostDTO
                    {
                        Id = p.Id,
                        CreationDate = p.CreationDate,
                        Title = p.Title,
                        Image = p.Image,
                        Text = p.Text,
                        Category = p.Category,
                    }).ToList()

                };

                return Ok(newUserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] UserDTO userDTO)
        //{

        //}

    }
}
