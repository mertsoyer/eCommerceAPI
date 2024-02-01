using eCommerceAPI.Application.Repositories;
using eCommerceAPI.Domain.Entities.Common;
using eCommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly eCommerceAPIDbContext _context;

        public ReadRepository(eCommerceAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
        {
            return Table;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            //return await _context.Set<T>().FindAsync(id); 

            // Yukarda şartı where T : BaseEntity olarak verdiğimiz için artık gelen değerlerin bir BaseEntity olduğu yanı bir ID si mutlaka olacağını biliyoruz. x=>x.Id yi bu şekilde kullanabiliyoruz.

            return await Table.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return Table.Where(expression);
        }
    }
}
