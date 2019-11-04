using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LateOS.Models
{
    [MetadataType(typeof(cClienteDireccion))]
    public partial class tbClienteDireccion
    {

    }


    public class cClienteDireccion
    {
        public int clted_Id { get; set; }
        [Display(Name ="Direccion descripcion")]
        public string clted_Descripcion { get; set; }
        [Display(Name = "Cliente ID")]
        public int clte_Id { get; set; }
        [Display(Name = "ID Ciudad")]
        public int ciu_Id { get; set; }
        public int clted_UsuarioCrea { get; set; }
        public System.DateTime clted_FechaCrea { get; set; }
        public Nullable<int> clted_UsuarioModifica { get; set; }
        public Nullable<System.DateTime> clted_FechaModifica { get; set; }

    }
}