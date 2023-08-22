using AutoMapper;
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

        [HttpPost]
        public IActionResult Post(Comment comment)
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
                    CreationDate = comment.CreationDate,
                    Text = comment.Text,
                    Post = comment.Post,
                    User = comment.User
                }
                _commentRepository.Save(newComment);
                CommentDTO newCommentDTO = new CommentDTO
                {
                    CreationDate = newCommentDTO.CreationDate,
                    Text = newCommentDTO.Text,
                    Post = newCommentDTO.Post,
                    User = newCommentDTO.User
                }
                return Created("Creado con exito", newCommentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
