using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Models.DTOs;
using MusicApp.Repositories.Interfaces;

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

                var newPostDTO = _mapper.Map<PostDTO>(post);

                return Ok(newPostDTO);

            }
            catch (Exception Ex)
            {
                return StatusCode(500, Ex.Message);
            }
        }


    }
}
