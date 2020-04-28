using Domain.DbContext;
using Domain.Model;
using Domain.RepositoryContracts;

namespace Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IContext dbContext)
            : base(dbContext)
        { }
    }
}
