using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace LateOS.Models
{
    [MetadataType(typeof(cFormaPago))]
    public partial class tbFormaPago
    {

    }

    public class cFormaPago
    {
        [Display(Name = "ID Forma Pago")]
        public int forp_Id { get; set; }

        [Display(Name = "Descripcion Forma Pago")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} de requerido.")]
        [MaxLength(50, ErrorMessage = "Excedió el máximo de caracteres permitidos.")]
        public string forp_Descripcion { get; set; }

        public Nullable<int> forp_UsuarioCrea { get; set; }
        public Nullable<System.DateTime> forp_FechaCrea { get; set; }
        public Nullable<int> forp_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> forp_FechaModifica { get; set; }

        public virtual tbUsuarios tbUsuarios { get; set; }
        public virtual tbUsuarios tbUsuarios1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbFactura> tbFactura { get; set; }
    }
}