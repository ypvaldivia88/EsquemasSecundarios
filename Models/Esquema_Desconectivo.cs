using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Esquema_Desconectivo")]
    public class Esquema_Desconectivo
    {
        [Key, Column(Order = 1)]
        [MaxLength(7)]
        public string desconectivo { get; set; }

        [Key, Column(Order = 2)]
        public int esquema { get; set; }
        
    }
}