namespace HTLegal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class E_Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public E_Roles()
        {
            E_AccessFunctionRole = new HashSet<E_AccessFunctionRole>();
        }

        public int Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public int? RoleLevel { get; set; }

        public bool? Default { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<E_AccessFunctionRole> E_AccessFunctionRole { get; set; }
    }
}
