using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("LineaTranmision")]
    public class LineaTransmision
    {
        [Key]
        public string Codigolinea { get; set; }
        public string NombreCircuito { get; set; }

    }
}