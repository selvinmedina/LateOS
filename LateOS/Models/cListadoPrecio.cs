using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LateOS.Models
{
    [MetadataType(typeof(cListadoPrecio))]
    public partial class tbListadoPrecio
    {

    }
    public class cListadoPrecio
    {
        [Display(Name = "ID Listado Precio")]
        public int lip_Id { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string lip_Descripcion { get; set; } 

        [Display(Name = "Prioridad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido.")]
        public int lip_Prioridad { get; set; }

        [Display(Name = "Activo")]
        public bool lip_EsActivo { get; set; }

        [Display(Name = "Fecha de Inicio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public System.DateTime lip_FechaInicio { get; set; }

        [Display(Name = "Fecha de Finalización")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public System.DateTime lip_FechaFin { get; set; }

        [Display(Name = "Creado por")]
        public int lip_UsuarioCrea { get; set; }
        [Display(Name = "Fecha de Creación")]
        public System.DateTime lip_FechaCrea { get; set; }
        [Display(Name = "Modificado por")]
        public Nullable<int> lip_UsuarioModifica { get; set; }
        [Display(Name = "Fecha de Modificación")]
        public Nullable<System.DateTime> lip_FechaModifica { get; set; }
    }
}