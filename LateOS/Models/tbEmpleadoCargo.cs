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
    
    public partial class tbEmpleadoCargo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEmpleadoCargo()
        {
            this.tbEmpleado = new HashSet<tbEmpleado>();
        }
    
        public int empc_Id { get; set; }
        public string empc_Descripcion { get; set; }
        public int empd_Id { get; set; }
        public int empc_UsuarioCrea { get; set; }
        public System.DateTime empc_FechaCrea { get; set; }
        public Nullable<int> empc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> empc_FechaModifica { get; set; }
    
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbEmpleado> tbEmpleado { get; set; }
        public virtual tbEmpleadoDepartamento tbEmpleadoDepartamento { get; set; }
    }
}
