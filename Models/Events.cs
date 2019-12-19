namespace TPDMS.RestApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.Events")]
    public partial class Events
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Events()
        {
            Events1 = new HashSet<Events>();
            Messages = new HashSet<Messages>();
        }

        [Key]
        public int EventId { get; set; }

        public int EntityId { get; set; }

        public int? RelatedWith { get; set; }

        public int EventTypeId { get; set; }

        public int? DepartmentId { get; set; }

        public string Data { get; set; }

        public virtual Entities Entities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Events> Events1 { get; set; }

        public virtual Events Events2 { get; set; }

        public virtual EventTypes EventTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Messages> Messages { get; set; }
    }
}