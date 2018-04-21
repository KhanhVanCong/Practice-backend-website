

namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class E_Home_FeedbackClient
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Icon_108x108 { get; set; }

        public string Content { get; set; }

        [StringLength(100)]
        public string Author { get; set; }
        
        public int Order { get; set; }
    }
}