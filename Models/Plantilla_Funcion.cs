using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_PlantillaRele_Funciones")]
    public class Plantilla_Funcion
    {
        [Key]      
        [Column(Order = 1)]       
        public int id_Plantilla { get; set; }   
            
        [Key]
        [Column(Order = 2)]
        public int id_Funcion { get; set; }

        [ForeignKey("id_Funcion")]
        public Plantilla Plantilla { get; set; }

        [ForeignKey("id_Funcion")]
        public Funcion Funcion { get; set; }
    }
}