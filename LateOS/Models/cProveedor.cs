using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cProveedor))]
    public partial class tbProveedor
    {
    }

    public class cProveedor
    {
        public int prov_Id { get; set; }

        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        public string prov_Descripcion { get; set; }
        public int prov_UsuarioCrea { get; set; }
        public System.DateTime prov_FechaCrea { get; set; }
        public Nullable<int> prov_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> prov_FechaModifica { get; set; }
    }
}