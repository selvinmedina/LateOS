using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cDevolucionDetalle))]
    public partial class tbDevolucionDetalle
    {

    }
    public class cDevolucionDetalle
    {


        [Display(Name = "ID Devolución Detalle")]
        public int devd_Id { get; set; }

        [Display(Name = "ID Devolución")]
        public int dev_Id { get; set; }


        [Display(Name = "Producto")]
        public int prod_Id { get; set; }


        [Display(Name = "Precio Producto")]
        public decimal prod_Precio { get; set; }


        [Display(Name = "Cantidad Devolución")]
        public int devd_Cantidad { get; set; }


        [Display(Name = "Impuesto")]
        public Nullable<decimal> devd_Impuesto { get; set; }


        [Display(Name = "Descuento")]
        public Nullable<decimal> devd_Descuento { get; set; }
        public int devd_UsuarioCrea { get; set; }
        public System.DateTime devd_FechaCrea { get; set; }
        public Nullable<int> devd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> devd_FechaModifica { get; set; }


    }
}