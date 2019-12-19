namespace TPDMS.RestApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DataTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DataTypes()
        {
            DataTypes1 = new HashSet<DataTypes>();
            EntityFields = new HashSet<EntityFields>();
        }

        [Key]
        public int DataTypeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [StringLength(2000)]
        public string Example { get; set; }

        public int? BaseDataTypeId { get; set; }

        [StringLength(512)]
        public string RegEx { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DataTypes> DataTypes1 { get; set; }

        public virtual DataTypes DataTypes2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EntityFields> EntityFields { get; set; }
    }
}