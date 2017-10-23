using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("InstalacionDesconectivos")]
    public class Desconectivo
    {
        [Key, Column(Order = 1)]
        [MaxLength(7)]
        public string Codigo { get; set; }

        [Key, Column(Order = 2)]
        public int id_EAdministrativa_Prov { get; set; }
    }
}