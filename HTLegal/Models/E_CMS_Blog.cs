namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_CMS_Blog
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Logo_1920x400 { get; set; }
    }
}
