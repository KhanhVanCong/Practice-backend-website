namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_AccessFunctionInPage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public E_AccessFunctionInPage()
        {
            E_AccessFunctionRole = new HashSet<E_AccessFunctionRole>();
        }

        [Key]
        [StringLength(50)]
        public string FunctionCode { get; set; }

        [Required]
        [StringLength(50)]
        public string PageCode { get; set; }

        [StringLength(200)]
        public string FunctionName { get; set; }

        public virtual E_AccessPage E_AccessPage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<E_AccessFunctionRole> E_AccessFunctionRole { get; set; }
    }
}
