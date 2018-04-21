namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Blog_Reply
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public E_Blog_Reply()
        {
            E_Blog_Reply_Reply = new HashSet<E_Blog_Reply_Reply>();
        }

        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? BlogId { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(200)]
        public string DisabledReason { get; set; }

        public DateTime? DisabledAt { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string DisabledBy { get; set; }

        [StringLength(200)]
        public string ReActiveBy { get; set; }

        public DateTime? ReActiveAt { get; set; }

        public virtual E_Blog E_Blog { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<E_Blog_Reply_Reply> E_Blog_Reply_Reply { get; set; }
    }
}
