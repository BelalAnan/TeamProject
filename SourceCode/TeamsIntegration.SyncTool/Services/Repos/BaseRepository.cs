using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsIntegration.Data;
using TeamsIntegration.SyncTool.Services.Contracts;
using EFCore.DbContextFactory.Extensions;

namespace TeamsIntegration.SyncTool.Services.Repos
{
   public class BaseRepository<T> : IAsyncRepository<T> where T :class
    {
       // protected  TeamDBContext _dbContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        //public BaseRepository(IServiceScopeFactory serviceScopeFactory)
        //{
        //    _serviceScopeFactory = serviceScopeFactory;
        //    using var scope = _serviceScopeFactory.CreateScope();
        //    _dbContext = scope.ServiceProvider.GetRequiredService<TeamDBContext>();
        //    //_dbContext = dbContext;
        //}
        public BaseRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            
            // return await _dbContext.Set<T>().FindAsync(id);
            return null;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            //  return await _dbContext.Set<T>().ToListAsync();
            return null;
        }

     
        public virtual async Task<T> AddAsync(T entity)
        {
              using var scope = _serviceScopeFactory.CreateScope();
          var    _dbContext = scope.ServiceProvider.GetRequiredService<TeamDBContext>();
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
          //  _dbContext.Entry(entity).State = EntityState.Modified;
            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
          //  _dbContext.Set<T>().Remove(entity);
            //await _dbContext.SaveChangesAsync();
        }
    }
}

