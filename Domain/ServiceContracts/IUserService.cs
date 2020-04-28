using Domain.Model;
using Domain.ViewModel;
using System;
using System.Linq.Expressions;

namespace Domain.ServiceContracts
{
    public interface IUserService
    {
        ResponseEntityVM Find(object id);

        ResponseEntityVM FindBy(Expression<Func<User, bool>> predicate);

        ResponseEntityVM GetAll();

        ResponseEntityVM Create(UserVM entity, string username);

        ResponseEntityVM Update(UserVM entity, string username);

        ResponseEntityVM Delete(object id);
    }
}
