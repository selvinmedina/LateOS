using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cPais))]
    public partial class tbPais
    {
    }

    public class cPais
    {
        [Display(Name ="ID Pais")]        
        public int pais_Id { get; set; }

        [Display(Name = "Descripcion Pais")]
        [Required(AllowEmptyStrings =false,ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(35,ErrorMessage ="Excedió el máximo de caracteres permitidos.")]
        public string pais_Descripcion { get; set; }
        public int pais_UsuarioCrea { get; set; }
        public System.DateTime pais_FechaCrea { get; set; }
        public Nullable<int> pais_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> pais_FechaModifica { get; set; }

    }
}