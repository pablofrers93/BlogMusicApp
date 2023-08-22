using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;
using MusicApp.Models.Enums;
using MusicApp.Repositories.Interfaces;
using System.Net;
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

        [HttpPost]
        public IActionResult Post([FromBody] NewUserDTO newUserDTO)
        {
            try
            {
                // Validación de nombre y apellido
                if (!IsNameValid(newUserDTO.FirstName) || !IsNameValid(newUserDTO.LastName))
                {
                    return StatusCode(400, "datos inválidos");
                }
                // Validación de contraseña
                if (!IsPasswordValid(newUserDTO.Password))
                {
                    return StatusCode(400, "datos inválidos");
                }
                // Validación de dirección de correo electrónico
                if (!IsValidEmail(newUserDTO.Email))
                {
                    return StatusCode(400, "Email inválido.");
                }

                // Validación de datos obligatorios
                if (String.IsNullOrEmpty(newUserDTO.Email) || String.IsNullOrEmpty(newUserDTO.Password) || String.IsNullOrEmpty(newUserDTO.FirstName) || String.IsNullOrEmpty(newUserDTO.LastName))
                    return StatusCode(403, "datos inválidos");

                // Buscar si el correo electrónico ya está en uso
                User user = _userRepository.FindByEmail(newUserDTO.Email);
                if (user is not null)
                {
                    return StatusCode(403, "Email está en uso");
                }

                //Mapeamos el DTO que llega del front a Usuario para guardarlo en el repo
                var newUser = _mapper.Map<User>(newUserDTO);
                _userRepository.Save(newUser);

                // Mapeamos el User a un UserDTO para devolver una respuesta de éxito
                var userDTO = _mapper.Map<UserDTO>(newUser);

                return Created("", userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        private static bool IsNameValid(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z\s]+$") && name.Length >= 3;
        }

        private static bool IsPasswordValid(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$");
        }

        private static bool IsValidEmail(string email)
        {
            // Verificación básica del formato de correo electrónico usando una expresión regular
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return false;
            }

            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
