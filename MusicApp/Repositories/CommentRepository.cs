using MusicApp.Models;
using MusicApp.Models.Entities;
using MusicApp.Repositories.Interfaces;

namespace MusicApp.Repositories
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public Comment FindById(long id)
        {
            return FindByCondition(c => c.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Comment> GetCommentsByUser(long userId)
        {
            return FindByCondition(c => c.UserId == userId)
                .ToList();
        }

        public string GetLastComment()
        {
            var lastComment = FindAll()
               .OrderByDescending(comment => comment.Id)
               .FirstOrDefault();
            if (lastComment == null)
            {
                return null;
            }
            return lastComment.Text;
        }

        public void Save(Comment comment)
        {
            Create(comment);
            SaveChanges();
        }
    }
}
