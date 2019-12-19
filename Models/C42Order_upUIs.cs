namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.42Order_upUIs")]
    public partial class C42Order_upUIs
    {
        [Key]
        public long upUIsId { get; set; }

        public long Id { get; set; }

        [StringLength(1000)]
        public string upUIs { get; set; }

        public virtual C42Order C42Order { get; set; }
    }
}