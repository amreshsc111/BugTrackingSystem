using BugTrackingSystem.Application.Interfaces;
using BugTrackingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace BugTrackingSystem.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDbContext dbContext) : IRepository<T> where T : class
    {
        public async Task<T?> GetByIdAsync(Guid id)
            => await dbContext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync()
            => await dbContext.Set<T>().ToListAsync();

        public async Task AddAsync(T entity)
            => await dbContext.Set<T>().AddAsync(entity);

        public void Update(T entity)
            => dbContext.Set<T>().Update(entity);

        public void Delete(Guid id)
        {
            var entity = dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                dbContext.Set<T>().Remove(entity);
            }
        }

        public Task<int> SaveChangesAsync()
            => dbContext.SaveChangesAsync();
    }
}
