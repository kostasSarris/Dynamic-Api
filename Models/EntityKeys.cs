namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class EntityKeys
    {
        [Key]
        public int EntityKeyId { get; set; }

        public int? EntityId { get; set; }

        public int? KeyIndex { get; set; }

        public int? FieldId { get; set; }
    }
}