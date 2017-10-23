using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Plantilla_Rele")]
    public class Plantilla
    {
        [Key]
        public int id_Plantilla { get; set; }
        public string Modelo { get; set; }
        public int Id_Fabricante { get; set; }

        public List<Relevador> Relevadores { get; set; }
        public List<Plantilla_Funcion> Plantillas_Funciones { get; set; }

        [ForeignKey("Id_Fabricante")]
        public Fabricante Fabricante { get; set; }
        
    }
}