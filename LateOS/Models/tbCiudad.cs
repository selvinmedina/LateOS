//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LateOS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbCiudad
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbCiudad()
        {
            this.tbClienteDireccion = new HashSet<tbClienteDireccion>();
        }
    
        public int ciu_Id { get; set; }
        public string ciu_Descripcion { get; set; }
        public int pais_Id { get; set; }
        public int ciu_UsuarioCrea { get; set; }
        public System.DateTime ciu_FechaCrea { get; set; }
        public Nullable<int> ciu_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> ciu_FechaModifica { get; set; }
    
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        public virtual tbPais tbPais { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbClienteDireccion> tbClienteDireccion { get; set; }
    }
}
