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
            return View(db.Mantenimientos.ToList());
        }

        // GET: Mantenimientos/Details/5
        public ActionResult Details(DateTime Fecha, string CodEquipo)
        {
            if (Fecha == null || CodEquipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimientos mantenimientos = db.Mantenimientos.Find(Fecha, CodEquipo);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientos);
        }

        // GET: Mantenimientos/Create
        public ActionResult Create()
        {
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.Subestacion = new SelectList(subestaciones, "Value", "Text");

            return View();
        }

        // POST: Mantenimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fecha,CodEquipo,Mantenimiento,Observaciones,FechaProximo,MttoProximo,Subestacion")] Mantenimientos mantenimientos)
        {
            if (ModelState.IsValid)
            {
                db.Mantenimientos.Add(mantenimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mantenimientos);
        }

        // GET: Mantenimientos/Edit/5
        public ActionResult Edit(DateTime Fecha, string CodEquipo)
        {
            if (Fecha == null || CodEquipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimientos mantenimientos = db.Mantenimientos.Find(Fecha,CodEquipo);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }

            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            ViewBag.Subestacion = new SelectList(subestaciones, "Value", "Text");

            return View(mantenimientos);
        }

        // POST: Mantenimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fecha,CodEquipo,Mantenimiento,Observaciones,FechaProximo,MttoProximo,Subestacion")] Mantenimientos mantenimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mantenimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mantenimientos);
        }

        // GET: Mantenimientos/Delete/5
        public ActionResult Delete(DateTime Fecha, string CodEquipo)
        {
            if (Fecha == null || CodEquipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mantenimientos mantenimientos = db.Mantenimientos.Find(Fecha, CodEquipo);
            if (mantenimientos == null)
            {
                return HttpNotFound();
            }
            return View(mantenimientos);
        }

        // POST: Mantenimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime Fecha, string CodEquipo)
        {
            Mantenimientos mantenimientos = db.Mantenimientos.Find(Fecha, CodEquipo);
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
