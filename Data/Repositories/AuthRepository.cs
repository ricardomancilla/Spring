using Domain.DbContext;
using Domain.Model;
using Domain.RepositoryContracts;

namespace Data.Repositories
{
    public class AuthRepository : SecurityRepository<User, UserRole>, IAuthRepository
    {
        public AuthRepository(IContext dbContext)
            : base(dbContext)
        { }
    }
}
