namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.Traders")]
    public partial class Traders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TraderID { get; set; }

        public int? Seller { get; set; }

        public int? Buyer { get; set; }

        public string Comments { get; set; }

        public virtual EconomicOperators EconomicOperators { get; set; }

        public virtual EconomicOperators EconomicOperators1 { get; set; }
    }
}