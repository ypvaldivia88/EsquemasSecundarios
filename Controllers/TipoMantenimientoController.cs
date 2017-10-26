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
    public class TipoMantenimientoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoMantenimiento
        public ActionResult Index()
        {
            return View(db.TipoMantenimiento.ToList());
        }

        // GET: TipoMantenimiento/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMantenimiento tipoMantenimiento = db.TipoMantenimiento.Find(id);
            if (tipoMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMantenimiento);
        }

        // GET: TipoMantenimiento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoMantenimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Tipo,Tipo")] TipoMantenimiento tipoMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.TipoMantenimiento.Add(tipoMantenimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoMantenimiento);
        }

        // GET: TipoMantenimiento/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMantenimiento tipoMantenimiento = db.TipoMantenimiento.Find(id);
            if (tipoMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMantenimiento);
        }

        // POST: TipoMantenimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Tipo,Tipo")] TipoMantenimiento tipoMantenimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoMantenimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoMantenimiento);
        }

        // GET: TipoMantenimiento/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoMantenimiento tipoMantenimiento = db.TipoMantenimiento.Find(id);
            if (tipoMantenimiento == null)
            {
                return HttpNotFound();
            }
            return View(tipoMantenimiento);
        }

        // POST: TipoMantenimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            TipoMantenimiento tipoMantenimiento = db.TipoMantenimiento.Find(id);
            db.TipoMantenimiento.Remove(tipoMantenimiento);
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
