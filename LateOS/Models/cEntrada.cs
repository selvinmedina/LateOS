using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LateOS.Models
{
    [MetadataType(typeof(cEntrada))]
    public partial class tbEntrada
    {

    }
    public class cEntrada
    {
        public int ent_Id { get; set; }

        [Display(Name = "Id Producto")]
        public int prod_Id { get; set; }

        [Display(Name = "Existencia")]
        public int ent_Existencia { get; set; }

        [Display(Name = "Cantidad")]
        public int ent_Cantidad { get; set; }

        [Display(Name = "Fecha de Entrada")]
        public System.DateTime ent_FechaEntrada { get; set; }

        [Display(Name = "Usuario Crea")]
        public int ent_UsuarioCrea { get; set; }

        [Display(Name = "Fecha Crea")]
        public System.DateTime ent_FechaCrea { get; set; }

        [Display(Name = "Usuario Modifica")]
        public Nullable<int> ent_UsuarioModifica { get; set; }

        [Display(Name = "Fecha Modifica")]
        public Nullable<System.DateTime> ent_FechaModifica { get; set; }

        [Display(Name = "Usuario Crea")]
        public virtual tbUsuarios tbUsuarios { get; set; }

        [Display(Name = "Usuario Modifica")]
        public virtual tbUsuarios tbUsuarios1 { get; set; }

        [Display(Name = "Código Producto")]
        public virtual tbProducto tbProducto { get; set; }
    }
}