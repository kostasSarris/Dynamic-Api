namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.Machines")]
    public partial class Machines
    {
        [Key]
        public int MachineID { get; set; }

        public int FacilityID { get; set; }

        [Required]
        [StringLength(50)]
        public string MID { get; set; }

        [Required]
        [StringLength(500)]
        public string Producer { get; set; }

        [Required]
        [StringLength(500)]
        public string Model { get; set; }

        [Required]
        [StringLength(50)]
        public string Number { get; set; }

        public int Capacity { get; set; }

        public virtual EOFacilities EOFacilities { get; set; }
    }
}