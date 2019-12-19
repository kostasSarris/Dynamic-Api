namespace TPDMS.RestApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Mappings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mappings()
        {
            MappingFields = new HashSet<MappingFields>();
        }

        [Key]
        public int MappingId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? SourceEntityId { get; set; }

        public int? TargetEntityId { get; set; }

        [StringLength(200)]
        public string FileNameMask { get; set; }

        [StringLength(50)]
        public string FilterValue { get; set; }

        public int? ParentMappingId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MappingFields> MappingFields { get; set; }
    }
}