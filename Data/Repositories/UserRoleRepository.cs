using Domain.DbContext;
using Domain.Model;
using Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IContext dbContext)
            : base(dbContext)
        { }

        public IEnumerable<UserRole> FindRolesByUserId(Expression<Func<UserRole, bool>> predicate)
        {
            return _dbSet.Include("Role").Where(predicate);
        }
    }
}
