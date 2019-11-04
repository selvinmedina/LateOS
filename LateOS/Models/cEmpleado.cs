using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cEmpleado))]
    public partial class tbEmpleado
    {
    }
        public class cEmpleado
    {
        [Display(Name = "Id Empleado: ")]
        public int emp_Id { get; set; }

        [Display(Name = "Identidad: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        [MaxLength(35, ErrorMessage = "Excedio limite de caracteres permitidos.")]
        public string emp_Identidad { get; set; }

        [Display(Name = "Nombres: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        [MaxLength(35, ErrorMessage = "Excedio limite de caracteres permitidos.")]
        public string emp_Nombres { get; set; }

        [Display(Name = "Apellidos: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        [MaxLength(35, ErrorMessage = "Excedio limite de caracteres permitidos.")]
        public string emp_Apellidos { get; set; }

        [Display(Name = "Fecha Nacimiento: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        //[MaxLength(35, ErrorMessage = "Excedio limite de caracteres permitidos.")]
        public System.DateTime emp_FechaNacimiento { get; set; }

        [Display(Name = "Sexo: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        //[MaxLength(35, ErrorMessage = "Excedio limite de caracteres permitidos.")]
        public string emp_Sexo { get; set; }

        [Display(Name = "Correo Electronico: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        //[MaxLength(35, ErrorMessage = "Excedio limite de caracteres permitidos.")]
        public string emp_CorreoElectronico { get; set; }

        [Display(Name = "Telefono: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        //[MaxLength(35, ErrorMessage = "Excedio limite de caracteres permitidos.")]
        public string emp_Telefono { get; set; }

        [Display(Name = "Fecha Ingreso: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        //[MaxLength(35, ErrorMessage = "Excedio limite de caracteres permitidos.")]
        public System.DateTime emp_FechaIngreso { get; set; }

        public Nullable<bool> emp_EsActivo { get; set; }

        [Display(Name = "Cargo: ")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo {0} requerido!")]
        public int empc_id { get; set; }
        public int usu_Id { get; set; }
        public int emp_UsuarioCrea { get; set; }
        public System.DateTime emp_FechaCrea { get; set; }
        public Nullable<int> emp_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> emp_FechaModifica { get; set; }
    }
}