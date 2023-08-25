using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;
using MusicApp.Repositories;
using MusicApp.Repositories.Interfaces;
using System.Text.RegularExpressions;

namespace MusicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentRepository _commentRepository;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        

        public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            
        }

        [HttpGet("user/{id}")]
            public IActionResult GetCommentsByUser(long id)
            {
                try 
                {
                var comments = _commentRepository.GetCommentsByUser(id);
                if (comments is null)
                {
                    return NotFound();
                }

                var commentsDTO = _mapper.Map<List<CommentDTO>>(comments);

                return Ok(commentsDTO);
                }
                catch(Exception Ex)
                {
                return StatusCode(500, Ex.Message);
                }
            }

        [HttpGet("post/{id}")]
            public IActionResult GetCommentsByPost(long id)
            {
                try
                {
                var comments = _commentRepository.GetAllCommentsByPost(id);
                if(comments is null)
                {
                    return NotFound();
                }

                var commentsDTO = _mapper.Map<List<CommentDTO>>(comments);

                return Ok(commentsDTO);
                }
                catch (Exception Ex)
                {
                        return StatusCode(500, Ex.Message);
                }
            }

        [HttpPost]
       public IActionResult Post(CommentNewDTO comment)
       {
            try
            {
                //validaciones
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
                Comment newComment = new Comment
                {
                    Id = comment.Id,
                    CreationDate = DateTime.Now,
                    Text = comment.Text,
                    PostId = comment.PostId,
                    UserId = comment.UserId
                };
                _commentRepository.Save(newComment);
                CommentDTO newCommentDTO = new CommentDTO
                {
                    Id = comment.Id,
                    CreationDate = DateTime.Now,
                    Text = comment.Text
                };
                return Created("Creado con exito", newCommentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
       }

    }
}
