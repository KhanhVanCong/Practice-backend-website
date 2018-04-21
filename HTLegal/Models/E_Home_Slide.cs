namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Home_Slide
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Logo_1920x940_Slide { get; set; }

        [StringLength(200)]
        public string Title_Slide { get; set; }

        [StringLength(200)]
        public string SubTitle_Slide { get; set; }
    }
}
