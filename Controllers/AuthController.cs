using Serilog;
using System;
using System.Linq;
using System.Web.Http;
using TPDMS.DataLayer;
using TPDMS.RestApi.Models;

namespace TPDMS.RestApi.Controllers
{
    public class AuthController : ApiController
    {
        // POST api/auth/token
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Token([FromBody]TokenRequest request)
        {
            try
            {
                var auth = new AuthenticationModule();
                var token = auth.GenerateTokenForAudience(request.audienceName, request.secret);
                if (string.IsNullOrWhiteSpace(token))
                {
                    return BadRequest("Not authorized.Invalid credentials.Please try again.");
                }
                Log.Information("User {UserName} requests token.", request.audienceName);
                using (var dbContext = new TPDMSDbContext(WebApiConfig.Options))
                {
                    var SpecificUser = dbContext.admUsers.Where(x => x.Username == request.audienceName).FirstOrDefault();
                    return Ok(new { token, expiresin = SpecificUser.TokenDuration });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return InternalServerError();
            }
        }
    }
}