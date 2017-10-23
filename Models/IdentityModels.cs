using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EsquemasSecundarios.Models
{    

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("ESConnection")
        {
        }

        public DbSet<Plantilla> Plantillas { get; set; }
        public DbSet<Relevador> Relevadores { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Funcion> Funciones { get; set; }
        public DbSet<EsquemaProteccion>EsquemasProteccion { get; set; }
        public DbSet<Desconectivo> Desconectivos { get; set; }
        public DbSet<TransformadorPotencial> TransformadoresPotencial { get; set  ; }
        public DbSet<TransformadorCorriente> TransformadoresCorriente { get; set  ; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Subestacion> Subestacion { get; set; }
        public DbSet<SubestacionTransmision> SubestacionTransmision { get; set; }
        public DbSet<LineaTransmision> LineaTransmision { get; set; }
        public DbSet<CircuitoPrimario> CircuitoPrimario { get; set; }
        public DbSet<CircuitoSubtransmision> CircuitoSubtransmision { get; set; }
        public DbSet<Barra> Barras { get; set; }
        public DbSet<VoltajeSistema> VoltajesSistemas { get; set; }
        public DbSet<TransformadorSubtransmision> TransformadorSubtransmision { get; set; }
        public DbSet<TransformadorTransmision> TransformadorTransmision { get; set; }
        public DbSet<LineaSubestacion> LineaSubestacion { get; set; }
        public DbSet<SubestacionCabezaLinea> SubestacionCabezaLinea { get; set; }
        public DbSet<Mantenimientos> Mantenimientos { get; set; }


        public DbSet<Plantilla_Funcion> Plantilla_Funcion { get; set; }    
        public DbSet<Esquema_Relevador> Esquema_Rele { get; set; }
        public DbSet<Esquema_Desconectivo> Esquema_Desconectivo { get; set; }
        public DbSet<Esquema_TC> Esquema_TC { get; set; }
        public DbSet<Esquema_TP> Esquema_TP { get; set; }
        public DbSet<Relevador_Funcion> Relevador_Funcion { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

}