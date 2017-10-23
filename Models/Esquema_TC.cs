using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Esquema_TC")]
    public class Esquema_TC
    {
        [Key]
        [Column(Order = 1)]
        public string TC { get; set; }
        [Key]
        [Column(Order = 2)]
        public int esquema { get; set; }

        [ForeignKey("esquema")]
        public EsquemaProteccion EsquemaProteccion  { get; set; }

        [ForeignKey("TC")]
        public TransformadorCorriente TransformadorCorriente { get; set; }
    }
}