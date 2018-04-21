namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_ViewerMonitor
    {
        public int Id { get; set; }

        public int? ViewInDay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AtDate { get; set; }
    }
}
