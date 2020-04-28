using AutoMapper;
using Domain.RepositoryContracts;
using Domain.ServiceContracts;
using Domain.ViewModel;
using System;
using System.Linq;
using System.Text;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        private IAuthRepository _repository;
        private IMapper _mapper;

        public AuthService(IAuthRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ResponseEntityVM Authenticate(string userName, string password)
        {
            try
            {
                var user = _repository.FindBy(x => x.Username.Equals(userName)).FirstOrDefault();

                if ((user == null) || (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)))
                    return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.Forbidden, Message = "Incorrect username or password" };

                user.Password = string.Empty;

                var userVM = _mapper.Map<AuthUserVM>(user);

                userVM.Role = _repository.FindRoles(x => x.UserId.Equals(user.UserId)).Select(x => new RoleVM() { RoleName = x.Role.RoleName }).FirstOrDefault();

                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.OK, Result = userVM };
            }
            catch (Exception ex)
            {
                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"There was an error authenticating the user: {ex.Message}" };
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
