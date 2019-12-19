namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.FacilityFIDs")]
    public partial class FacilityFIDs
    {
        [Key]
        public int FacilityFIDID { get; set; }

        public int FacilityID { get; set; }

        [Required]
        [StringLength(50)]
        public string F_ID { get; set; }

        public byte Main { get; set; }

        public virtual EOFacilities EOFacilities { get; set; }
    }
}