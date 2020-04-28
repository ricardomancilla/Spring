using AutoMapper;
using Domain.Model;
using Domain.ViewModel;

namespace Business.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserVM>();
            CreateMap<User, AuthUserVM>();
            CreateMap<Role, RoleVM>();
        }
    }
}
