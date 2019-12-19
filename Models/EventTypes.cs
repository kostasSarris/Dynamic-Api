namespace TPDMS.RestApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.EventTypes")]
    public partial class EventTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventTypes()
        {
            Events = new HashSet<Events>();
        }

        [Key]
        public int EventTypeId { get; set; }

        public int? EntityId { get; set; }

        public int? IndustrySectorId { get; set; }

        public int? ProcessId { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual Entities Entities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Events> Events { get; set; }

        public virtual IndustrySectors IndustrySectors { get; set; }

        public virtual Processes Processes { get; set; }
    }
}