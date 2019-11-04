using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cListadoPrecioDetalle))]
    public partial class tbListadoPrecioDetalle
    {
    }
    public class cListadoPrecioDetalle
    {
        [Display(Name = "ID")]
        public int lipd_Id { get; set; }
        [Display(Name = "ID Listado Precio")]
        public int lip_Id { get; set; }
        [Display(Name = "ID Producto")]
        public int prod_Id { get; set; }

        [Display(Name = "ID Tipo de Descuento")]
        public Nullable<int> tipd_Id { get; set; }

        [Display(Name = "Precio")]
        public decimal lipd_Precio { get; set; }

        [Display(Name = "Impuesto")]
        public Nullable<decimal> lipd_ISV { get; set; }

        [Display(Name = "Creado por")]
        public int lipd_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime lipd_FechaCrea { get; set; }

        [Display(Name = "Modificado por")]
        public Nullable<int> lipd_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> lipd_FechaModifica { get; set; }
    }
}