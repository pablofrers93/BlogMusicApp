using Microsoft.EntityFrameworkCore;
using MusicApp.Models;
using MusicApp.Models.Entities;
using MusicApp.Repositories.Interfaces;

namespace MusicApp.Repositories
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public Post FindById(long id)
        {
            return FindByCondition(post => post.Id == id)
                .FirstOrDefault();
        }

        public Post FindByCategory(string number)
        {
            return FindByCondition(post => post.Category.ToUpper() == post.Category.ToUpper())
            .FirstOrDefault();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return FindAll()
                .ToList();
        }

        public Post GetLastPostRegistered()
        {
            return RepositoryBlogContext.Posts.OrderByDescending(post => post.CreationDate).FirstOrDefault();
        }

        public IEnumerable<Post> GetPostsByUser(long userId)
        {
            return FindByCondition(post => post.UserId == userId)
                .ToList();
        }

        public void Save(Post post)
        {
            if (post.Id == 0)
            {
                Create(post);
            }
            else
            {
                Update(post);
            }
            SaveChanges();
        }
    }
}
