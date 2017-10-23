using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("TransformadoresTransmision")]
    public class TransformadorTransmision
    {
        [Key]
        [Column(Order = 1)]
        public short Id_EAdministrativa { get; set; }

        [Key]
        [Column(Order = 2)]
        public int Id_Transformador { get; set; }

        [MaxLength(7)]
        public string Codigo { get; set; }

        [MaxLength(5)]
        public string Nombre { get; set; }
    }
}