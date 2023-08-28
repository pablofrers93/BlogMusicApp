using MusicApp.Models.Entities;

namespace MusicApp.Repositories.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        void Save(Post post);
        Post FindById(long id);
        IEnumerable<Post> GetPostsByUser(long userId);
        Post GetLastPostRegistered();
        IEnumerable<Post> FindByCategory(string number);
         
    }
}
