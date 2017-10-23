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
    [TienePermiso(Servicio: 1)]
    public class RelevadoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Relevadores
        [AllowAnonymous]
        public ActionResult Index()
        {
            var relevadores = db.Relevadores.Include(r => r.Plantilla);
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
            return View(relevador);
        }

        // GET: Relevadores/Create
        public ActionResult Create()
        {
            ViewBag.id_Plantilla = new SelectList(db.Plantillas, "id_Plantilla", "Modelo");
            return View();
        }

        // POST: Relevadores/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nro_Serie,Ubicado,Voltaje_Alterno,Voltaje_Directo,id_Plantilla")] Relevador relevador)
        {
            if (ModelState.IsValid)
            {
                db.Relevadores.Add(relevador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Plantilla = new SelectList(db.Plantillas, "id_Plantilla", "Modelo", relevador.id_Plantilla);
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
            ViewBag.id_Plantilla = new SelectList(db.Plantillas, "id_Plantilla", "Modelo", relevador.id_Plantilla);
            return View(relevador);
        }

        // POST: Relevadores/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nro_Serie,Ubicado,Voltaje_Alterno,Voltaje_Directo,id_Plantilla")] Relevador relevador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(relevador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Plantilla = new SelectList(db.Plantillas, "id_Plantilla", "Modelo", relevador.id_Plantilla);
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
            return View(relevador);
        }

        // POST: Relevadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Relevador relevador = db.Relevadores.Find(id);
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
    }
}
