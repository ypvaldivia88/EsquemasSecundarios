using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("Fabricantes")]
    public class Fabricante
    {
        [Key]
        public int Id_Fabricante { get; set; }

        [Display(Name = "Fabricante")]
        [MaxLength(20)]        
        public string Nombre { get; set; }
        public List<Plantilla> Plantillas { get; set; }
    }
}