using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsquemasSecundarios.Models
{
    [Table("Personal")]
    public class Personal
    {
        [Key]
        public int Id_Persona { get; set; }

        public string Nombre { get; set; }
        
        public string Password { get; set; }
        
        public short? id_grupo { get; set; }
        public short id_EAdministrativa { get; set; }

        public short id_EA_Persona { get; set; }

    }
}