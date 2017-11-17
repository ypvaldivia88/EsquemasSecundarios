using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Mantenimientos")]
    public class Mantenimientos
    {
        [Key]
        public int IdMantenimiento { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd h:mm tt}")]
        public DateTime Fecha { get; set; }        
        public string Observaciones { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Subestación")]
        public string CodSubestacion { get; set; }
        [ForeignKey("CodSubestacion")]
        public Subestacion Subestacion { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Esquema")]
        public int IdEsquema { get; set; }
        [ForeignKey("IdEsquema")]
        public EsquemaProteccion Esquema { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Tipo de mantenimiento")]
        public short? id_Tipo { get; set; }
        [ForeignKey("id_Tipo")]
        public TipoMantenimiento TipoMantenimiento { get; set; }
    }
}