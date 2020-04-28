using Domain.ServiceContracts;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    public class RoleController : BaseApiController
    {
        IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [ActionName("Get")]
        public async Task<IHttpActionResult> Get()
        {
            return Result(await Task.FromResult(_service.GetAll()));
        }
    }
}
