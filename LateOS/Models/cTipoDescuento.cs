using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    
    [MetadataType(typeof(cTipoDescuento))]

    public partial class tbTipoDescuento
    {

    }
    public class cTipoDescuento
    {
        [Display(Name = "ID País")]
        public int tipd_Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Campo '{0}' es requerido.")]
        [MaxLength(100, ErrorMessage = "Excedio el máximo de Caracteres Permitidos.")]
        public string tipd_Descripcion { get; set; }
        [Display(Name = "Porcentaje")]
        public Nullable<decimal> tipd_Porcentaje { get; set; }
        [Display(Name = "Usuario Crea")]
        public int tipd_UsuarioCrea { get; set; }
        [Display(Name = "Fecha Crea")]
        public System.DateTime tipd_FechaCrea { get; set; }
        [Display(Name = "Usuario Modifica")]
        public Nullable<int> tipd_UsuarioModifica { get; set; }
        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> tipd_FechaModifica { get; set; }
        [Display(Name = "Usuario Crea")]
        public virtual tbUsuarios tbUsuarios { get; set; }
        [Display(Name = "Usuario Modifica")]
        public virtual tbUsuarios tbUsuarios1 { get; set; }
    }
}