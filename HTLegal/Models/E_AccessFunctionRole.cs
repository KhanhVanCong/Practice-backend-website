namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_AccessFunctionRole
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FunctionCode { get; set; }

        public int? RoleId { get; set; }

        public bool? Access { get; set; }

        public virtual E_AccessFunctionInPage E_AccessFunctionInPage { get; set; }

        public virtual E_Roles E_Roles { get; set; }
    }
}
