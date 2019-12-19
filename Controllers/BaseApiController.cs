using Microsoft.ApplicationInsights;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TPDMS.RestApi.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class BaseApiController : ApiController
    {
        internal TelemetryClient Log { get; } = new TelemetryClient();
    }
}