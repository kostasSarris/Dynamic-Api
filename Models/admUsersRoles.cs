namespace TPDMS.RestApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class admUsersRoles
    {
        [Key]
        public int UsersRolesID { get; set; }

        public int RoleID { get; set; }

        public byte? Active { get; set; }

        public int? PositionID { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }
    }
}