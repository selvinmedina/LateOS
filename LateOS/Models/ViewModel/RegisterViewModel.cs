using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LateOS.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "Identidad")]
        [Required]
        [MaxLength(20)]        
        public string identidad{ get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [MaxLength(20)]
        public string nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required]
        [MaxLength(20)]
        public string Apellido { get; set; }

        [Display(Name = "Fecha nacimiento")]
        [Required]
        public System.DateTime FechaNacimiento { get; set; }

        [Display(Name = "Sexo")]
        [Required]
        [MaxLength(1)]
        public string sexo { get; set; }

        [Display(Name = "Telefono")]
        [Required]
        [MaxLength(20)]
        public string Telefono { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required]
        [EmailAddress]
        public string correo { get; set; }

        [Display(Name = "Contraseña")]
        [Required]
        public byte[] password { get; set; }

    }
}