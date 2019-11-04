using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cTransaccionDetalle))]
    public partial class tbTransaccionDetalle
    {

    }
    public class cTransaccionDetalle
    {
        [Display(Name = "ID Transacción")]
        public int trad_Id { get; set; }

        [Display(Name = "Transacción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        
        public string trad_Descripcion { get; set; }

        [Display(Name = "Fecha de Transacción")]
       public System.DateTime trad_Fecha { get; set; }

        [Display(Name = "ID Factura")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public int fact_Id { get; set; }

        [Display(Name = "Estado de Transacción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        
        public int estt_Id { get; set; }

        public int trad_UsuarioCrea { get; set; }

        public System.DateTime trad_FechaCrea { get; set; }
        public Nullable<int> trad_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> trad_FechaModifica { get; set; }
    }
}