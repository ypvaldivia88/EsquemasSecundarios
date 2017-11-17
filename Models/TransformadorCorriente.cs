using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_TransformadorCorriente")]
    public class TransformadorCorriente
    {
        [Key]
        [Display(Name = "Número de serie")]
        public string Nro_Serie { get; set; }
        public char? Fase { get; set; }
        [Display(Name = "Relación transformación")]
        public string Relacion_Transformacion { get; set; }
        [Display(Name = "Cantidad devanado")]
        public short? Cant_Devanado { get; set; }
        public double? Frecuencia { get; set; }        
        public bool Ubicado { get; set; }
        [Display(Name = "Subestación")]
        public string CodSub { get; set; }
        [Display(Name = "Tipo equipo primario")]
        public string Tipo_Equipo_Primario { get; set; }
        [Display(Name = "Elemento eléctrico")]
        public string Elemento_Electrico { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        [Required(ErrorMessage = "Debe introducir {0}")]
        [Display(Name = "Id Voltaje nominal")]
        public short? id_Voltaje_Nominal { get; set; }
        [ForeignKey("id_Voltaje_Nominal")]
        [Display(Name = "Voltaje")]
        public VoltajeSistema VoltajeSistema { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        public int? id_Plantilla { get; set; }
        [ForeignKey("id_Plantilla")]
        public Plantilla Plantilla { get; set; }

        public List<Esquema_TC> Esquemas_TC { get; set; }
    }
}