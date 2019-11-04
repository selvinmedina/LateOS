using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cSalida))]

    public partial class tbSalida
    {

    }
    public class cSalida
    {
        [Display(Name = "ID Salida")]
        public int sali_Id { get; set; }

        [Display(Name = "Código Producto")]
        public int prod_Id { get; set; }

        [Display(Name = "Existencia")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Campo '{0}' es requerido.")]
        [MaxLength(100, ErrorMessage = "Excedio el máximo de Caracteres Permitidos.")]
        public int sali_Existencia { get; set; }

        [Display(Name = "Cantidad")]
        public int sali_Cantidad { get; set; }
        [Display(Name = "Fecha  de Entrada")]
        public System.DateTime sali_FechaEntrada { get; set; }





        public int sali_UsuarioCrea { get; set; }
        public System.DateTime sali_FechaCrea { get; set; }
        public Nullable<int> sali_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> sali_FechaModifica { get; set; }
    }
}