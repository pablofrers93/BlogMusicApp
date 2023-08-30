using AutoMapper;
using MusicApp.Models.DTOs;
using MusicApp.Models.Entities;


namespace MusicApp.Automapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts))
                .ReverseMap();
            CreateMap<NewUserDTO, User>();
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment,CommentNewDTO>().ReverseMap();
            CreateMap<Post, GetPostDTO>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => CreateImage(src.Image)));
        }

        public string CreateImage(string ruta)
        {
            if (ruta == string.Empty || ruta == null)
            {
                return "";
            }
            byte[] file = File.ReadAllBytes(ruta);
            return Convert.ToBase64String(file);
        }
    }
}
