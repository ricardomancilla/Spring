using Domain.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Web.Http;

namespace API.Controllers
{
    public class BaseApiController : ApiController
    {
        protected StringBuilder GetModelErrors(System.Web.Http.ModelBinding.ModelStateDictionary ModelState)
        {
            StringBuilder modelErrors = new StringBuilder();
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    modelErrors.AppendLine(error.ErrorMessage);
                }
            }
            return modelErrors;
        }

        protected IHttpActionResult Result(ResponseEntityVM responseEntity)
        {
            switch (responseEntity.StatusCode)
            {
                case (HttpStatusCode.OK):
                case (HttpStatusCode.Created):
                    return Content(responseEntity.StatusCode, JsonConvert.SerializeObject(responseEntity.Result));
                case (HttpStatusCode.NoContent):
                case (HttpStatusCode.NotFound):
                    return StatusCode(responseEntity.StatusCode);
                case (HttpStatusCode.BadRequest):
                case (HttpStatusCode.Forbidden):
                case (HttpStatusCode.Conflict):
                case (HttpStatusCode.InternalServerError):
                    return Content(responseEntity.StatusCode, responseEntity.Message);
                default:
                    return Ok();
            }
        }
    }
}
