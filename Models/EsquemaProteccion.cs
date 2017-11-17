using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Esquemas_Prot")]
    public class EsquemaProteccion
    {
        [Key]
        public int id_Esquema { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [MaxLength(7)]
        public string Subestacion { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [MaxLength(15)]
        public string Tipo_Equipo_Primario { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [MaxLength(20)]
        public string Elemento_Electrico { get; set; }

        public string Clase { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        public List<Esquema_Relevador> Esquemas_Relevadores { get; set; }
        public List<Esquema_Desconectivo> Esquemas_Desconectivos { get; set; }
        public List<Esquema_TC> Esquemas_TC { get; set; }
        public List<Esquema_TP> Esquemas_TP { get; set; }
    }
}