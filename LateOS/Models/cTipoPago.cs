using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cTipoPago))]
    public partial class tbTipoPago
    {
    }

    public class cTipoPago
    {
        [Display(Name = "ID Tipo pago")]
        public int tipp_Id { get; set; }

        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(35, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string tipp_Descripcion { get; set; }
        public int tipp_UsuarioCrea { get; set; }
        public System.DateTime tipp_FechaCrea { get; set; }
        public Nullable<int> tipp_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> tipp_FechaModifica { get; set; }
    }
}