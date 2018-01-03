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
    [TienePermiso(Servicio: 1)]//Servicio: Nomenclar relevadores
    public class RelevadoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Relevadores
        [AllowAnonymous]
        public ActionResult Index()
        {
            var relevadores = db.Relevadores.Include(r => r.Plantilla).Include(p => p.Plantilla.Fabricante);
            return View(relevadores.ToList());
        }

        // GET: Relevadores/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relevador relevador = db.Relevadores.Find(id);
            if (relevador == null)
            {
                return HttpNotFound();
            }

            var f = (
                from pl in db.Plantillas
                join pf in db.Plantilla_Funcion on pl.id_Plantilla equals pf.id_Plantilla
                join fun in db.Funciones on pf.id_Funcion equals fun.Id_Esquema
                where pl.id_Plantilla == relevador.id_Plantilla
                select fun.Descripcion
                ).ToList();

            ViewBag.Fabricante = db.Plantillas.Include(c=> c.Fabricante).Where(c => c.id_Plantilla == relevador.id_Plantilla).Select(c=> c.Fabricante.Nombre).FirstOrDefault();
            ViewBag.Modelo = db.Plantillas.Where(c => c.id_Plantilla == relevador.id_Plantilla).Select(c => c.Modelo).FirstOrDefault(); ;
            ViewBag.Funciones = f;

            return View(relevador);
        }

        // GET: Relevadores/Create
        public ActionResult Create()
        {
            var plantillas = (
                from pl in db.Plantillas
                join fa in db.Fabricantes on pl.Id_Fabricante equals fa.Id_Fabricante
                select new SelectListItem
                {
                    Value = pl.id_Plantilla.ToString(),
                    Text = "Modelo: " + pl.Modelo + " Fabricante: " + fa.Nombre
                }
            );

            ViewBag.id_Plantilla = new SelectList(plantillas, "Value", "Text");
            return View();
        }

        // POST: Relevadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nro_Serie,Ubicado,Voltaje_Alterno,Voltaje_Directo,id_Plantilla")] Relevador relevador)
        {
            var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            short EAdmin = usuario_logueado.id_EAdministrativa;

            if (ModelState.IsValid)
            {
                relevador.Plantilla = db.Plantillas.Find(relevador.id_Plantilla);
                relevador.Id_EAdministrativa = EAdmin;
                relevador.Id_NumAccion = GetNumAccion("I", "ESR", 0);
                db.Relevadores.Add(relevador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var plantillas = (
                from pl in db.Plantillas
                join fa in db.Fabricantes on pl.Id_Fabricante equals fa.Id_Fabricante
                select new SelectListItem
                {
                    Value = pl.id_Plantilla.ToString(),
                    Text = "Modelo: " + pl.Modelo + " Fabricante: " + fa.Nombre
                }
            );
            ViewBag.id_Plantilla = new SelectList(plantillas, "Value", "Text", relevador.id_Plantilla);
            return View(relevador);
        }

        // GET: Relevadores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relevador relevador = db.Relevadores.Find(id);
            if (relevador == null)
            {
                return HttpNotFound();
            }
            var plantillas = (
                from pl in db.Plantillas
                join fa in db.Fabricantes on pl.Id_Fabricante equals fa.Id_Fabricante
                select new SelectListItem
                {
                    Value = pl.id_Plantilla.ToString(),
                    Text = "Modelo: " + pl.Modelo + " Fabricante: " + fa.Nombre
                }
            );
            ViewBag.id_Plantilla = new SelectList(plantillas, "Value", "Text", relevador.id_Plantilla);
            return View(relevador);
        }

        // POST: Relevadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nro_Serie,Ubicado,Voltaje_Alterno,Voltaje_Directo,id_Plantilla")] Relevador relevador)
        {
            var usuario = System.Web.HttpContext.Current.User?.Identity?.Name ?? null;
            string nombre_usuario = System.Web.HttpContext.Current.User.Identity.Name;
            var usuario_logueado = db.Personal.FirstOrDefault(c => c.Nombre == nombre_usuario);
            short EAdmin = usuario_logueado.id_EAdministrativa;

            if (ModelState.IsValid)
            {
                relevador.Plantilla = db.Plantillas.Find(relevador.id_Plantilla);
                relevador.Id_EAdministrativa = EAdmin;
                relevador.Id_NumAccion = GetNumAccion("M", "ESR", relevador.Id_NumAccion ?? 0);
                db.Entry(relevador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var plantillas = (
                from pl in db.Plantillas
                join fa in db.Fabricantes on pl.Id_Fabricante equals fa.Id_Fabricante
                select new SelectListItem
                {
                    Value = pl.id_Plantilla.ToString(),
                    Text = "Modelo: " + pl.Modelo + " Fabricante: " + fa.Nombre
                }
            );
            ViewBag.id_Plantilla = new SelectList(plantillas, "Value", "Text");
            return View(relevador);
        }

        // GET: Relevadores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Relevador relevador = db.Relevadores.Find(id);
            if (relevador == null)
            {
                return HttpNotFound();
            }

            var f = (
                from pl in db.Plantillas
                join pf in db.Plantilla_Funcion on pl.id_Plantilla equals pf.id_Plantilla
                join fun in db.Funciones on pf.id_Funcion equals fun.Id_Esquema
                where pl.id_Plantilla == relevador.id_Plantilla
                select fun.Descripcion
                ).ToList();

            ViewBag.Fabricante = db.Plantillas.Include(c => c.Fabricante).Where(c => c.id_Plantilla == relevador.id_Plantilla).Select(c => c.Fabricante.Nombre).FirstOrDefault();
            ViewBag.Modelo = db.Plantillas.Where(c => c.id_Plantilla == relevador.id_Plantilla).Select(c => c.Modelo).FirstOrDefault(); ;
            ViewBag.Funciones = f;

            return View(relevador);
        }

        // POST: Relevadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Relevador relevador = db.Relevadores.Find(id);
            int accion = GetNumAccion("B", "ESR", relevador.Id_NumAccion ?? 0);
            db.Relevadores.Remove(relevador);
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

    }
}
