using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.RepositoryContracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Find(object id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Insert(T entity);
        void Delete(object id);
        void Delete(T entity);
        void Update(T entity);
        void SaveChanges();
    }
}
