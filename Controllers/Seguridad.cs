using System;
using System.Collections.Generic;
using System.Linq;
using EsquemasSecundarios.Models;
using System.Data.SqlClient;
using System.Web.WebPages;
using System.Web.Mvc;
using System.Web;
using System.Web.Configuration;
using CSCifrado;

namespace EsquemasSecundarios.Controllers
{
    public class Seguridad
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public bool Validar_Credenciales(string name, string password)
        {
            try
            {
                password = CipherUtility.Encript(password);
                var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == name && c.Password == password);
                return usuario_logueado != null ? true : false;
            }
            catch (Exception)
            { }
            return false;
        }        
    }
}

public class TienePermiso : AuthorizeAttribute
{
    private ApplicationDbContext db = new ApplicationDbContext();
    private int Servicio;

    public TienePermiso(int Servicio)
    {
        this.Servicio = Servicio;
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        if (httpContext != null)
        {
            if (base.AuthorizeCore(httpContext))
            {
                var usuario = httpContext?.User?.Identity?.Name ?? null;
                string nombre_usuario = httpContext.User.Identity.Name;
                var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
                int id_usuario = usuario_logueado.Id_Persona;
                short? id_grupo = usuario_logueado.id_grupo;
                List<int> Permisos_Persona = Obtener_Permisos_Persona(id_usuario, id_grupo);
                if (Permisos_Persona.Contains(Servicio))
                    return true;
                else
                    httpContext.Response.Redirect("~/Home/SinAcceso");
            }
            return false;
        }
        return false;
    }

    public List<int> Obtener_Permisos_Persona(int id_persona, short? id_grupo)
    {
        try
        {           
            
            List<int> ids = new List<int>();
            using (SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["ESConnection"].ToString()))
            {
                conexion.Open();
                var lista = new SqlCommand(String.Format(@"
                    SELECT  Servicios.id_servicio
			        FROM    Servicios
					        INNER JOIN PermisosGrupos ON Servicios.id_modulo = PermisosGrupos.id_modulo
							    AND Servicios.id_servicio = PermisosGrupos.id_servicio
			        WHERE   (PermisosGrupos.id_grupo = {1})
					        AND Servicios.id_modulo = 26
					        AND (NOT EXISTS (
                                SELECT id_EAdministrativa,
								    id_grupo,
								    id_persona,
								    id_modulo,
								    id_servicio,
								    estado
								FROM   PermisosPersonas
								WHERE  (estado = 'D')
									AND (id_modulo = Servicios.id_modulo)
									AND (id_servicio = Servicios.id_servicio)
									AND (id_persona = {0})))
			        UNION
			        SELECT  id_servicio
			        FROM    Servicios AS Servicios_1
			        WHERE   Servicios_1.id_modulo = 26
					AND EXISTS (
                        SELECT id_EAdministrativa ,
							id_grupo,
							id_persona,
							id_modulo,
							id_servicio,
							estado
						FROM   PermisosPersonas AS PermisosPersonas_1
						WHERE  (estado = 'A')
							AND (id_modulo = Servicios_1.id_modulo)
							AND (id_servicio = Servicios_1.id_servicio)
							AND (id_persona = {0}))
	                    ", id_persona, id_grupo), conexion).ExecuteReader();

                if (lista.HasRows)
                {
                    while (lista.Read())
                    {
                        ids.Add(int.Parse(lista["id_servicio"].ToString()));
                    }
                    return ids;
                }
                else
                    return new List<int>();
            }            
        }
        catch (Exception)
        {
            return new List<int>();
        }
    }
}