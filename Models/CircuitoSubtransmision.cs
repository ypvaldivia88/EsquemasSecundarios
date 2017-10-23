using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("CircuitosSubtransmision")]
    public class CircuitoSubtransmision
    {
        [Key]
        public string CodigoCircuito { get; set; }
        public string NombreCircuito { get; set; }
        public string SubestacionTransmision { get; set; }
    }
}