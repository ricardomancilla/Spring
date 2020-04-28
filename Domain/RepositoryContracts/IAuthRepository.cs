using Domain.Model;

namespace Domain.RepositoryContracts
{
    public interface IAuthRepository : ISecurityRepository<User, UserRole>
    {
    }
}
