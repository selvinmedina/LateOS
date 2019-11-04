using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cDevolucion))]
    public partial class tbDevolucion
    {

    }
    public class cDevolucion
    {

        [Display(Name = "ID Devolución")]
        public Nullable<int> dev_Id { get; set; }

        [Display(Name = "ID Factura")]
        public int fact_Id { get; set; }

        [Display(Name = "Razón Devolución")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(250, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string dev_Razon { get; set; }

        [Display(Name = "Fecha Devolución")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public Nullable<System.DateTime> dev_Fecha { get; set; }



        public int dev_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> dev_FechaCrea { get; set; }
        public Nullable<int> dev_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> dev_FechaModifica { get; set; }
    }
}