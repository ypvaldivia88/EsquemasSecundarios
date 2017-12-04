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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyy-MM-dd h:mm tt}"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public DateTime Fecha { get; set; }       
        
        public string Observaciones { get; set; }

        [Display(Name = "Subestación"),Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string CodSubestacion { get; set; }

        [Display(Name = "Esquema"),Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public int IdEsquema { get; set; }

        [Display(Name = "Tipo de mantenimiento"),Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public short id_Tipo { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        [ForeignKey("CodSubestacion")]
        public Subestacion Subestacion { get; set; }        

        [ForeignKey("IdEsquema")]
        public EsquemaProteccion Esquema { get; set; }        

        [ForeignKey("id_Tipo")]
        public TipoMantenimiento TipoMantenimiento { get; set; }
    }
}