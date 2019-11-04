using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cCiudad))]
    public partial class tbCiudad
    {

    }
    public class cCiudad
    {
        [Display(Name = "ID Ciudad")]
        public int ciu_Id { get; set; }

        [Display(Name = "Descripcion Ciudad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(50, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string ciu_Descripcion { get; set; }
        public int pais_Id { get; set; }
        public int ciu_UsuarioCrea { get; set; }
        public System.DateTime ciu_FechaCrea { get; set; }
        public Nullable<int> ciu_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> ciu_FechaModifica { get; set; }
    }
}