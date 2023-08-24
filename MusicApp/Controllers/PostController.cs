using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;
using MusicApp.Repositories.Interfaces;
using System.Security.Principal;

namespace MusicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var posts = _postRepository.GetAllPosts();
                var postsDTO = _mapper.Map<List<PostDTO>>(posts);

                return Ok(postsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("posts")]
        public IActionResult GetPostsByCategory(string category)
        {
            try
            {
                var posts = _postRepository.GetAllPosts().Where(p => p.Category == category).ToList();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }   
        }
    }
}
