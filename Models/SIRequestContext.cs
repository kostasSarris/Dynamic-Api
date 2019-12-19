using Microsoft.Extensions.Logging;
using System;
using System.Web;

namespace TPDMS.RestApi.Models
{
    public class SIRequestContext
    {
        public SIRequestContext(ILoggerFactory loggerFactory)
        {
            this.LoggerFactory = loggerFactory;
        }

        public static readonly string RequestIdField = "RequestId";

        private string _RequestId = "";

        public string RequestId
        {
            get
            {
                if (string.IsNullOrEmpty(_RequestId))
                {
                    //κοιτάει αν υπάρχει τιμή sto HttpContext
                    var context = HttpContext.Current;
                    if (context != null)
                    {
                        if (context.Items.Contains(SIRequestContext.RequestIdField))
                        {
                            _RequestId = (string)context.Items[SIRequestContext.RequestIdField];
                        }
                    }
                    //αν συνεχίζει να μην έχει τιμή
                    if (string.IsNullOrEmpty(_RequestId))
                    {
                        _RequestId = Guid.NewGuid().ToString();
                    }
                }
                return _RequestId;
            }
        }

        public ILoggerFactory LoggerFactory { get; }
    }
}