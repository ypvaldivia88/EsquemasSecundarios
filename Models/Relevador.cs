using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EsquemasSecundarios.Models
{
    [Table("ES_Relevador")]
    public class Relevador
    {
        [Key]
        [Display(Name = "Nombre"), Required(ErrorMessage = "El campo Nombre es obligatorio"), MaxLength(120)]
        public string Nro_Serie { get; set; }

        [Display(Name = "Plantilla"), Required]
        public int id_Plantilla { get; set; }

        public short? Id_EAdministrativa { get; set; }

        public int? Id_NumAccion { get; set; }

        //Propiedades Virtuales Referencias a otras clases

        [ForeignKey("id_Plantilla")]
        public Plantilla Plantilla { get; set; }

        public List<Esquema_Relevador> Esquemas_Relevadores { get; set; }
    }
}