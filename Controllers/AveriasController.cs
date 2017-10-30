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
    public class AveriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Averias
        public ActionResult Index()
        {
            var averias = db.Averias.Include(a => a.Esquema).Include(a => a.Subestacion);
            return View(averias.ToList());
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
            ViewBag.Esquema = db.EsquemasProteccion.Find(averias.IdEsquema).Nombre;
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
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text", averias.CodSubestacion);
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre", averias.IdEsquema);
            var personaQueAtendio = db.Personal.FirstOrDefault(c => c.Nombre == PersonaQueAtendio);
            ViewBag.PersonaQueAtendio = new SelectList(db.Personal, "Nombre", "Nombre", personaQueAtendio.Id_Persona);
            var elaboradoPor = db.Personal.FirstOrDefault(c => c.Nombre == ElaboradoPor);
            ViewBag.ElaboradoPor = new SelectList(db.Personal, "Nombre", "Nombre", elaboradoPor.Id_Persona);
            var revisadoPor = db.Personal.FirstOrDefault(c => c.Nombre == RevisadoPor);
            ViewBag.RevisadoPor = new SelectList(db.Personal, "Nombre", "Nombre", revisadoPor.Id_Persona);
            var aprobadoPor = db.Personal.FirstOrDefault(c => c.Nombre == AprobadoPor);
            ViewBag.AprobadoPor = new SelectList(db.Personal, "Nombre", "Nombre", aprobadoPor.Id_Persona);
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

            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre", averias.IdEsquema);

            var personaQueAtendio = db.Personal.FirstOrDefault(c => c.Nombre == averias.PersonaQueAtendio);
            ViewBag.PersonaQueAtendio = new SelectList(db.Personal, "Nombre", "Nombre", personaQueAtendio.Id_Persona);

            var elaboradoPor = db.Personal.FirstOrDefault(c => c.Nombre == averias.ElaboradoPor);
            ViewBag.ElaboradoPor = new SelectList(db.Personal, "Nombre", "Nombre", elaboradoPor.Id_Persona);

            var revisadoPor = db.Personal.FirstOrDefault(c => c.Nombre == averias.RevisadoPor);
            ViewBag.RevisadoPor = new SelectList(db.Personal, "Nombre", "Nombre", revisadoPor.Id_Persona);

            var aprobadoPor = db.Personal.FirstOrDefault(c => c.Nombre == averias.AprobadoPor);
            ViewBag.AprobadoPor = new SelectList(db.Personal, "Nombre", "Nombre", aprobadoPor.Id_Persona);

            return View(averias);
        }

        // POST: Averias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre", IdEsquema);
            var personaQueAtendio = db.Personal.FirstOrDefault(c => c.Nombre == PersonaQueAtendio);
            ViewBag.PersonaQueAtendio = new SelectList(db.Personal, "Nombre", "Nombre", personaQueAtendio.Id_Persona);
            var elaboradoPor = db.Personal.FirstOrDefault(c => c.Nombre == ElaboradoPor);
            ViewBag.ElaboradoPor = new SelectList(db.Personal, "Nombre", "Nombre", elaboradoPor.Id_Persona);
            var revisadoPor = db.Personal.FirstOrDefault(c => c.Nombre == RevisadoPor);
            ViewBag.RevisadoPor = new SelectList(db.Personal, "Nombre", "Nombre", revisadoPor.Id_Persona);
            var aprobadoPor = db.Personal.FirstOrDefault(c => c.Nombre == AprobadoPor);
            ViewBag.AprobadoPor = new SelectList(db.Personal, "Nombre", "Nombre", aprobadoPor.Id_Persona);
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
    }
}
