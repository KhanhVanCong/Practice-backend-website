namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Blog
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public E_Blog()
        {
            E_Blog_Reply = new HashSet<E_Blog_Reply>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Title_En { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [Column(TypeName = "text")]
        public string Content_En { get; set; }

        [StringLength(200)]
        public string Picture_810x305 { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsActive { get; set; }

        public int? GroupId { get; set; }

        [StringLength(200)]
        public string DisabledReason { get; set; }

        public DateTime? DisabledAt { get; set; }

        public int? TotalView { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string Picture_370x188 { get; set; }

        public bool IsActiveHome { get; set; }

        public DateTime? UpdateAt { get; set; }

        [StringLength(200)]
        public string UpdateBy { get; set; }

        [StringLength(200)]
        public string DisabledBy { get; set; }

        public virtual E_Blog_Categories E_Blog_Categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<E_Blog_Reply> E_Blog_Reply { get; set; }
    }
}
