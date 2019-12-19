using Newtonsoft.Json.Linq;
using TPDMS.RestApi.Models;

namespace TPDMS.RestApi.services
{
    public interface IMapping
    {
        void DeclareObject(JObject entity, string user, string entityName = null, Error error = null);
    }
}