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
    
    public partial class tbListadoPrecioDetalle
    {
        public int lipd_Id { get; set; }
        public int lip_Id { get; set; }
        public int prod_Id { get; set; }
        public Nullable<int> tipd_Id { get; set; }
        public decimal lipd_Precio { get; set; }
        public Nullable<decimal> lipd_ISV { get; set; }
        public int lipd_UsuarioCrea { get; set; }
        public System.DateTime lipd_FechaCrea { get; set; }
        public Nullable<int> lipd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> lipd_FechaModifica { get; set; }
    
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        public virtual tbTipoDescuento tbTipoDescuento { get; set; }
        public virtual tbListadoPrecio tbListadoPrecio { get; set; }
        public virtual tbProducto tbProducto { get; set; }
    }
}
