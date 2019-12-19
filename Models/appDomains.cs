namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.appDomains")]
    public partial class appDomains
    {
        [Key]
        public int DomainID { get; set; }

        [StringLength(100)]
        public string DescriptionEN { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public byte IsSystem { get; set; }

        public int? OwnerSchemaID { get; set; }
    }
}