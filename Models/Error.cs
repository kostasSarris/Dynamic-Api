using System.Collections.Generic;

namespace TPDMS.RestApi.Models
{
    public class Error
    {
        public bool HasWarning { get; set; }
        public IDictionary<string, string> WarningMessages { get; set; }
    }
}