using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cProducto))]
    public partial class tbProducto
    {
    }

    public class cProducto
    {
        [Display(Name = "Id Producto")]
        public int prod_Id { get; set; }

        [Display(Name = "Codigo del Producto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(30, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string prod_Codigo { get; set; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string prod_Descripcion { get; set; }

        [Display(Name = "Id Proveedor")]
        public int prov_Id { get; set; }

        [Display(Name = "Id Subcategoría")]
        public int subc_Id { get; set; }

        [Display(Name = "Precio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        public Nullable<decimal> prod_Precio { get; set; }

        [Display(Name = "Autor")]
        public int prod_UsuarioCrea { get; set; }

        [Display(Name = "Fecha de Creación")]
        public System.DateTime prod_FechaCrea { get; set; }

        [Display(Name = "Usuario que modificó")]
        public Nullable<int> prod_UsuarioModifica { get; set; }

        [Display(Name = "Fecha de edicíón")]
        public Nullable<DateTime> prod_FechaModifica { get; set; }

        [Display(Name = "Imagen Principal")]
        public string prod_Img { get; set; }
    }
}