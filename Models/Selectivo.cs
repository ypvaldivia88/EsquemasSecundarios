using System.ComponentModel.DataAnnotations;

namespace EsquemasSecundarios.Models
{
    public class Selectivo
    {
        [Key]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoCtoA { get; set; }
        public string CodigoCtoB { get; set; }
    }
}