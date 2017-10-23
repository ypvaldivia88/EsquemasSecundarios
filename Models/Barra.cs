using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("Sub_Barra")]
    public class Barra
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(7)]
        public string Subestacion { get; set; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(30)]
        public string codigo { get; set; }

        public int ID_Voltaje { get; set; }

        [ForeignKey("Subestacion")]
        public virtual Subestacion Subestaciones { get; set; }

        [ForeignKey("Subestacion")]
        public virtual SubestacionTransmision SubestacionesTransmision { get; set; }
    }
}