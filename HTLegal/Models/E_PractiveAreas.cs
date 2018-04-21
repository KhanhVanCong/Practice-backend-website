namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_PractiveAreas
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Name_En { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public int? CreatedById { get; set; }

        [StringLength(200)]
        public string CreatedByName { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
