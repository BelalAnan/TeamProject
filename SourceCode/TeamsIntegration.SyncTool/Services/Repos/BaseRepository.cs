using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsIntegration.Data;
using TeamsIntegration.SyncTool.Services.Contracts;

namespace TeamsIntegration.SyncTool.Services.Repos
{
   public class BaseRepository<T> : IAsyncRepository<T> where T :class
    {
        protected readonly TeamDBContext _dbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BaseRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            using var scope = _serviceScopeFactory.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<TeamDBContext>();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

     
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

