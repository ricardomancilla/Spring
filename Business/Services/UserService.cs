using AutoMapper;
using Domain.Model;
using Domain.RepositoryContracts;
using Domain.ServiceContracts;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;
        private IRoleRepository _rolRepository;
        private IUserRoleRepository _userRolRepository;
        private IMapper _mapper;

        public UserService(IUserRepository repository, IUserRoleRepository userRolRepository, IRoleRepository rolRepository, IMapper mapper)
        {
            _repository = repository;
            _userRolRepository = userRolRepository;
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public ResponseEntityVM Create(UserVM entity, string username)
        {
            try
            {
                if (ValidateIfExists(entity))
                    return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.Forbidden, Message = $"Already exists a user with the email {entity.Email} or username {entity.Username}" };

                var timestamp = DateTime.Now;

                UserRole userRol = new UserRole()
                {
                    User = new User()
                    {
                        Username = entity.Username,
                        Email = entity.Email,
                        Password = entity.Password,
                        CreateDtm = timestamp,
                        CreateUsr = username
                    },
                    RoleId = entity.Role.RoleId,
                    CreateDtm = timestamp,
                    CreateUsr = username
                };

                CreatePasswordHashSalt(entity.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

                userRol.User.PasswordHash = PasswordHash;
                userRol.User.PasswordSalt = PasswordSalt;

                var entityResult = _userRolRepository.Insert(userRol);

                _userRolRepository.SaveChanges();

                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.Created, Result = entityResult };
            }
            catch (Exception ex)
            {
                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"There was an error creating the user: {ex.Message}" };
            }
        }

        public ResponseEntityVM Delete(object id)
        {
            try
            {
                var user = _repository.Find(id);

                if (user == null)
                    return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.NotFound };

                _repository.Delete(user);
                _repository.SaveChanges();

                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.NoContent };
            }
            catch (Exception ex)
            {
                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"There was an error deleting the user: {ex.Message}" };
            }
        }

        public ResponseEntityVM Find(object id)
        {
            try
            {
                var user = _repository.Find(id);

                if (user == null)
                    return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.NotFound };

                var userVM = _mapper.Map<UserVM>(user);

                userVM.Role = _userRolRepository.FindRolesByUserId(x => x.UserId.Equals(user.UserId)).Select(x => new RoleVM() { RoleId = x.RoleId, RoleName = x.Role.RoleName }).FirstOrDefault();

                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.OK, Result = userVM };
            }
            catch (Exception ex)
            {
                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"There was an error getting the user: {ex.Message}" };
            }
        }

        public ResponseEntityVM FindBy(Expression<Func<User, bool>> predicate)
        {
            try
            {
                var userList = _repository.FindBy(predicate).ToList();

                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.OK, Result = _mapper.Map<IList<UserVM>>(userList) };
            }
            catch (Exception ex)
            {
                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"There was an error getting the users: {ex.Message}" };
            }
        }

        public ResponseEntityVM GetAll()
        {
            try
            {
                var userList = _repository.GetAll();

                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.OK, Result = _mapper.Map<IList<UserVM>>(userList) };
            }
            catch (Exception ex)
            {
                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"There was an error getting the users: {ex.Message}" };
            }
        }

        public ResponseEntityVM Update(UserVM entity, string username)
        {
            try
            {
                if (ValidateIfExists(entity))
                    return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.Forbidden, Message = $"Already exists a user with the email {entity.Email} or username {entity.Username}" };

                var currentUser = _repository.Find(entity.UserId);
                currentUser.Username = entity.Username;
                currentUser.Email = entity.Email;
                currentUser.UpdateDtm = DateTime.Now;
                currentUser.UpdateUsr = username;

                var currentUserRol = _userRolRepository.FindBy(x => x.UserId == entity.UserId).FirstOrDefault();

                //Update role
                if (currentUserRol.RoleId != entity.Role.RoleId)
                {
                    currentUserRol.RoleId = entity.Role.RoleId;

                    _userRolRepository.Update(currentUserRol);
                    _userRolRepository.SaveChanges();
                }

                _repository.Update(currentUser);
                _repository.SaveChanges();

                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.NoContent };
            }
            catch (Exception ex)
            {
                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"There was an error updating the user: {ex.Message}" };
            }
        }

        private bool ValidateIfExists(UserVM entity)
        {
            return _repository.FindBy(x => (x.Email.Equals(entity.Email) || x.Username.Equals(entity.Username)) &&
                                           (entity.UserId > 0 ? !x.UserId.Equals(entity.UserId) : true)).Any();
        }

        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}