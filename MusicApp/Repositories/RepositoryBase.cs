using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MusicApp.Models;
using MusicApp.Repositories.Interfaces;
using System.Linq.Expressions;

namespace MusicApp.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected BlogContext RepositoryBlogContext { get; set; }
        public RepositoryBase(BlogContext blogContext)
        {
            RepositoryBlogContext = blogContext;
        }
        public void Create(T entity)
        {
            this.RepositoryBlogContext.Add(entity);
        }
        public void Update(T entity)
        {
            this.RepositoryBlogContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryBlogContext.Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this.RepositoryBlogContext.Set<T>().AsNoTrackingWithIdentityResolution();
        }

        public IQueryable<T> FindAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> queryable = this.RepositoryBlogContext.Set<T>();

            if (includes != null)
            {
                queryable = includes(queryable);
            }
            return queryable.AsNoTrackingWithIdentityResolution();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryBlogContext.Set<T>().Where(expression).AsNoTrackingWithIdentityResolution();
        }

        public void SaveChanges()
        {
            this.RepositoryBlogContext.SaveChanges();
        }

        
    }
}
