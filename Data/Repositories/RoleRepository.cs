using Domain.DbContext;
using Domain.Model;
using Domain.RepositoryContracts;

namespace Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IContext dbContext)
            : base(dbContext)
        { }
    }
}
