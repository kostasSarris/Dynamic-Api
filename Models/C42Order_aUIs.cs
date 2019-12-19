namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.42Order_aUIs")]
    public partial class C42Order_aUIs
    {
        [Key]
        public long aUIsId { get; set; }

        public long Id { get; set; }

        [StringLength(1000)]
        public string aUIs { get; set; }

        public virtual C42Order C42Order { get; set; }
    }
}