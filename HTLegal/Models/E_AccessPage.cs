namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_AccessPage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public E_AccessPage()
        {
            E_AccessFunctionInPage = new HashSet<E_AccessFunctionInPage>();
        }

        [Key]
        [StringLength(50)]
        public string PageCode { get; set; }

        [StringLength(200)]
        public string PageName { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int? Order { get; set; }

        [StringLength(200)]
        public string Link { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<E_AccessFunctionInPage> E_AccessFunctionInPage { get; set; }
    }
}
