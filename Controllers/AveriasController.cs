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
            ViewBag.Personas = new SelectList(db.Personal, "Nombre", "Nombre");
            return View();
        }

        // POST: Averias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAveria,FechaReporte,CodSubestacion,IdEsquema,FechaAtencion,PersonaQueAtendio,DatosReportados,Analisis,Conclusiones,Recomendaciones,ElaboradoPor,RevisadoPor,AprobadoPor")] Averias averias)
        {
            if (ModelState.IsValid)
            {
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
            return View(averias);
        }

        // POST: Averias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAveria,FechaReporte,CodSubestacion,IdEsquema,FechaAtencion,PersonaQueAtendio,DatosReportados,Analisis,Conclusiones,Recomendaciones,ElaboradoPor,RevisadoPor,AprobadoPor")] Averias averias,
            string subestacion, int esquema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(averias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text", subestacion);
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre", esquema);
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
