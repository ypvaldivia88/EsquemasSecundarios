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

        public DateTime FechaReporte { get; set; }        
        public string CodSubestacion { get; set; }        
        public int IdEsquema { get; set; }
        public DateTime FechaAtencion { get; set; }
        public string PersonaQueAtendio { get; set; }
        public string DatosReportados { get; set; }
        public string Analisis { get; set; }
        public string Conclusiones { get; set; }
        public string Recomendaciones { get; set; }
        public string ElaboradoPor { get; set; }
        public string RevisadoPor { get; set; }
        public string AprobadoPor { get; set; }

        [ForeignKey("CodSubestacion")]
        public Subestacion Subestacion { get; set; }

        [ForeignKey("IdEsquema")]
        public EsquemaProteccion Esquema { get; set; }
    }
}