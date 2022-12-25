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
        private readonly AppDbContext _db;

        public EfRepository(AppDbContext context) => _db = context;

        public async Task<T> AddAsync<T>(T entity) where T : class, IBaseEntity
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public Task<T> Get<T>(Expression<Func<T, bool>> predicate) where T : class, IBaseEntity
        {
            return _db.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Update<T>(T eintity) where T : class, IBaseEntity
        {
            _db.Set<T>().Update(eintity);
            _db.SaveChanges();
        }
        /// <summary>
        /// Получить полный список
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns>IEnumerable T</returns>
        public IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class, IBaseEntity
        {
            return _db.Set<T>().Where(predicate).ToList();
        }

        public Task DeleteAsync<T>(T entity) where T : class, IBaseEntity
        {
            _db.Set<T>().Remove(entity);
            return _db.SaveChangesAsync();
        }
    }
}
