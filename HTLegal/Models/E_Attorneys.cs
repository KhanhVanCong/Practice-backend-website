namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Attorneys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [StringLength(200)]
        public string Picture_270x288 { get; set; }

        [StringLength(200)]
        public string Picture_470x638 { get; set; }

        [StringLength(200)]
        public string PractiveAreas { get; set; }

        public string ProfessionalExperiences { get; set; }

        public string ProfessionalExperiences_En { get; set; }

        [StringLength(200)]
        public string Facebook { get; set; }

        [StringLength(200)]
        public string Twifter { get; set; }

        [StringLength(200)]
        public string Google { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string Position { get; set; }

        public bool IsActiceHome { get; set; }

        public virtual E_Users E_Users { get; set; }
    }
}
