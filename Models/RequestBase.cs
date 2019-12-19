using Newtonsoft.Json;

namespace TPDMS.RestApi.Models
{
    public class RequestBase
    {
        [JsonProperty(propertyName: "auth")]
        public AuthToken Token { get; set; }

        [JsonProperty(propertyName: "appStatus")]
        public DeviceStatus DeviceStatus { get; set; }

        [JsonProperty(propertyName: "dataVersion")]
        public long RowVersion { get; set; }
    }
}