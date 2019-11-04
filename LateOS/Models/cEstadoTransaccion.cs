using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cEstadoTransaccion))]
    public partial class tbEstadoTransaccion
    {

    }

    public class cEstadoTransaccion
    {
        [Display(Name = "ID Transaccion")]
        public int estt_Id { get; set; }

        [Display(Name = "Estado Transacción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(50, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string estt_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public int estt_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public System.DateTime estt_FechaCrea { get; set; }
        public Nullable<int> estt_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> estt_FechaModifica { get; set; }
        [Display(Name = "Usuario Crea")]
        public virtual tbUsuarios tbUsuarios { get; set; }
        [Display(Name = "Usuario Modifica")]
        public virtual tbUsuarios tbUsuarios1 { get; set; }
    }
}