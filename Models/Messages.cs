namespace TPDMS.RestApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.Messages")]
    public partial class Messages
    {
        [Key]
        public int MessageID { get; set; }

        public int? DepartmentId { get; set; }

        public int EventID { get; set; }

        [Required]
        [StringLength(10)]
        public string Message_Type { get; set; }

        [StringLength(50)]
        public string EO_ID { get; set; }

        [StringLength(50)]
        public string Recall_CODE { get; set; }

        public int? Recall_Reason1 { get; set; }

        [StringLength(500)]
        public string Recall_Reason2 { get; set; }

        public string Recall_Reason3 { get; set; }

        public int MessageStatus { get; set; }

        public DateTime TimeSubmitted { get; set; }

        public byte? Aknowledgment { get; set; }

        public DateTime? AknowledgmentTime { get; set; }

        public string Message { get; set; }

        public virtual Events Events { get; set; }
    }
}