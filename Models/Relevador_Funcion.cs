using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Rele_Funciones")]
    public class Relevador_Funcion
    {
        [Key]
        [Column(Order = 1)]
        public string Nro_Serie_R { get; set; }

        [Key]
        [Column(Order = 2)]
        public int id_Funcion { get; set; }

        [ForeignKey("Nro_Serie_R")]
        public Relevador FK_ES_Rele_Funciones_ES_Relevador1 { get; set; }
    }
}