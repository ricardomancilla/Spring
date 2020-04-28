using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.RepositoryContracts
{
    public interface ISecurityRepository<T, U> where T : class where U : class
    {
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        IEnumerable<U> FindRoles(Expression<Func<U, bool>> predicate);

        void SaveChanges();
    }
}
