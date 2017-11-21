using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EsquemasSecundarios.Models;

namespace EsquemasSecundarios.Controllers
{
    [TienePermiso(Servicio: 9)]
    public class AveriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Averias
        [AllowAnonymous]
        public ActionResult Index()
        {            
            var averias = db.Averias.Include(c => c.Esquema);
            return View(averias);
        }

        // GET: Averias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Averias averias = db.Averias.Find(id);
            if (averias == null)
            {
                return HttpNotFound();
            }
            ViewBag.NombreEsquema = db.EsquemasProteccion.Find(averias.IdEsquema).Nombre;
            var sub = db.Subestacion.Find(averias.CodSubestacion);
            var subtra = db.SubestacionTransmision.Find(averias.CodSubestacion);
            if (sub != null && subtra == null)
                ViewBag.NombreSubestacion = sub.NombreSubestacion;
            else if (sub == null && subtra != null)
                ViewBag.NombreSubestacion = subtra.NombreSubestacion;

            return View(averias);
        }

        // GET: Averias/Create
        public ActionResult Create()
        {
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));

            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text");
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre");
            ViewBag.PersonaQueAtendio = new SelectList(db.Personal, "Nombre", "Nombre");
            ViewBag.ElaboradoPor = new SelectList(db.Personal, "Nombre", "Nombre");
            ViewBag.RevisadoPor = new SelectList(db.Personal, "Nombre", "Nombre");
            ViewBag.AprobadoPor = new SelectList(db.Personal, "Nombre", "Nombre");

            return View();
        }

        // POST: Averias/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAveria,FechaReporte,CodSubestacion,IdEsquema,FechaAtencion,PersonaQueAtendio,DatosReportados,Analisis,Conclusiones,Recomendaciones,ElaboradoPor,RevisadoPor,AprobadoPor")] Averias averias,
            string CodSubestacion , int IdEsquema, string PersonaQueAtendio, string ElaboradoPor, string RevisadoPor, string AprobadoPor)
        {
            if (ModelState.IsValid)
            {
                averias.CodSubestacion = CodSubestacion;
                averias.IdEsquema = IdEsquema;
                averias.PersonaQueAtendio = PersonaQueAtendio;
                averias.ElaboradoPor = ElaboradoPor;
                averias.RevisadoPor = RevisadoPor;
                averias.AprobadoPor = AprobadoPor;
                db.Averias.Add(averias);
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

            ViewBag.PersonaQueAtendio = new SelectList(db.Personal, "Nombre", "Nombre", PersonaQueAtendio);
            ViewBag.ElaboradoPor = new SelectList(db.Personal, "Nombre", "Nombre", ElaboradoPor);
            ViewBag.RevisadoPor = new SelectList(db.Personal, "Nombre", "Nombre", RevisadoPor);
            ViewBag.AprobadoPor = new SelectList(db.Personal, "Nombre", "Nombre", AprobadoPor);

            return View(averias);
        }

        // GET: Averias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Averias averias = db.Averias.Find(id);
            if (averias == null)
            {
                return HttpNotFound();
            }
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text", averias.CodSubestacion);

            var e = db.EsquemasProteccion.Where(c => c.Subestacion == averias.CodSubestacion);
            ViewBag.IdEsquema = new SelectList(e, "id_Esquema", "Nombre", averias.IdEsquema);

            ViewBag.PersonaQueAtendio = new SelectList(db.Personal, "Nombre", "Nombre", averias.PersonaQueAtendio);
            ViewBag.ElaboradoPor = new SelectList(db.Personal, "Nombre", "Nombre", averias.ElaboradoPor);
            ViewBag.RevisadoPor = new SelectList(db.Personal, "Nombre", "Nombre", averias.RevisadoPor);
            ViewBag.AprobadoPor = new SelectList(db.Personal, "Nombre", "Nombre", averias.AprobadoPor);

            return View(averias);
        }

        // POST: Averias/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAveria,FechaReporte,CodSubestacion,IdEsquema,FechaAtencion,PersonaQueAtendio,DatosReportados,Analisis,Conclusiones,Recomendaciones,ElaboradoPor,RevisadoPor,AprobadoPor")] Averias averias,
            string CodSubestacion, int IdEsquema, string PersonaQueAtendio, string ElaboradoPor, string RevisadoPor, string AprobadoPor)
        {
            if (ModelState.IsValid)
            {
                averias.CodSubestacion = CodSubestacion;
                averias.IdEsquema = IdEsquema;
                averias.PersonaQueAtendio = PersonaQueAtendio;
                averias.ElaboradoPor = ElaboradoPor;
                averias.RevisadoPor = RevisadoPor;
                averias.AprobadoPor = AprobadoPor;
                db.Entry(averias).State = EntityState.Modified;
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
            ViewBag.PersonaQueAtendio = new SelectList(db.Personal, "Nombre", "Nombre", PersonaQueAtendio);
            ViewBag.ElaboradoPor = new SelectList(db.Personal, "Nombre", "Nombre", ElaboradoPor);
            ViewBag.RevisadoPor = new SelectList(db.Personal, "Nombre", "Nombre", RevisadoPor);
            ViewBag.AprobadoPor = new SelectList(db.Personal, "Nombre", "Nombre", AprobadoPor);
            return View(averias);
        }

        // GET: Averias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Averias averias = db.Averias.Find(id);
            if (averias == null)
            {
                return HttpNotFound();
            }
            ViewBag.Esquema = db.EsquemasProteccion.Find(averias.IdEsquema).Nombre;
            return View(averias);
        }

        // POST: Averias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Averias averias = db.Averias.Find(id);
            db.Averias.Remove(averias);
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
