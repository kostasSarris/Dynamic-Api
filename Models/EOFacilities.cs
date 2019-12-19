namespace TPDMS.RestApi.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EMS.EOFacilities")]
    public partial class EOFacilities
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EOFacilities()
        {
            FacilityFIDs = new HashSet<FacilityFIDs>();
            Machines = new HashSet<Machines>();
        }

        [Key]
        public int FacilityID { get; set; }

        public int EconomicOperatorID { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        public int CountryId { get; set; }

        public int TypeId { get; set; }

        [StringLength(500)]
        public string TypeOther { get; set; }

        public byte StatusId { get; set; }

        public byte ExciseNumber1 { get; set; }

        [StringLength(50)]
        public string ExciseNumber2 { get; set; }

        public byte OtherFID_R { get; set; }

        public byte Reg_3RD { get; set; }

        [StringLength(50)]
        public string Reg_EOID { get; set; }

        public int? DepartmentId { get; set; }

        public virtual EconomicOperators EconomicOperators { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FacilityFIDs> FacilityFIDs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Machines> Machines { get; set; }
    }
}