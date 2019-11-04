using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cSubCategoria))]
    public partial class tbSubCategoria
    {

    }

    public class cSubCategoria
    {
        public int subc_Id { get; set; }

        [Display(Name = "Descripción Subcategoria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(80, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string subc_Descripcion { get; set; }

        [Display(Name = "ID Subcategoria")]
        public int cat_Id { get; set; }

        [Display(Name = "Usuario Crea")]
        public int subc_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime subc_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> subc_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> subc_FechaModifica { get; set; }
    }
}