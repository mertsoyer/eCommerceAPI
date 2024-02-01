using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Domain.Entities.Common;
using eCommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly eCommerceAPIDbContext _context;

        public WriteRepository(eCommerceAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            var addedEntity = await Table.AddAsync(entity);
            return addedEntity.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entity)
        {
            await Table.AddRangeAsync(entity);
            return true;
        }

        public bool Remove(T entity)
        {
            var entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> entity)
        {
            Table.RemoveRange(entity);
            return true;
        }

        public async Task<bool> Remove(string id)
        {
            // Yukarda şartı where T : BaseEntity olarak verdiğimiz için artık gelen değerlerin bir BaseEntity olduğu yanı bir ID si mutlaka olacağını biliyoruz. x=>x.Id yi bu şekilde kullanabiliyoruz. Diğer türlü Find ile ilgili ID yi bulup Remove yapabilirdik ;
            //return await _context.Set<T>().FindAsync(id); 

            T model = await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
            return Remove(model);
        }

        public bool Update(T entity)
        {
            var entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


    }
}
