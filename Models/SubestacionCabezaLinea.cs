using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("SubestacionesCabezasLineas")]
    public class SubestacionCabezaLinea
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(7)]
        public string Codigolinea { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(7)]
        public string SubestacionTransmicion { get; set; }
    }
}