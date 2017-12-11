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
    public class ReportesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reportes
        public ActionResult Averias()
        {
            var averias = db.Averias.Include(c => c.Esquema);
            return View(averias);
        }

        public ActionResult Mantenimientos()
        {
            var mantenimientos = db.Mantenimientos.Include(c => c.Esquema).Include(c => c.TipoMantenimiento);
            return View(mantenimientos.ToList());
        }

        public ActionResult Esquemas()
        {
            return View(db.EsquemasProteccion);
        }

        public ActionResult Relevadores()
        {
            var relevadores = db.Relevadores.Include(r => r.Plantilla).Include(p => p.Plantilla.Fabricante);
            return View(relevadores.ToList());
        }
    }
}