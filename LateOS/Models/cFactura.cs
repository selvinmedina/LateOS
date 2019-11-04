using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cFactura))]
    public partial class tbFactura
    {
    }
    public class cFactura
    {
        [Display(Name ="ID Factura")]
        public int fact_Id { get; set; }
        [Display(Name = "Código")]
        public string fact_Codigo { get; set; }
        [Display(Name = "Fecha")]
        public System.DateTime fact_Fecha { get; set; }
        [Display(Name = "ID Cliente")]
        public int clte_Id { get; set; }
        [Display(Name = "ID Tipo Pago")]
        public int tipp_Id { get; set; }
        [Display(Name = "ID Forma pago")]
        public int forp_Id { get; set; }
        [Display(Name = "Usuario crea")]
        public int fact_UsuarioCrea { get; set; }
        [Display(Name = "Fecha crea")]
        public System.DateTime fact_FechaCrea { get; set; }
        [Display(Name = "Usuario modifica")]
        public Nullable<int> fact_UsuarioModifica { get; set; }
        [Display(Name = "Fecha modifica")]
        public Nullable<System.DateTime> fact_FechaModifica { get; set; }
    }
}