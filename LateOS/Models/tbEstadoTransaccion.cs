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
    
    public partial class tbEstadoTransaccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbEstadoTransaccion()
        {
            this.tbTransaccionDetalle = new HashSet<tbTransaccionDetalle>();
        }
    
        public int estt_Id { get; set; }
        public string estt_Descripcion { get; set; }
        public int estt_UsuarioCrea { get; set; }
        public System.DateTime estt_FechaCrea { get; set; }
        public Nullable<int> estt_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> estt_FechaModifica { get; set; }
    
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbTransaccionDetalle> tbTransaccionDetalle { get; set; }
    }
}
