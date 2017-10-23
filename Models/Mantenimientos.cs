using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Mantenimientos")]
    public class Mantenimientos
    {
        [Key, Column(Order = 1)]
        public DateTime Fecha { get; set; }

        [Key, Column(Order = 2)]
        [MaxLength(50)]
        public string CodEquipo { get; set; }
        
        [MaxLength(2)]
        public char TipoEquipo { get; set; }

        public short Mantenimiento { get; set; }

        public string Observaciones { get; set; }

        public DateTime? FechaProximo { get; set; }

        public short? MttoProximo { get; set; }

        public string Subestacion { get; set; }

    }
}