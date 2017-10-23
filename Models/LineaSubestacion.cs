using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("Sub_LineasSubestacion")]
    public class LineaSubestacion
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(7)]
        public string Subestacion { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(7)]
        public string Circuito { get; set; }
    }
}