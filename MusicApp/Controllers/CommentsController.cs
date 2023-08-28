using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;
using MusicApp.Repositories.Interfaces;

namespace MusicApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentRepository _commentRepository;
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private IPostRepository _postRepository;
        

        public CommentsController(ICommentRepository commentRepository, IUserRepository userRepository, IMapper mapper, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _postRepository = postRepository;
        }

        [HttpGet("user/{id}")]
<<<<<<<<< Temporary merge branch 1

            public IActionResult GetCommentsByUser(long id)
            {

                try 
                {
                    var comments = _commentRepository.GetCommentsByUser(id);
                    if (comments is null)
                    {
                        return NotFound();
                    }
=========
        public IActionResult GetCommentsByUser(long id)
        {
            try
            {
                var comments = _commentRepository.GetCommentsByUser(id);
                if (comments is null)
                {
                    return NotFound();
                }
>>>>>>>>> Temporary merge branch 2
                    }
=========
        public IActionResult GetCommentsByUser(long id)
<<<<<<<<< Temporary merge branch 1
                    return Ok(commentsDTO);
                }
                catch(Exception Ex)
                {
                return StatusCode(500, Ex.Message);
                }
        }

        [HttpGet("post/{id}")]

                    return Ok(commentsDTO);
                }
                catch (Exception Ex)
                {
                        return StatusCode(500, Ex.Message);
                }
            {
                return StatusCode(500, Ex.Message);
<<<<<<<<< Temporary merge branch 1
       [HttpPost]
       public IActionResult Post(CommentNewDTO commentNewDTO)
       {
=========
        [HttpPost]
        public IActionResult Post(CommentNewDTO commentNewDTO)
        {
>>>>>>>>> Temporary merge branch 2
       {
=========
        [HttpPost]
        public IActionResult Post(CommentNewDTO commentNewDTO)
        {
>>>>>>>>> Temporary merge branch 2
                if (comments is null)

                var comments = _commentRepository.GetAllCommentsByPost(id);
                if (comments is null)
                {
                    return NotFound();
                }
>>>>>>>>> Temporary merge branch 2

        [HttpGet("post/{id}")]
        public IActionResult GetCommentsByPost(long id)
        {
            try
            {
                var comments = _commentRepository.GetAllCommentsByPost(id);
                if (comments is null)
                {
                    return NotFound();
                }
>>>>>>>>> Temporary merge branch 2

                var commentsDTO = _mapper.Map<List<CommentDTO>>(comments);

<<<<<<<<< Temporary merge branch 1
                    return Ok(commentsDTO);
                }
                var commentDTO = _mapper.Map<CommentDTO>(comment);
>>>>>>>>> Temporary merge branch 2
                        return StatusCode(500, Ex.Message);
                }
=========
                return Ok(commentsDTO);
>>>>>>>>> Temporary merge branch 2
            }
            catch (Exception Ex)
            {
                return StatusCode(500, Ex.Message);
            }

<<<<<<<<< Temporary merge branch 1
       [HttpPost]
       public IActionResult Post(CommentNewDTO commentNewDTO)
       {
=========
        [HttpPost]
        public IActionResult Post(CommentNewDTO commentNewDTO)
        {
>>>>>>>>> Temporary merge branch 2
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

<<<<<<<<< Temporary merge branch 1
                
                var postexist = _postRepository.FindById(commentNewDTO.PostId);
                if (postexist is null)
                {
                    return BadRequest();
                }
                
                if (commentNewDTO.Text is null)
                {
                    return NotFound("comentario vacio");
                }
                
                var comment = _mapper.Map<Comment>(commentNewDTO);
                _commentRepository.Save(comment);

                var commentDTO =_mapper.Map<CommentDTO>(comment);
=========
                // Validación de modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var postexist = _postRepository.FindById(commentNewDTO.PostId);

                if (postexist is null)
                {
                    ModelState.AddModelError("PostId", "El PostId proporcionado no es válido.");
                    return BadRequest(ModelState);
                }

                Comment comment = new Comment
                {
                    CreationDate = DateTime.Now,
                    Text = commentNewDTO.Text,
                    PostId = commentNewDTO.PostId,
                    UserId = user.Id
                };
                
                _commentRepository.Save(comment);

                var commentDTO = _mapper.Map<CommentDTO>(comment);
>>>>>>>>> Temporary merge branch 2

                return Created("Creado con exito", commentDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
