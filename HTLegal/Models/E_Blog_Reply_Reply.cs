namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Blog_Reply_Reply
    {
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? ReplyId { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(200)]
        public string DisabledReason { get; set; }

        [StringLength(200)]
        public string DisabledById { get; set; }

        public DateTime? DisabledAt { get; set; }

        public virtual E_Blog_Reply E_Blog_Reply { get; set; }
    }
}
