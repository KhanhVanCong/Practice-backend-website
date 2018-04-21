namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_UploadMoreFiles
    {
        public int Id { get; set; }

        public int? TableId { get; set; }

        [StringLength(200)]
        public string TableName { get; set; }

        [StringLength(200)]
        public string FileName { get; set; }
    }
}
