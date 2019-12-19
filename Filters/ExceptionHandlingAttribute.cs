using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using TPDMS.RestApi.CustomExceptions;

namespace TPDMS.RestApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        private static readonly TelemetryClient Logger = new TelemetryClient();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            // GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new Logger.NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();

            var statusCode = ExceptionToStatusCodeMapper.GetExceptionStatusCode(actionExecutedContext.Exception);
            if (statusCode.statusCode.HasValue)
            {
                actionExecutedContext.Response = new HttpResponseMessage(statusCode.statusCode.Value);
                actionExecutedContext.Response.Content = new StringContent(statusCode.descritption);
            }
            if (actionExecutedContext.Exception is System.OperationCanceledException)
            {
                Logger.TrackEvent("operation cancelled", new Dictionary<string, string> { { "url", actionExecutedContext.Request.RequestUri.ToString() }, { "exception", actionExecutedContext.Exception.ToString() } });
            }
        }
    }
}