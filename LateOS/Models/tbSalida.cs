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
    
    public partial class tbSalida
    {
        public int sali_Id { get; set; }
        public int prod_Id { get; set; }
        public int sali_Existencia { get; set; }
        public int sali_Cantidad { get; set; }
        public System.DateTime sali_FechaEntrada { get; set; }
        public int sali_UsuarioCrea { get; set; }
        public System.DateTime sali_FechaCrea { get; set; }
        public Nullable<int> sali_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> sali_FechaModifica { get; set; }
    
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        public virtual tbProducto tbProducto { get; set; }
    }
}
