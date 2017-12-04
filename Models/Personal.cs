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
        [Key,Column(Order = 1)]
        public int Id_Persona { get; set; }

        [Key,Column(Order = 2)]
        public int id_EAdministrativa_Prov { get; set; }

        public string Nombre { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public short? id_grupo { get; set; }

        public short id_EA_Persona { get; set; }

    }
}