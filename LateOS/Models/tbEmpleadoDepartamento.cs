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
    
    public partial class tbEmpleadoDepartamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEmpleadoDepartamento()
        {
            this.tbEmpleadoCargo = new HashSet<tbEmpleadoCargo>();
        }
    
        public int empd_Id { get; set; }
        public string empd_Descripcion { get; set; }
        public int empd_UsuarioCrea { get; set; }
        public System.DateTime empd_FechaCrea { get; set; }
        public Nullable<int> empd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> empd_FechaModifica { get; set; }
    
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleadoCargo> tbEmpleadoCargo { get; set; }
    }
}
