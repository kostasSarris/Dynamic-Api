namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.Vehicles")]
    public partial class Vehicles
    {
        [Key]
        public int VehicleID { get; set; }

        public int EconomicOperatorID { get; set; }

        [Required]
        [StringLength(50)]
        public string Transport_vehicle { get; set; }

        public virtual EconomicOperators EconomicOperators { get; set; }
    }
}