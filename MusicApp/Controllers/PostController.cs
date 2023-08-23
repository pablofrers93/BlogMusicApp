using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var posts = _postRepository.GetAllPosts();
                var postsDTO = new List<PostDTO>();
                foreach (Post post in posts)
                {
                    var newPostDTO = new PostDTO
                    {
                        Id = post.Id,
                        CreationDate = post.CreationDate,
                        Title = post.Title,
                        Image = post.Image,
                        Text = post.Text,
                        Category = post.Category
                    };
                    postsDTO.Add(newPostDTO);
                }
                return Ok(postsDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
