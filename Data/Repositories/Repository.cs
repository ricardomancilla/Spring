using Domain.DbContext;
using Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected IContext _dbContext;
        protected DbSet<T> _dbSet;

        public Repository(IContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T Find(object id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual T Insert(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void Delete(object id)
        {
            Delete(_dbSet.Find(id));
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
