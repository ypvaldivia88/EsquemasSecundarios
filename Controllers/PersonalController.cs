using System.Data;
using System.Linq;
using System.Web.Mvc;
using EsquemasSecundarios.Models;
using System.Web.Security;
using System.Web.Configuration;
using System.Configuration;
using System;
using System.Data.SqlClient;

namespace EsquemasSecundarios.Controllers
{
    public class PersonalController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        Seguridad seguridad = new Seguridad();

        // GET: Login
        public ActionResult Login()
        {
            ViewBag.Nombre = new SelectList(db.Personal, "Nombre", "Nombre");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Personal persona )
        {
            if (seguridad.Validar_Credenciales(persona.Nombre, persona.Password))
            {
                FormsAuthentication.SetAuthCookie(persona.Nombre, false);
                return Redirect("~/Home");
            }
            else
                ModelState.AddModelError("Nombre", "Las credenciales no son válidas.");

            ViewBag.Nombre = new SelectList(db.Personal, "Nombre", "Nombre");
            return View(persona);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Personal/Login");
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ConfigurarConexion(string server, string database, string username, string userpass)
        {
            string ConnStr = "data source=" + server + ";initial catalog=" + database + ";User ID=" + username + ";Password=" + userpass + ";MultipleActiveResultSets=False;App=EntityFramework";            
            try
            {
                using (SqlConnection conexion = new SqlConnection(ConnStr))
                {
                    conexion.Open();
                    var lista = new SqlCommand(String.Format(@"SELECT * FROM Personal"), conexion).ExecuteReader();
                }
                Configuration Config = WebConfigurationManager.OpenWebConfiguration("~");
                ConnectionStringsSection conSetting = (ConnectionStringsSection)Config.GetSection("connectionStrings");
                conSetting.ConnectionStrings["ESConnection"].ConnectionString = ConnStr;
                Config.Save();
            }
            catch (Exception)
            {
                ViewBag.Error = "Los datos de conexión no son válidos";
                return Redirect("~/Personal/Login#signup");
            }
            return Redirect("~/Personal/Login");
        }
    }
}