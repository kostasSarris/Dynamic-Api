namespace TPDMS.RestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.42Order")]
    public partial class C42Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C42Order()
        {
            C42Order_aUIs = new HashSet<C42Order_aUIs>();
            C42Order_upUIs = new HashSet<C42Order_upUIs>();
        }

        public long Id { get; set; }

        public DateTime? File_Date { get; set; }

        [StringLength(1000)]
        public string aUIs { get; set; }

        public DateTime? Order_Date { get; set; }

        [StringLength(1000)]
        public string EO_ID { get; set; }

        public int? UI_Type { get; set; }

        [StringLength(1000)]
        public string Order_comment { get; set; }

        [StringLength(1000)]
        public string File_ID { get; set; }

        [StringLength(1000)]
        public string Invoice_Number { get; set; }

        [StringLength(1000)]
        public string Message_Type { get; set; }

        [StringLength(1000)]
        public string Order_Number { get; set; }

        public DateTime? Event_Time { get; set; }

        [StringLength(1000)]
        public string upUIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C42Order_aUIs> C42Order_aUIs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<C42Order_upUIs> C42Order_upUIs { get; set; }
    }
}