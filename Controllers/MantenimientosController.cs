using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EsquemasSecundarios.Models;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace EsquemasSecundarios.Controllers
{
    [TienePermiso(Servicio: 23)]// Servicio: Gestionar Mantenimientos
    public class MantenimientosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region CRUD
        // GET: Mantenimientos
        [AllowAnonymous]
        public ActionResult Index()
        {
            var mantenimientos = db.Mantenimientos.Include(c => c.Esquema).Include(c => c.TipoMantenimiento);            
            return View(mantenimientos.ToList().OrderByDescending(c=> c.IdMantenimiento));
        }

        // GET: Mantenimientos/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimientos mantenimientos = db.Mantenimientos.Find(id);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }
            EsquemaProteccion ep = db.EsquemasProteccion.Find(mantenimientos.IdEsquema);
            TipoMantenimiento tm = db.TipoMantenimiento.Find(mantenimientos.id_Tipo);
            ViewBag.NombreEsquema = ep.Nombre;
            ViewBag.SiglasTipoMant = tm.Siglas;
            ViewBag.TipoMant = tm.Tipo;
            return View(mantenimientos);
        }

        // GET: Mantenimientos/Create
        public ActionResult Create()
        {
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text");
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre");
            var tipo = db.TipoMantenimiento
                .Select(c => new SelectListItem { Value = c.id_Tipo.ToString(), Text = c.Siglas + " - " + c.Tipo });
            ViewBag.id_Tipo = new SelectList(tipo, "Value", "Text");
            return View();
        }

        // POST: Mantenimientos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMantenimiento,CodSubestacion,IdEsquema,id_Tipo,Fecha,Observaciones")] Mantenimientos mantenimientos)
        {
            var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            short EAdmin = usuario_logueado.id_EAdministrativa;

            if (ModelState.IsValid)
            {
                mantenimientos.Id_EAdministrativa = EAdmin;
                mantenimientos.Id_NumAccion = GetNumAccion("I", "ESM", 0);
                db.Mantenimientos.Add(mantenimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text");
            
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre");

            var tipo = db.TipoMantenimiento
                .Select(c => new SelectListItem { Value = c.id_Tipo.ToString(), Text = c.Siglas + " - " + c.Tipo });
            ViewBag.id_Tipo = new SelectList(tipo, "Value", "Text");

            return View(mantenimientos);
        }

        // GET: Mantenimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimientos mantenimientos = db.Mantenimientos.Find(id);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }

            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text", mantenimientos.CodSubestacion);

            var e = db.EsquemasProteccion.Where(c => c.Subestacion == mantenimientos.CodSubestacion);
            ViewBag.IdEsquema = new SelectList(e, "id_Esquema", "Nombre", mantenimientos.IdEsquema);

            var tipo = db.TipoMantenimiento
                .Select(c => new SelectListItem { Value = c.id_Tipo.ToString(), Text = c.Siglas + " - " + c.Tipo });
            ViewBag.id_Tipo = new SelectList(tipo, "Value", "Text", mantenimientos.id_Tipo);

            return View(mantenimientos);
        }

        // POST: Mantenimientos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMantenimiento,CodSubestacion,IdEsquema,id_Tipo,Fecha,Observaciones")] Mantenimientos mantenimientos,
            string CodSubestacion, int IdEsquema, short id_Tipo)
        {
            var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            short EAdmin = usuario_logueado.id_EAdministrativa;

            if (ModelState.IsValid)
            {
                mantenimientos.CodSubestacion = CodSubestacion;
                mantenimientos.IdEsquema = IdEsquema;
                mantenimientos.id_Tipo = id_Tipo;
                mantenimientos.Id_EAdministrativa = EAdmin;
                mantenimientos.Id_NumAccion = GetNumAccion("M", "ESM", mantenimientos.Id_NumAccion ?? 0);
                db.Entry(mantenimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text", CodSubestacion);

            var e = db.EsquemasProteccion.Where(c => c.Subestacion == CodSubestacion);
            ViewBag.IdEsquema = new SelectList(e, "id_Esquema", "Nombre", IdEsquema);

            var tipo = db.TipoMantenimiento
                .Select(c => new SelectListItem { Value = c.id_Tipo.ToString(), Text = c.Siglas + " - " + c.Tipo });
            ViewBag.id_Tipo = new SelectList(tipo, "Value", "Text", id_Tipo);

            return View(mantenimientos);
        }

        // GET: Mantenimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimientos mantenimientos = db.Mantenimientos.Find(id);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEsquema = db.EsquemasProteccion.Find(mantenimientos.IdEsquema).Nombre;
            ViewBag.id_Tipo = db.TipoMantenimiento.Find(mantenimientos.id_Tipo).Tipo;
            return View(mantenimientos);
        }

        // POST: Mantenimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mantenimientos mantenimientos = db.Mantenimientos.Find(id);
            int accion = GetNumAccion("B", "ESM", mantenimientos.Id_NumAccion ?? 0);
            db.Mantenimientos.Remove(mantenimientos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion CRUD

        public int GetNumAccion(string tipoSubAccion, string tipoAccion, int? amod)
        {
            var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            short id_usuario = short.Parse(usuario_logueado.Id_Persona.ToString());
            short EAdmin = usuario_logueado.id_EAdministrativa;
            short EA = usuario_logueado.id_EA_Persona;

            try
            {
                using (SqlConnection conexion = new SqlConnection(WebConfigurationManager.ConnectionStrings["ESConnection"].ToString()))
                {
                    conexion.Open();
                    SqlCommand command = conexion.CreateCommand();
                    command.CommandText = "EXEC GetNumAccion @EAdmin,@tipoSubAccion,@tipoAccion,@usuario,@EA,@amod,@numAccion OUTPUT SELECT @numAccion";
                    command.Parameters.Add(new SqlParameter("EAdmin", EAdmin));
                    command.Parameters.Add(new SqlParameter("tipoSubAccion", tipoSubAccion));
                    command.Parameters.Add(new SqlParameter("tipoAccion", tipoAccion));
                    command.Parameters.Add(new SqlParameter("usuario", id_usuario));
                    command.Parameters.Add(new SqlParameter("EA", EA));
                    command.Parameters.Add(new SqlParameter("amod", amod));
                    command.Parameters.Add(new SqlParameter("numAccion", amod));
                    var dr = command.ExecuteReader();
                    if (dr.Read())
                    {
                        return int.Parse(dr.GetValue(0).ToString());
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception)
            {
                return new int();
            }

        }

        #region AJAX
        public ActionResult CargarEsquemas(string codsub)
        {            
            var e = db.EsquemasProteccion.Where(c => c.Subestacion == codsub);
            ViewBag.IdEsquema = new SelectList(e, "id_Esquema", "Nombre");
            return PartialView("_CargarEsquemas");
        }        
        #endregion

    }
}
