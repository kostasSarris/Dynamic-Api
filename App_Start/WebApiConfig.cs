using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TPDMS.RestApi
{
    public static partial class WebApiConfig
    {
        public static DbContextOptions<SI.Abstractions.DbContext> Options { get; set; }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var optionsBuilder = new DbContextOptionsBuilder<SI.Abstractions.DbContext>();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["TPDMSDbModel"].ConnectionString);
            Options = optionsBuilder.Options;

            config.Filters.Add(new Filters.ExceptionHandlingAttribute());

            // Web API routes

            // pl: Allow cross domain ajax calls by enabling CORS
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}