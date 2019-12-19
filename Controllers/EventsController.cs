using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TPDMS.RestApi.Models;
using TPDMS.RestApi.services;

namespace TPDMS.RestApi.Controllers
{
    public class EventsController : BaseApiController
    {

        [JWTAuthenticationFilter]
        [HttpPost]
        [Route("api/events/{entityName}")]
        public HttpResponseMessage PostEntity([FromUri] string entityName, [FromBody]JObject entity)
        {
            var authorize = this.Request.Headers.Authorization.Parameter;
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(authorize);
            var user = token.Subject;
            try
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                var error = new Error() { HasWarning = false };
                var mapping = new Mapping();
                mapping.DeclareObject(entity, user, entityName, error);
                if (!error.HasWarning) return result;
                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(error))
                };
                return result;
            }
            catch (NullReferenceException ex)
            {
                Serilog.Log.Error("Response Message {Exception}{EntityName}{UserName}{Data})", ex, entityName, user, entity.ToString(Formatting.None));
                throw;
            }
        }
    }
}