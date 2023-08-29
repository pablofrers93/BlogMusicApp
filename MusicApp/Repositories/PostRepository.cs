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
                .Include(post => post.Comments.OrderByDescending(comment => comment.CreationDate))
                  .ThenInclude(c => c.User)
                .FirstOrDefault();
        }

        public IEnumerable<Post> FindByCategory(string category)
        {
            return FindByCondition(post => post.Category.ToUpper() == category.ToUpper())
                //.Include(post => post.Comments.OrderByDescending(comment => comment.CreationDate))
                  //.ThenInclude(c => c.User)
                .ToList();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            //return FindAll()
            //    .Include(post => post.Comments)
            //    .OrderBy(post => post.Comments.OrderBy(comment => comment.CreationDate))
            //    .ToList();
            return FindAll()
                  .Include(post => post.Comments.OrderByDescending(comment => comment.CreationDate))
                  .ThenInclude(c => c.User)
                  .ToList();
        }

        public Post GetLastPostRegistered()
        {
            return RepositoryBlogContext.Posts.OrderByDescending(post => post.CreationDate).FirstOrDefault();
        }

        public IEnumerable<Post> GetPostsByUser(long userId)
        {
            return FindByCondition(post => post.UserId == userId)
               .Include(post => post.Comments.OrderByDescending(comment => comment.CreationDate))
                  .ThenInclude(c => c.User)
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
