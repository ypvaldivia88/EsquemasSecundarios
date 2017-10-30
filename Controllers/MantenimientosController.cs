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
    public class MantenimientosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mantenimientos
        public ActionResult Index()
        {
            var mantenimientos = db.Mantenimientos.Include(m => m.Esquema).Include(m => m.Subestacion).Include(m => m.TipoMantenimiento);
            return View(mantenimientos.ToList());
        }

        // GET: Mantenimientos/Details/5
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
            ViewBag.IdEsquema = db.EsquemasProteccion.Find(mantenimientos.IdEsquema).Nombre;
            ViewBag.id_Tipo = db.TipoMantenimiento.Find(mantenimientos.id_Tipo).Tipo;
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
            ViewBag.id_Tipo = new SelectList(db.TipoMantenimiento, "id_Tipo", "Tipo");
            return View();
        }

        // POST: Mantenimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMantenimiento,Subestacion,Esquema,TipoMantenimiento,Fecha,Observaciones")] Mantenimientos mantenimientos,
            string CodSubestacion, int IdEsquema, short id_Tipo)
        {
            if (ModelState.IsValid)
            {
                mantenimientos.CodSubestacion = CodSubestacion;
                mantenimientos.IdEsquema = IdEsquema;
                mantenimientos.id_Tipo = id_Tipo;

                db.Mantenimientos.Add(mantenimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre", IdEsquema);
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text", CodSubestacion);
            ViewBag.id_Tipo = new SelectList(db.TipoMantenimiento, "id_Tipo", "Tipo", id_Tipo);
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
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre", mantenimientos.IdEsquema);
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text", mantenimientos.CodSubestacion);
            ViewBag.id_Tipo = new SelectList(db.TipoMantenimiento, "id_Tipo", "Tipo", mantenimientos.id_Tipo);
            return View(mantenimientos);
        }

        // POST: Mantenimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMantenimiento,Subestacion,Esquema,TipoMantenimiento,Fecha,Observaciones")] Mantenimientos mantenimientos,
            string CodSubestacion, int IdEsquema, short id_Tipo)
        {
            if (ModelState.IsValid)
            {
                mantenimientos.CodSubestacion = CodSubestacion;
                mantenimientos.IdEsquema = IdEsquema;
                mantenimientos.id_Tipo = id_Tipo;
                db.Entry(mantenimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.IdEsquema = new SelectList(db.EsquemasProteccion, "id_Esquema", "Nombre", IdEsquema);
            ViewBag.CodSubestacion = new SelectList(subestaciones, "Value", "Text", CodSubestacion);
            ViewBag.id_Tipo = new SelectList(db.TipoMantenimiento, "id_Tipo", "Tipo", id_Tipo);
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
    }
}
