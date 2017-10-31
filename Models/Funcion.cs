using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("EsquemasProteccion")]
    public class Funcion
    {
        [Key]
        public int Id_Esquema { get; set; }

        [MaxLength(50)]
        public string Codigo { get; set; }

        [MaxLength(200)]
        public string Descripcion { get; set; }

        public List<Plantilla_Funcion> Plantillas_Funciones { get; set; }
    }
}