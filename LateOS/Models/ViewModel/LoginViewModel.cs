using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Display(Name ="Correo electrónico")]
        [Required]
        public string correo { get; set; }
        

        [Display(Name = "Contraseña")]
        [Required]
        public string Password { get; set; }
        
    }
}