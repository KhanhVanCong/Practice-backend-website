namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_CMS_AboutUs
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Organization { get; set; }

        [StringLength(200)]
        public string Logo { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(200)]
        public string Hotline { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Website { get; set; }

        [StringLength(500)]
        public string Introduce { get; set; }

        [StringLength(200)]
        public string Introduce_En { get; set; }

        [StringLength(200)]
        public string Logo_1920x400 { get; set; }

        [StringLength(200)]
        public string Picture_Left { get; set; }

        [StringLength(200)]
        public string Picture_Right { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string SubTitle { get; set; }
    }
}
