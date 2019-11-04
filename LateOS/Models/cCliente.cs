using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LateOS.Models
{
    [MetadataType(typeof(cCliente))]
    public partial class tbCliente
    {

    }
    public class cCliente
    {
        public int clte_Id { get; set; }

        [Display(Name = "No. Identidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(14, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string clte_Identidad { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(40, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string clte_Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(40, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string clte_Apellido { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public System.DateTime clte_FechaNacimiento { get; set; }

        [Display(Name = "Sexo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(1, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string clte_Sexo { get; set; }

        [Display(Name = "Telefono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(15, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string clte_Telefono { get; set; }

        [Display(Name = "Correo Electronico")]
        [EmailAddress]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(40, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string clte_Correo { get; set; }

        [Display(Name = "ID Usuario")]
        public int usu_Id { get; set; }

        [Display(Name = "Usuario Crea")]
        public int clte_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public System.DateTime clte_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> clte_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> clte_FechaModifica { get; set; }
    }
}