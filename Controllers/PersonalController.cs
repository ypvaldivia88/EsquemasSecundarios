using System.Data;
using System.Linq;
using System.Web.Mvc;
using EsquemasSecundarios.Models;
using System.Web.Security;

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
       
    }
}