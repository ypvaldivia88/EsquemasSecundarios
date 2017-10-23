using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("CircuitosPrimarios")]
    public class CircuitoPrimario
    {
        [Key]
        public string CodigoCircuito { get; set; }

        public string NombreAntiguo { get; set; }

        [MaxLength(7)]
        public string SubAlimentadora { get; set; }
    }
}