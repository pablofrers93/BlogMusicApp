using AutoMapper;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;


namespace MusicApp.Automapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<NewUserDTO, User>();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();


        }
    }
}
