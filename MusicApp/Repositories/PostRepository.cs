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
                            .Include(post => post.Comments)
                            .ThenInclude(c => c.User)
                            .Select(post => new Post
                            {
                                Id = post.Id,
                                CreationDate = post.CreationDate,
                                Title = post.Title,
                                Image = post.Image,
                                Text = post.Text,
                                Category = post.Category,
                                UserId = post.UserId,
                                User = new User
                                {
                                    Id = post.User.Id,
                                    FirstName = post.User.FirstName,
                                    LastName = post.User.LastName,
                                    Email = post.User.Email
                                },
                                Comments = post.Comments
                                 .OrderByDescending(comment => comment.CreationDate)
                                 .Select(comment => new Comment
                                 {
                                     Id = comment.Id,
                                     CreationDate = comment.CreationDate,
                                     Text = comment.Text,
                                     PostId = post.Id,
                                     UserId = comment.UserId,
                                     User = new User
                                     {
                                         Email = comment.User.Email,
                                     }
                                 })
                                 .ToList()
                            })
                            .FirstOrDefault();
        }

        public IEnumerable<Post> FindByCategory(string category)
        {
            return FindByCondition(post => post.Category.ToUpper() == category.ToUpper())
                            .Include(post => post.User)
                            .Include(post => post.Comments)
                            .ThenInclude(c => c.User)
                            .Select(post => new Post
                            {
                                Id = post.Id,
                                CreationDate = post.CreationDate,
                                Title = post.Title,
                                Image = post.Image,
                                Text = post.Text,
                                Category = post.Category,
                                UserId = post.UserId,
                                User = new User
                                {
                                    Id = post.User.Id,
                                    FirstName = post.User.FirstName,
                                    LastName = post.User.LastName,
                                    Email = post.User.Email
                                },
                                Comments = post.Comments
                                 .OrderByDescending(comment => comment.CreationDate)
                                 .Select(comment => new Comment
                                 {
                                     Id = comment.Id,
                                     CreationDate = comment.CreationDate,
                                     Text = comment.Text,
                                     PostId = post.Id,
                                     UserId = comment.UserId,
                                     User = new User
                                     {
                                         Email = comment.User.Email,
                                     }
                                 })
                                 .ToList()
                            })
                            .ToList();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return FindAll().Include(post => post.User)
                            .Include(post => post.Comments)
                            .ThenInclude(c => c.User)
                            .Select(post => new Post
                            {
                                Id = post.Id,
                                CreationDate = post.CreationDate,
                                Title = post.Title,
                                Image = post.Image,
                                Text = post.Text,
                                Category = post.Category,
                                UserId = post.UserId,
                                User = new User
                                {
                                    Id = post.User.Id,
                                    FirstName = post.User.FirstName,
                                    LastName = post.User.LastName,  
                                    Email = post.User.Email
                                },
                                Comments = post.Comments
                                 .OrderByDescending(comment => comment.CreationDate)
                                 .Select(comment => new Comment
                                 {
                                     Id = comment.Id,
                                     CreationDate = comment.CreationDate,
                                     Text = comment.Text,
                                     PostId = post.Id,
                                     UserId = comment.UserId,
                                     User = new User
                                     {
                                         Email = comment.User.Email,
                                     }
                                 })
                                 .ToList()
                            })
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
