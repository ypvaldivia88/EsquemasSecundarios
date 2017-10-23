using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("VoltajesSistemas")]
    public class VoltajeSistema
    {
        [Key]
        public short Id_VoltajeSistema { get; set; }
        public double Voltaje { get; set; }
    }
}