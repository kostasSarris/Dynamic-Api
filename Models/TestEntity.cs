using Newtonsoft.Json;

namespace TPDMS.RestApi.Models
{
    public class TestEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}