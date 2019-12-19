using Newtonsoft.Json;

namespace TPDMS.RestApi.Models
{
    public class AuthToken

    {
        [JsonProperty(propertyName: "devId")]
        public int? DeviceId { get; set; }

        [JsonProperty(propertyName: "imei")]
        public string Imei { get; set; }
    }
}