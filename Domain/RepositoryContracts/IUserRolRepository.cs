using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Domain.RepositoryContracts
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        IEnumerable<UserRole> FindRolesByUserId(Expression<Func<UserRole, bool>> predicate);
    }
}
