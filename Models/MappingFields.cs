namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class MappingFields
    {
        [Key]
        public int MappingFieldId { get; set; }

        public int? MappingId { get; set; }

        [StringLength(100)]
        public string SourceMapping { get; set; }

        [StringLength(100)]
        public string TargetMapping { get; set; }

        public virtual Mappings Mappings { get; set; }
    }
}