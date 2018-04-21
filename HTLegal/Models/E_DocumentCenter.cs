namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_DocumentCenter
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Title_En { get; set; }

        public string Description { get; set; }

        public string Description_En { get; set; }

        [StringLength(200)]
        public string AttachFiles { get; set; }

        public bool? Public { get; set; }

        public bool? MultiFiles { get; set; }
    }
}
