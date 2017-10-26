using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Nomenclador_Mantenimiento")]
    public class TipoMantenimiento
    {
        [Key]
        public short id_Tipo { get; set; }
        public string Tipo { get; set; }
        public char Siglas { get; set; }
    }
}