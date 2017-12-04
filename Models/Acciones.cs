using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("AccionesE")]
    public class Acciones
    {
        [Key,Column(Order = 1)]
        public short Id_EAdministrativa { get; set; }

        [Key,Column(Order = 2)]
        public int NumAccion { get; set; }

        [Key,Column(Order = 3)]
        public int id_EAdministrativa_Prov { get; set; }

        public string TipoAccion { get; set; }

        public char TipoSubAccion { get; set; }

        public DateTime FechaIntroduccion { get; set; }

        public short IntroducidaPor { get; set; }

        public short EAModificada { get; set; }

        public int AccionModificada { get; set; }

        public short EADestino { get; set; }

        [MaxLength(7,ErrorMessage ="El campo debe tener un máximo de {0} caracteres")]
        public string Instalacion { get; set; }

        

    }
}