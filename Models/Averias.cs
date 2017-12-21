using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Averias")]
    public class Averias
    {
        [Key]
        public int IdAveria { get; set; }

        [Required(ErrorMessage = "Debe introducir el campo: {0}")]
        [Display(Name = "Fecha del Reporte"), DisplayFormat(DataFormatString = "{0:yyy-MM-dd h:mm tt}")]
        public DateTime FechaReporte { get; set; }

        [Display(Name = "Fecha de Atención"), DisplayFormat(DataFormatString = "{0:yyy-MM-dd h:mm tt}")]
        public DateTime FechaAtencion { get; set; }

        [Display(Name = "Persona Que Atendió")]
        public string PersonaQueAtendio { get; set; }

        [Display(Name = "Datos Reportados")]
        public string DatosReportados { get; set; }

        [Display(Name = "Análisis")]
        public string Analisis { get; set; }

        public string Conclusiones { get; set; }

        public string Recomendaciones { get; set; }

        [Display(Name = "Elaborado Por")]
        public string ElaboradoPor { get; set; }

        [Display(Name = "Revisado Por")]
        public string RevisadoPor { get; set; }

        [Display(Name = "Aprobado Por")]
        public string AprobadoPor { get; set; }

        [Display(Name = "Subestación"), Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public string CodSubestacion { get; set; }

        [Display(Name = "Esquema"),Required(ErrorMessage = "Debe introducir el campo: {0}")]
        public int IdEsquema { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? Id_NumAccion { get; set; }


        //Propiedades Virtuales Referencias a otras clases
        [ForeignKey("CodSubestacion"),Display(Name = "Subestación")]
        public Subestacion Subestacion { get; set; }

        [ForeignKey("IdEsquema")]
        public EsquemaProteccion Esquema { get; set; }
    }
}