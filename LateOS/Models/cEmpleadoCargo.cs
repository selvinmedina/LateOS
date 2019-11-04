using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cEmpleadoCargo))]
    public partial class tbEmpleadoCargo
    {

    }
    public class cEmpleadoCargo
    {
        [Display(Name = "ID Cargo")]
        public int empc_Id { get; set; }

        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(50, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string empc_Descripcion { get; set; }
        [Display(Name = "Descripcion Departamento")]
        public int empd_Id { get; set; }
        public int empc_UsuarioCrea { get; set; }
        public System.DateTime empc_FechaCrea { get; set; }
        public Nullable<int> empc_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> empc_FechaModifica { get; set; }
    }
}