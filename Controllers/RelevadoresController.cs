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
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nro_Serie,Ubicado,Voltaje_Alterno,Voltaje_Directo,id_Plantilla")] Relevador relevador)
        {
            if (ModelState.IsValid)
            {
                relevador.Plantilla = db.Plantillas.Find(relevador.id_Plantilla);
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
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nro_Serie,Ubicado,Voltaje_Alterno,Voltaje_Directo,id_Plantilla")] Relevador relevador)
        {
            if (ModelState.IsValid)
            {
                relevador.Plantilla = db.Plantillas.Find(relevador.id_Plantilla);
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
