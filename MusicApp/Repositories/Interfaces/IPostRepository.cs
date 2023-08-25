using MusicApp.Models.Entities;
using System.Security.Principal;

namespace MusicApp.Repositories.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        void Save(Post post);
        Post FindById(long id);
        IEnumerable<Post> GetPostsByUser(long userId);
        Post GetLastPostRegistered();
        Post FindByCategory(string number);
         
    }
}
