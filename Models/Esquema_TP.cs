using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Esquema_TP")]
    public class Esquema_TP
    {
        [Key]
        [Column(Order = 1)]
        public string TP { get; set; }
        [Key]
        [Column(Order = 2)]
        public int esquema { get; set; }

        [ForeignKey("esquema")]
        public EsquemaProteccion EsquemaProteccion { get; set; }

        [ForeignKey("TP")]
        public TransformadorPotencial TransformadorPotencial { get; set; }
    }
}