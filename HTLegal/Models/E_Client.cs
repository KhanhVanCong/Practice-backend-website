namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Client
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Icon_143x98 { get; set; }

        public string Link { get; set; }
    }
}
