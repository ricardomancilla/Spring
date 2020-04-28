using API.Authorization;
using Domain.Model;
using Domain.ServiceContracts;
using Domain.ViewModel;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : BaseApiController
    {
        IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [ActionName("Get")]
        public async Task<IHttpActionResult> Get()
        {
            return Result(await Task.FromResult(_service.GetAll()));
        }

        [ActionName("Get")]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Result(await Task.FromResult(_service.Find(id)));
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create(UserVM entity)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder modelErrors = GetModelErrors(ModelState);
                var result = new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = modelErrors.ToString() };
                return Result(result);
            }

            return Result(await Task.FromResult(_service.Create(entity, RequestContext.Principal.Identity.Name)));
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(UserVM entity)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder modelErrors = GetModelErrors(ModelState);
                var result = new ResponseEntityVM() { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = modelErrors.ToString() };
                return Result(result);
            }

            return Result(await Task.FromResult(_service.Update(entity, RequestContext.Principal.Identity.Name)));
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            return Result(await Task.FromResult(_service.Delete(id)));
        }
    }
}
