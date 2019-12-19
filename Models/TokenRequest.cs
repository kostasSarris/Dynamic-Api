namespace TPDMS.RestApi.Models
{
    public class TokenRequest
    {
        public string audienceName { get; set; }
        public string secret { get; set; }
    }
}