using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Esquema_Rele")]
    public class Esquema_Relevador
    {
        [Key]
        [Column(Order = 1)]
        public string rele { get; set; }
        [Key]
        [Column(Order = 2)]
        public int esquema { get; set; }
        

        [ForeignKey("rele")]
        public Relevador Relevador { get; set; }

        [ForeignKey("esquema")]
        public EsquemaProteccion EsquemasProteccion { get; set; }

    }
}