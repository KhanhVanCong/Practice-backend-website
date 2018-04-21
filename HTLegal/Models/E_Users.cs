namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Users
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(200)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string CellPhone { get; set; }

        public bool? IsActive { get; set; }

        public int? RolesId { get; set; }

        [StringLength(200)]
        public string RolesName { get; set; }

        public DateTime? RegisterAt { get; set; }

        [StringLength(200)]
        public string BannedBy { get; set; }

        public DateTime? BannedAt { get; set; }

        [StringLength(50)]
        public string LangCode { get; set; }

        public virtual E_Attorneys E_Attorneys { get; set; }
    }
}
