using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("ES_TransformadorCorriente")]
    public class TransformadorCorriente
    {
        [Key]
        public string Nro_Serie { get; set; }
        public char? Fase { get; set; }
        public string Relacion_Transformacion { get; set; }
        public short? Cant_Devanado { get; set; }
        public double? Frecuencia { get; set; }
        public short? id_Voltaje_Nominal { get; set; }
        public bool Ubicado { get; set; }
        public int? id_Plantilla { get; set; }
        public string CodSub { get; set; }
        public string Tipo_Equipo_Primario { get; set; }
        public string Elemento_Electrico { get; set; }
        public short? Id_EAdministrativa { get; set; }
        public int? NumAccion { get; set; }


        public List<Esquema_TC> Esquemas_TC { get; set; }
    }
}