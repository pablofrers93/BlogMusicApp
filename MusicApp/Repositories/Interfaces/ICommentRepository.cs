using MusicApp.Models.Entities;

namespace MusicApp.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        void Save(Comment comment);
        Comment FindById(long id);
        IEnumerable<Comment> GetCommentsByUser(long userId);
        IEnumerable<Comment> GetAllCommentsByPost(long postI);
        string GetLastComment();
    }
}
