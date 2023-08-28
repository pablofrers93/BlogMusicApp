using MusicApp.Models.Entities;

namespace MusicApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        void Save(User client);
        User? FindById(long id);
        User? FindByEmail(string email);
    }
}
