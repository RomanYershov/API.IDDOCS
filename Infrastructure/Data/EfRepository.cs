using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        //private readonly AppDbContext _dbContext;

        //public EfRepository(AppDbContext dbContext)
        //    => _dbContext = dbContext;

        public async Task<T> AddAsync<T>(T entity) where T : class, IBaseEntity
        {
            using (var db = new AppDbContext())
            {
                await db.Set<T>().AddAsync(entity);
                await db.SaveChangesAsync();

                return entity;
            }
        }

        public Task<T> Get<T>(Expression<Func<T, bool>> predicate) where T : class, IBaseEntity
        {
            using (var db = new AppDbContext())
            {
                return db.Set<T>().SingleOrDefaultAsync(predicate);
            }
        }

        public void Update<T>(T eintity) where T : class, IBaseEntity
        {
            using(var db = new AppDbContext())
            {
                db.Set<T>().Update(eintity);
                db.SaveChanges();
            }
        }
    }
}
