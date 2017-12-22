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
    public class PlantillasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Plantillas
        [AllowAnonymous]
        public ActionResult Index()
        {
            var plantillas = db.Plantillas.Include(p => p.Fabricante);
            return View(plantillas.ToList());
        }

        // GET: Plantillas/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Plantilla plantilla = db.Plantillas.Find(id);

            var fabricante = db.Plantillas.Include(p => p.Fabricante).FirstOrDefault().Fabricante.Nombre;

            if (plantilla == null)
            {
                return HttpNotFound();
            }

            var f =(
                from pl in db.Plantillas
                join pf in db.Plantilla_Funcion on pl.id_Plantilla equals pf.id_Plantilla
                join fun in db.Funciones on pf.id_Funcion equals fun.Id_Esquema
                where pl.id_Plantilla == id
                select fun.Descripcion
                ).ToList();

            ViewBag.Fabricante = fabricante;
            ViewBag.Funciones = f;

            return View(plantilla);
        }

        // GET: Plantillas/Create
        public ActionResult Create()
        {
            ViewBag.Id_Fabricante = new SelectList(db.Fabricantes, "Id_Fabricante", "Nombre");
            ViewBag.Funciones = new SelectList(db.Funciones, "Id_Esquema", "Descripcion");
            return View();
        }

        // POST: Plantillas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_Plantilla,Modelo,Id_Fabricante")] Plantilla plantilla, int[] Funciones)
        {
            if (ModelState.IsValid)
            {
                db.Plantillas.Add(plantilla);
                db.SaveChanges();

                for (int i = 0; i < Funciones.Length; i++)
                {
                    Plantilla_Funcion pf = new Plantilla_Funcion();
                    pf.id_Plantilla = plantilla.id_Plantilla;
                    pf.id_Funcion = Funciones[i];
                    db.Plantilla_Funcion.Add(pf);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Id_Fabricante = new SelectList(db.Fabricantes, "Id_Fabricante", "Nombre", plantilla.Id_Fabricante);
            ViewBag.Funciones = new SelectList(db.Funciones, "Id_Esquema", "Descripcion");
            return View(plantilla);
        }

        // GET: Plantillas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plantilla plantilla = db.Plantillas.Find(id);
            if (plantilla == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Fabricante = new SelectList(db.Fabricantes, "Id_Fabricante", "Nombre", plantilla.Id_Fabricante);

            var funcIds = (
                from pf in db.Plantilla_Funcion
                where pf.id_Plantilla.Equals(plantilla.id_Plantilla)
                select pf.id_Funcion
            ).ToList();

            var allfunc = db.Funciones.ToList().OrderBy(c => c.Descripcion);

            ViewBag.Funciones = new MultiSelectList(allfunc, "Id_Esquema", "Descripcion", funcIds);

            return View(plantilla);
        }

        // POST: Plantillas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Plantilla,Modelo,Id_Fabricante")] Plantilla plantilla, int[] Funciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plantilla).State = EntityState.Modified;
                db.SaveChanges();
                if (Funciones != null )
                {
                    for (int i = 0; i < Funciones.Length; i++)
                    {
                        Plantilla_Funcion pf = new Plantilla_Funcion();
                        pf.id_Plantilla = plantilla.id_Plantilla;
                        pf.id_Funcion = Funciones[i];
                        db.Plantilla_Funcion.Add(pf);
                        db.SaveChanges();
                    }
                }
                

                return RedirectToAction("Index");
            }
            ViewBag.Id_Fabricante = new SelectList(db.Fabricantes, "Id_Fabricante", "Nombre", plantilla.Id_Fabricante);
            return View(plantilla);
        }

        // GET: Plantillas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plantilla plantilla = db.Plantillas.Find(id);
            if (plantilla == null)
            {
                return HttpNotFound();
            }
            return View(plantilla);
        }

        // POST: Plantillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plantilla plantilla = db.Plantillas.Find(id);
            db.Plantillas.Remove(plantilla);
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
