namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_CMS_Attorney
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string Title_En { get; set; }

        public string Comment { get; set; }

        public string Comment_En { get; set; }

        [StringLength(200)]
        public string Logo_1920x400 { get; set; }

        [StringLength(200)]
        public string SubTitle { get; set; }

        [StringLength(500)]
        public string Content { get; set; }
    }
}
