using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsquemasSecundarios.Models
{
    [Table("SubestacionesTransmision")]
    public class SubestacionTransmision
    {
        [Key]
        [MaxLength(7)]
        public string Codigo { get; set; }
        [MaxLength(100)]
        public string NombreSubestacion { get; set; }
        public short? Id_EAdministrativa { get; set; }
        public int NumAccion { get; set; }
        public string TipoSub { get; set; }

        public List<Barra> Barras { get; set; }

    }
}