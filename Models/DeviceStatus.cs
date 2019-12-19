using Newtonsoft.Json;
using SI.Data.Adapters;

namespace TPDMS.RestApi.Models
{
    public class DeviceStatus
    {
        protected DeviceStatus()
        {
            using (var adapter = new SimpleAdapter())
            {
                //this.DataVersion = adapter.ExecuteScalar<int>("SELECT cast(@@DBTS as bigint)");
                this.ElectroralGroupsDataVersion = adapter.ExecuteScalar<int>("SELECT cast(MAX([RowVersion]) as bigint) FROM [dbo].[ElectoralGroups]");
                this.CandidatesDataVersion = adapter.ExecuteScalar<int>("SELECT cast(MAX([RowVersion]) as bigint) FROM [dbo].[Candidates]");
            }
        }

        [JsonProperty(propertyName: "version")]
        public string Version { get; set; }

        [JsonProperty(propertyName: "tmId")]
        public string TM_ID { get; set; }

        [JsonProperty(propertyName: "groupsDataVersion")]
        public int ElectroralGroupsDataVersion { get; set; }

        [JsonProperty(propertyName: "candidatesDataVersion")]
        public int CandidatesDataVersion { get; set; }
    }
}