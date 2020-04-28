using AutoMapper;
using Domain.RepositoryContracts;
using Domain.ServiceContracts;
using Domain.ViewModel;
using System;
using System.Collections.Generic;

namespace Business.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _repository;
        private IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ResponseEntityVM GetAll()
        {
            try
            {
                var rolList = _repository.GetAll();

                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.OK, Result = _mapper.Map<IList<RoleVM>>(rolList) };
            }
            catch (Exception ex)
            {
                return new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.InternalServerError, Message = $"There was an error getting the roles: {ex.Message}" };
            }
        }
    }
}
