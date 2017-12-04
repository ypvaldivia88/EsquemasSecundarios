using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("EstructurasAdministrativas")]
    public class EstructuraAdministrativa
    {
        [Key,Column(Order = 1)]
        public int Id_EAdministrativa { get; set; }

        [Key,Column(Order = 2)]
        public int id_EAdministrativa_Prov { get; set; }

        [MaxLength(35,ErrorMessage = "El campo debe tener un máximo de {0} caracteres")]
        public string Nombre { get; set; }
    }
}