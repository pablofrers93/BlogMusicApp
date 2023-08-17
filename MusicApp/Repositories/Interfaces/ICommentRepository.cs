using MusicApp.Models.Entities;

namespace MusicApp.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        void Save(Comment comment);
        void Update(Comment comment);
        void Delete(Comment comment);
        Comment FindById(long id);
        IEnumerable<Comment> GetCommentsByUser(long userId);
        string GetLastComment();
    }
}
