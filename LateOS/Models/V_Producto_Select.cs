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
    
    public partial class V_Producto_Select
    {
        public int prod_Id { get; set; }
        public string prod_Codigo { get; set; }
        public string prod_Descripcion { get; set; }
        public int subc_Id { get; set; }
        public string subc_Descripcion { get; set; }
        public Nullable<int> cat_Id { get; set; }
        public string cat_Descripcion { get; set; }
        public Nullable<int> invf_id { get; set; }
        public Nullable<int> invf_total { get; set; }
        public Nullable<System.DateTime> invf_FechaInventario { get; set; }
        public Nullable<decimal> lipd_Precio { get; set; }
        public Nullable<decimal> lipd_ISV { get; set; }
        public string lip_Descripcion { get; set; }
        public Nullable<System.DateTime> lip_FechaInicio { get; set; }
        public Nullable<System.DateTime> lip_FechaFin { get; set; }
        public Nullable<int> lip_Id { get; set; }
        public string tipd_Descripcion { get; set; }
        public Nullable<decimal> tipd_Porcentaje { get; set; }
        public Nullable<int> tipd_Id { get; set; }
        public string proi_imagen { get; set; }
    }
}