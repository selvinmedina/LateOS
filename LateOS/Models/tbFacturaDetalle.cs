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
    
    public partial class tbFacturaDetalle
    {
        public int facd_Id { get; set; }
        public int fact_Id { get; set; }
        public int prod_Id { get; set; }
        public Nullable<int> facd_Cantidad { get; set; }
        public Nullable<decimal> prod_Precio { get; set; }
        public Nullable<decimal> facd_Impuesto { get; set; }
        public Nullable<decimal> facd_Descuento { get; set; }
        public int facd_UsuarioCrea { get; set; }
        public System.DateTime facd_FechaCrea { get; set; }
        public Nullable<int> facd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> facd_FechaModifica { get; set; }
    
        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        public virtual tbFactura tbFactura { get; set; }
        public virtual tbProducto tbProducto { get; set; }
    }
}
