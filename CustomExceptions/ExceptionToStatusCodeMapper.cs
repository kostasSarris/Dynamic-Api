using System;
using System.Linq;
using System.Net;
using TPDMS.RestApi.Models;

namespace TPDMS.RestApi.CustomExceptions
{
    public static class ExceptionToStatusCodeMapper
    {
        private static readonly ExceptionMapping[] Mappings = new ExceptionMapping[]
            {
                // BAD REQUESTS - 400
                new ExceptionMapping(typeof(System.Runtime.Serialization.SerializationException), HttpStatusCode.BadRequest),
                new ExceptionMapping(typeof(Newtonsoft.Json.JsonSerializationException), HttpStatusCode.BadRequest),
                new ExceptionMapping(typeof(JsonSchemaException), HttpStatusCode.BadRequest),
                new ExceptionMapping(typeof(MandatoryArgumentException), HttpStatusCode.BadRequest),
                new ExceptionMapping(typeof(FieldSizeViolationException), HttpStatusCode.BadRequest),
                new ExceptionMapping(typeof(WrongTypeException), HttpStatusCode.BadRequest),

                // METHOD NOT ALLOWED - 405
                new ExceptionMapping(typeof(NotImplementedException), HttpStatusCode.MethodNotAllowed),
                new ExceptionMapping(typeof(UnsupportedOperationException), HttpStatusCode.MethodNotAllowed),

                // INTERNAL SERVER ERROR - 500
                new ExceptionMapping(typeof(System.Exception), HttpStatusCode.InternalServerError),
            };

        public static ExceptionReturn GetExceptionStatusCode(Exception exception)
        {
            var mapping = Mappings.FirstOrDefault(m => m.ExceptionType.IsAssignableFrom(exception.GetType()));
            var exceptionReturn = new ExceptionReturn();
            if (mapping != default(ExceptionMapping))
            {
                exceptionReturn.statusCode = mapping.StatusCode;
                exceptionReturn.descritption = exception.Message;
                return exceptionReturn;
            }

            return null;
        }

        private class ExceptionMapping
        {
            public ExceptionMapping(Type exceptionType, HttpStatusCode statusCode)
            {
                if (exceptionType == null)
                {
                    throw new ArgumentNullException("exceptionType");
                }
                else if (!typeof(System.Exception).IsAssignableFrom(exceptionType))
                {
                    throw new ArgumentException("exceptionType must derive from System.Exception");
                }

                this.ExceptionType = exceptionType;
                this.StatusCode = statusCode;
            }

            public Type ExceptionType { get; private set; }

            public HttpStatusCode StatusCode { get; private set; }
        }
    }
}