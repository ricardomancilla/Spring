using Domain.DbContext;
using Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class SecurityRepository<T, U> : ISecurityRepository<T, U> where T : class where U : class
    {
        protected IContext _dbContext;
        private DbSet<T> _dbSetT;
        private DbSet<U> _dbSetU;

        public SecurityRepository(IContext dbContext)
        {
            _dbContext = dbContext;
            _dbSetT = _dbContext.Set<T>();
            _dbSetU = _dbContext.Set<U>();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _dbSetT.Where(predicate);
        }

        public IEnumerable<U> FindRoles(Expression<Func<U, bool>> predicate)
        {
            return _dbSetU.Include("Role").Where(predicate);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
