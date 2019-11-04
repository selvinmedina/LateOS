using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{

    [MetadataType(typeof(cCategoria))]
    public partial class tbCategoria
    {

    } 

    public class cCategoria
    {
        [Display(Name = "ID Categoria")]
        public int cat_Id { get; set; }

        [Display(Name = "Descripción Categoria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(80, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string cat_Descripcion { get; set; }

        [Display(Name = "Usuario Crea")]
        public int cat_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime cat_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> cat_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> cat_FechaModifica { get; set; }
    }
}