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
    [TienePermiso(Servicio: 21)]//Servicio: Ver Reportes
    public class ReportesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reportes
        public ActionResult Averias()
        {
            SelectorSubestaciones();
            var averias = db.Averias.Include(c => c.Esquema);
            return View(averias);
        }

        public ActionResult Mantenimientos()
        {
            SelectorSubestaciones();
            var mantenimientos = db.Mantenimientos.Include(c => c.Esquema).Include(c => c.TipoMantenimiento);
            return View(mantenimientos.ToList());
        }

        public ActionResult Esquemas()
        {
            SelectorSubestaciones();
            return View(db.EsquemasProteccion);
        }

        public ActionResult Relevadores()
        {
            var relevadores = db.Relevadores.Include(r => r.Plantilla).Include(p => p.Plantilla.Fabricante);
            return View(relevadores.ToList());
        }

        public ActionResult CargarAverias(DateTime FechaInicio, DateTime FechaFin, string codSub)
        {
            var averias = db.Averias.Include(c => c.Esquema).Where(c => c.FechaReporte >= FechaInicio && c.FechaReporte <= FechaFin);
            if (codSub != "")
            {
                averias = db.Averias.Include(c => c.Esquema).Where(c => c.FechaReporte >= FechaInicio && c.FechaReporte <= FechaFin && c.CodSubestacion == codSub);
            }
            return PartialView("_CargarAverias",averias);
        }

        public ActionResult CargarMantenimientos(DateTime FechaInicio, DateTime FechaFin, string codSub)
        {
            var mantenimientos = db.Mantenimientos.Include(c => c.Esquema).Include(c => c.TipoMantenimiento)
                .Where(c => c.Fecha >= FechaInicio && c.Fecha <= FechaFin);
            if (codSub != "")
            {
                mantenimientos = db.Mantenimientos.Include(c => c.Esquema).Include(c => c.TipoMantenimiento)
                    .Where(c => c.Fecha >= FechaInicio && c.Fecha <= FechaFin && c.CodSubestacion == codSub);
            }
            return PartialView("_CargarMantenimientos", mantenimientos);
        }

        public ActionResult CargarEsquemas(string codSub)
        {
            var esquemas = db.EsquemasProteccion.Where(c => c.Subestacion == codSub);
            return PartialView("_CargarEsquemas", esquemas);
        }

        public void SelectorSubestaciones()
        {
            var instalaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                    .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }))
                .Union(db.LineaTransmision
                    .Select(c => new SelectListItem { Value = c.Codigolinea, Text = c.Codigolinea + " - " + c.NombreCircuito })
                    .Union(db.CircuitoPrimario
                        .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito + " - " + c.NombreAntiguo }))
                    .Union(db.CircuitoSubtransmision
                        .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito + " - " + c.NombreCircuito }))
                );

            ViewBag.Subestaciones = new SelectList(instalaciones, "Value", "Text");
        }
    }
}