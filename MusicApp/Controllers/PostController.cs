using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;
using MusicApp.Models.Enums;
using MusicApp.Repositories;
using MusicApp.Repositories.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace MusicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var posts = _postRepository.GetAllPosts();
                var postsDTO = _mapper.Map<List<GetPostDTO>>(posts);

                return Ok(postsDTO);
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
                var post = _postRepository.FindById(id);

                if (post is null)
                {
                    return NotFound(); //404
                }

                var newPostDTO = _mapper.Map<GetPostDTO>(post);

                return Ok(newPostDTO);

            }
            catch (Exception Ex)
            {
                return StatusCode(500, Ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDTO newPostDTO)
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.Email) != null ? User.FindFirst(ClaimTypes.Email).Value : string.Empty;
                if (email == string.Empty)
                {
                    return Unauthorized();
                }
                User user = _userRepository.FindByEmail(email);

                if (user is null)
                {
                    return Unauthorized();
                }

                if (!ValidarPost(newPostDTO))
                {
                    return BadRequest("El post no cumple con las validaciones");
                }

                //var imagePath = await SaveImage(newPostDTO.Image);
                var post = new Post
                {
                    CreationDate = DateTime.Now,
                    Title = newPostDTO.Title,
                    Image = "",
                    Text = newPostDTO.Text,
                    Category = newPostDTO.Category,
                    UserId = user.Id
                };

                _postRepository.Save(post);

                var postDTO = new PostDTO
                {
                    Title = post.Title,
                    //Image = newPostDTO.Image,
                    Text = post.Text,
                    Category = post.Category
                };

                return Created("El post se creo correctamente.", postDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private bool ValidarPost(PostDTO newPostDTO)
        {
            return newPostDTO.Text != string.Empty ||
                   newPostDTO.Title != string.Empty ||
                   newPostDTO.Category != string.Empty;
        }

       // Método para guardar la imagen de forma local y devuelver la ruta donde fue guardada.

        [HttpPost("SaveImage")]
        private async Task<string> SaveImage([FromForm] IFormFile file)
        {
            var ruta = String.Empty;

            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + ".jpg";
                ruta = $"Images/{fileName}";
                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            return ruta;
        }

        [HttpGet("category/{category}")]
            public IActionResult GetCategory(string category)
            {
                try
                {
                    var postsCategory = _postRepository.FindByCategory(category);

                    if(postsCategory is null)
                    {
                    return BadRequest("categoria vacia");
                    }
                    var postsDTO = _mapper.Map<List<GetPostDTO>>(postsCategory);
                    return Ok(postsDTO);

                }
                catch(Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

    }
    
}
