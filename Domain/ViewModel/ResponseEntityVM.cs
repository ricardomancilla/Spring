using System.Net;

namespace Domain.ViewModel
{
    public class ResponseEntityVM
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
