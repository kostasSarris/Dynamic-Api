using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TPDMS.DataLayer;
using TPDMS.RestApi.CustomExceptions;
using TPDMS.RestApi.Models;

namespace TPDMS.RestApi
{
    public class JWTAuthenticationIdentity : GenericIdentity
    {
        public const string JwtAuthenticationType = "Bearer";

        public JWTAuthenticationIdentity(string audienceName) : base(audienceName, JwtAuthenticationType)
        {
            AudienceName = audienceName;
        }

        public override string AuthenticationType => JwtAuthenticationType;
        public string AudienceName { get; set; }
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }

    public class JWTAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            try
            {
                if (!IsUserAuthorized(filterContext))
                {
                    ShowAuthenticationError(filterContext);
                    return;
                }
                base.OnAuthorization(filterContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        private static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            var response = new UnsupportedOperationException("You need to add token properly in order to call API");
            filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { Content = new StringContent(JsonConvert.SerializeObject(response)) };
        }

        private string FetchFromHeader(HttpActionContext actionContext)
        {
            try
            {
                string authHeader = actionContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
                return authHeader?.Replace($"{JWTAuthenticationIdentity.JwtAuthenticationType} ", "");
            }
            catch { return null; }
        }

        public bool IsUserAuthorized(HttpActionContext actionContext)
        {
            try
            {
                var authHeader = FetchFromHeader(actionContext);
                if (!string.IsNullOrWhiteSpace(authHeader))
                {
                    var auth = new AuthenticationModule();
                    JwtSecurityToken userPayloadToken = auth.GenerateAudienceClaimFromJWT(authHeader);
                    if (userPayloadToken != null)
                    {
                        var identity = auth.PopulateUserIdentity(userPayloadToken);
                        string[] roles = identity.Roles ?? new string[0];
                        var genericPrincipal = new GenericPrincipal(identity, roles);
                        Thread.CurrentPrincipal = genericPrincipal;
                        var authenticationIdentity = Thread.CurrentPrincipal.Identity as JWTAuthenticationIdentity;
                        if (authenticationIdentity != null && !String.IsNullOrEmpty(authenticationIdentity.AudienceName))
                        {
                            authenticationIdentity.UserId = identity.UserId;
                            authenticationIdentity.AudienceName = identity.AudienceName;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw new UnsupportedOperationException("You need to add token properly in order to call API");
            }
            return false;
        }
    }

    public class AuthenticationModule
    {
        private string CreateToken(string audienceName, string secret, string issuer, int? expiresInMinutes)
        {
            if (string.IsNullOrWhiteSpace(audienceName) || string.IsNullOrWhiteSpace(secret))
                return null;
            var token = (string)null;
            var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, audienceName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                     };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var jwtToken = new JwtSecurityToken(issuer: issuer,
                audience: audienceName,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes((double)expiresInMinutes),
                signingCredentials: creds);

            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        private TokenValidationParameters GetValidationParametersForAudience(string audienceName)
        {
            string signingkey = null;
            using (var context = new TPDMSDbModel())
            {
                signingkey = context.admUsers.Where(u => u.Username == audienceName)
                   .Select(u => u.Password).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(signingkey))
                {
                    return null;
                }
            }
            signingkey = $"{signingkey}:{audienceName}";
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = new string[] { audienceName },
                ValidIssuers = new string[] { "self", },
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingkey))
            };
            return tokenValidationParameters;
        }

        public string GenerateTokenForAudience(string audienceName, string secret)
        {
            //audienceName = username
            //Το secret πρέπει να ειναι base64 string του Sha512 του secret(password) του audience
            string password = string.Empty;
            using (var context = new TPDMSDbModel())
            {
                password = context.admUsers.Where(u => u.Username == audienceName)
                   .Select(u => u.Password).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(secret))
                {
                    return null;
                }
            }
            var result = SI.Identity.Helpers.SecurityHelper.GetHashedPassword(audienceName, secret);
            if (result != password)
            {
                return null;
            }

            //secret = password:audienceName
            secret = $"{result}:{audienceName}";
            using (var dbContext = new TPDMSDbContext(WebApiConfig.Options))
            {
                var SpecificUser = dbContext.admUsers.FirstOrDefault(x => x.Username == audienceName && x.Password == password);
                //TODO: Να πάρουμε τον issuer και το expiresInMinutes από configuration
                int tokenDuration = SpecificUser.TokenDuration ?? 30;
                return CreateToken(audienceName, secret, "self", tokenDuration);
            }
        }

        /// Using the same key used for signing token, user payload is generated back
        public JwtSecurityToken GenerateAudienceClaimFromJWT(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            try
            {
                //Διαβάζουμε το audienceName από το token χωρίς authentication
                securityToken = tokenHandler.ReadToken(authToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                    return null;
                var audienceName = jwtSecurityToken.Subject;
                //παίρνουμε τα validation parameters με τα οποία χτίσαμε το token
                var tokenValidationParameters = GetValidationParametersForAudience(audienceName);

                tokenHandler.ValidateToken(authToken, tokenValidationParameters, out securityToken);
            }
            catch (Exception)
            {
                return null;
            }

            return securityToken as JwtSecurityToken;
        }

        public JWTAuthenticationIdentity PopulateUserIdentity(JwtSecurityToken userPayloadToken)
        {
            string name = userPayloadToken.Subject;
            string userId = userPayloadToken.Claims.FirstOrDefault(cl => cl.Type == JwtRegisteredClaimNames.Jti)?.Value;
            string[] roles = userPayloadToken.Claims.Where(cl => cl.Type == ClaimTypes.Role).Select(cl => cl.Value).ToArray();
            return new JWTAuthenticationIdentity(name) { UserId = userId, AudienceName = name, Roles = roles };
        }
    }
}