using Microsoft.EntityFrameworkCore;
using MusicApp.Models;
using MusicApp.Models.Entities;
using MusicApp.Repositories.Interfaces;

namespace MusicApp.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public User? FindByEmail(string email)
        {
            return FindByCondition(user => user.Email.ToUpper() == email.ToUpper())
                .Include(user => user.Posts)
                .FirstOrDefault();
        }

        public User? FindById(long id)
        {
            return FindByCondition(user => user.Id == id)
                .Include(user => user.Posts)
                .FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .Include(user => user.Posts)
                .ToList();
        }

        public void Save(User user)
        {
            Create(user);
            SaveChanges();
        }
    }
}
