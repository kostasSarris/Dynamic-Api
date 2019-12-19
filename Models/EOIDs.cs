namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.EOIDs")]
    public partial class EOIDs
    {
        [Key]
        public int EOID_ID { get; set; }

        public int EconomicOperatorID { get; set; }

        [StringLength(50)]
        public string EO_ID { get; set; }

        public byte Main { get; set; }

        public virtual EconomicOperators EconomicOperators { get; set; }
    }
}