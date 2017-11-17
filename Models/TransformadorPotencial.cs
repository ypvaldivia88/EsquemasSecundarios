using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_TransformadorPotencial")]
    public class TransformadorPotencial
    {
        [Key]
        [Display(Name = "Número de serie")]
        public string Nro_Serie { get; set; }
        [Display(Name = "Cantidad devanado")]
        public short? Cant_Devanado { get; set; }
        public char? Fase { get; set; }        
        public bool Ubicado { get; set; }
        [Display(Name = "Subestación")]
        public string CodSub { get; set; }
        [Display(Name = "Tipo equipo primario")]
        public string Tipo_Equipo_Primario { get; set; }
        [Display(Name = "Elemento eléctrico")]
        public string Elemento_Electrico { get; set; }
        [Display(Name = "Voltaje nominal")]
        public string VoltajeNominal { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Id Voltaje nominal")]
        public short? id_Voltaje_Primario { get; set; }
        [ForeignKey("id_Voltaje_Primario")]
        [Display(Name = "Voltaje primario")]
        public VoltajeSistema VoltajeSistema { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        public int? id_Plantilla { get; set; }
        [ForeignKey("id_Plantilla")]
        public Plantilla Plantilla { get; set; }

        public List<Esquema_TP> Esquemas_TP { get; set; }
    }
}