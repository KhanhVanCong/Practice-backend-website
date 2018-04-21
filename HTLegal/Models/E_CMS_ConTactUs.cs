namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_CMS_ConTactUs
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(200)]
        public string SubTitle { get; set; }

        [StringLength(500)]
        public string Logo_1920x400 { get; set; }

        [StringLength(500)]
        public string Content { get; set; }

        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(200)]
        public string Picture_141x141 { get; set; }

        [StringLength(100)]
        public string Phone { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Facebook { get; set; }

        [StringLength(200)]
        public string Twifter { get; set; }

        [StringLength(200)]
        public string Google { get; set; }

        [StringLength(500)]
        public string Position { get; set; }

        public string Map { get; set; }
    }
}
