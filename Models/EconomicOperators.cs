namespace TPDMS.RestApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.EconomicOperators")]
    public partial class EconomicOperators
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EconomicOperators()
        {
            EOFacilities = new HashSet<EOFacilities>();
            EOIDs = new HashSet<EOIDs>();
            Traders = new HashSet<Traders>();
            Traders1 = new HashSet<Traders>();
            Vehicles = new HashSet<Vehicles>();
        }

        [Key]
        public int EconomicOperatorID { get; set; }

        [StringLength(50)]
        public string EO_ID { get; set; }

        [StringLength(50)]
        public string EO_CODE { get; set; }

        [StringLength(500)]
        public string EO_Name1 { get; set; }

        [StringLength(500)]
        public string EO_Name2 { get; set; }

        [StringLength(500)]
        public string EO_Address { get; set; }

        public int? EO_CountryReg { get; set; }

        [StringLength(50)]
        public string EO_Email { get; set; }

        public int? VAT_R { get; set; }

        [StringLength(50)]
        public string VAT_N { get; set; }

        [StringLength(50)]
        public string TAX_N { get; set; }

        public int? EO_ExciseNumber1 { get; set; }

        [StringLength(50)]
        public string EO_ExciseNumber2 { get; set; }

        public int? OtherEOID_R { get; set; }

        public int? Reg_3RD { get; set; }

        [StringLength(50)]
        public string Reg_EOID { get; set; }

        public int? DepartmentId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EOFacilities> EOFacilities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EOIDs> EOIDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Traders> Traders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Traders> Traders1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicles> Vehicles { get; set; }
    }
}