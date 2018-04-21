namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Services
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        [StringLength(500)]
        public string Image_374x219 { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsActiveHome { get; set; }
    }
}
