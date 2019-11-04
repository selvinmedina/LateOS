using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{

    [MetadataType(typeof(cEstadoFactura))]
    public partial class tbEstadoFactura
    {

    }

    public class cEstadoFactura
    {
        [Display(Name = "ID Estado Factura")]
        public int estf_Id { get; set; }

        [Display(Name = "Estado Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(25, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string estf_Descripcion { get; set; }


        public int estf_UsuarioCrea { get; set; }
        public System.DateTime estf_FechaCrea { get; set; }
        public Nullable<int> estf_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> estf_FechaModifica { get; set; }

        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
    }
}