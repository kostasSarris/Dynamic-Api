namespace TPDMS.RestApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class admUsers
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(40)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public byte Active { get; set; }

        public DateTime CreationDate { get; set; }

        [StringLength(15)]
        public string IP { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [StringLength(150)]
        public string email { get; set; }

        public int? PersonnelID { get; set; }

        public byte? PasswordReset { get; set; }

        public DateTime? LastPasswordDate { get; set; }

        public int? Attempt { get; set; }

        public int? OwnerSchemaID { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public bool EmailConfirmed { get; set; }

        [StringLength(20)]
        public string UserToken { get; set; }

        public int? UserAvatarId { get; set; }
    }
}