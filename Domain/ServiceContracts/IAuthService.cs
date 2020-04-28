using Domain.ViewModel;

namespace Domain.ServiceContracts
{
    public interface IAuthService
    {
        ResponseEntityVM Authenticate(string userName, string password);
    }
}
