using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public interface IRepository
    {
        Task<T> Get<T>(Expression<Func<T, bool>> predicate) where T : class, IBaseEntity;

        IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate) where T : class, IBaseEntity;
        Task<T> AddAsync<T>(T entity) where T : class, IBaseEntity;

        void Update<T>(T eintity) where T : class, IBaseEntity;

        Task DeleteAsync<T>(T entity) where T : class, IBaseEntity;
    }
}
