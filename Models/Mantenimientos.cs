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

        public DateTime Fecha { get; set; }       
        public string CodSubestacion { get; set; }        
        public int IdEsquema { get; set; }
        public short? id_Tipo { get; set; }
        public string Observaciones { get; set; }

        [ForeignKey("CodSubestacion")]
        public Subestacion Subestacion { get; set; }

        [ForeignKey("IdEsquema")]
        public EsquemaProteccion Esquema { get; set; }

        [ForeignKey("id_Tipo")]
        public TipoMantenimiento TipoMantenimiento { get; set; }
    }
}