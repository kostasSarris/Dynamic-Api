namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class EntityFields
    {
        [Key]
        public int EntityFieldId { get; set; }

        public int? EntityId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? DataTypeId { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(1)]
        public string Cardinality { get; set; }

        public int? FieldOrder { get; set; }

        public byte? Required { get; set; }

        [StringLength(512)]
        public string RequiredConditions { get; set; }

        public int? LookupId { get; set; }

        public int? MaxLength { get; set; }

        [StringLength(50)]
        public string Expression { get; set; }

        public virtual DataTypes DataTypes { get; set; }

        public virtual Entities Entities { get; set; }
    }
}