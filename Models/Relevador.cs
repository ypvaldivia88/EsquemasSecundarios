using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Relevador")]
    public class Relevador
    {
        [Key]
        public string Nro_Serie { get; set; }
        public bool Ubicado { get; set; }
        public string Voltaje_Alterno { get; set; }
        public string Voltaje_Directo { get; set; }
        public int id_Plantilla { get; set; }

        [ForeignKey("id_Plantilla")]
        public Plantilla Plantilla { get; set; }

        public List<Esquema_Relevador> Esquemas_Relevadores { get; set; }
    }
}