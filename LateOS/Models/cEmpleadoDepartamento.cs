using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cEmpleadoDepartamento))]
    public partial class tbEmpleadoDepartamento
    {

    }

    public class cEmpleadoDepartamento
    {
        [Display(Name = "ID Depto")]
        public int empd_Id { get; set; }

        [Display(Name = "Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(50, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string empd_Descripcion { get; set; }


        public int empd_UsuarioCrea { get; set; }
        public System.DateTime empd_FechaCrea { get; set; }
        public Nullable<int> empd_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> empd_FechaModifica { get; set; }
    }

}