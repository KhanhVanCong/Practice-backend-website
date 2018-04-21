namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Home_Content
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Welcome { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Info_1 { get; set; }

        [StringLength(500)]
        public string Info_2 { get; set; }

        [StringLength(500)]
        public string Image_875x510_RightSubmit { get; set; }

        [StringLength(200)]
        public string Title_FeatureServices { get; set; }

        [StringLength(200)]
        public string Title_Attorney_Area { get; set; }

        [StringLength(200)]
        public string Title_Blog_Area { get; set; }

        [StringLength(200)]
        public string Title_Form_Submit { get; set; }

        [StringLength(500)]
        public string Img_1920x329_Feedback { get; set; }
    }
}
