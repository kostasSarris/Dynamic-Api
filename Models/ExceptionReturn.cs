using System.Net;

namespace TPDMS.RestApi.Models
{
    public class ExceptionReturn
    {
        public HttpStatusCode? statusCode { get; set; }

        public string descritption { get; set; }
    }
}