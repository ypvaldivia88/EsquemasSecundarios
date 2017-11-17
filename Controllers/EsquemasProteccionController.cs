using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EsquemasSecundarios.Models;

namespace EsquemasSecundarios.Controllers
{
    [TienePermiso(Servicio: 9)]
    public class EsquemasProteccionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region CRUD

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.EsquemasProteccion.ToList());
        }

        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EsquemaProteccion esquemaProteccion = db.EsquemasProteccion.Find(id);
            if (esquemaProteccion == null)
            {
                return HttpNotFound();
            }

            ViewBag.Interruptores = (
                from ed in db.Esquema_Desconectivo
                where ed.esquema.Equals(esquemaProteccion.id_Esquema)
                select ed.desconectivo
                ).ToList();

            ViewBag.TC = (
                from e in db.Esquema_TC
                where e.esquema.Equals(esquemaProteccion.id_Esquema)
                select e.TC
                ).ToList();

            ViewBag.TP = (
                from e in db.Esquema_TP
                where e.esquema.Equals(esquemaProteccion.id_Esquema)
                select e.TP
                ).ToList();

            ViewBag.Relevadores = (
                from e in db.Esquema_Rele
                where e.esquema.Equals(esquemaProteccion.id_Esquema)
                select e.rele
                ).ToList();

            return View(esquemaProteccion);
        }

        public ActionResult Create()
        {
            Inicializar();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(
            [Bind(Include = "id_Esquema,Nombre,Subestacion,Tipo_Equipo_Primario,Elemento_Electrico")] EsquemaProteccion esquemaProteccion,
            int instalacion, string Linea, string[] Interruptores, string[] TC, string[] TP, string[] Relevadores, string RelevadorFunc, int[] Funciones
        )
        {
            Inicializar();
            if (ModelState.IsValid)
            {
                if (instalacion == 1)
                {
                    esquemaProteccion.Subestacion = Linea;
                    esquemaProteccion.Tipo_Equipo_Primario = "Línea";
                    esquemaProteccion.Elemento_Electrico = Linea;
                }

                EsquemaProteccion ep = db.EsquemasProteccion.Add(esquemaProteccion);
                db.Entry(ep).State = EntityState.Added;

                if (Interruptores != null)
                {
                    foreach (var item in Interruptores)
                    {
                        Esquema_Desconectivo e = new Esquema_Desconectivo();
                        e.desconectivo = item;
                        e.esquema = esquemaProteccion.id_Esquema;
                        db.Esquema_Desconectivo.Add(e);
                    }
                }

                if (TC != null)
                {
                    foreach (var item in TC)
                    {
                        Esquema_TC e = new Esquema_TC();
                        e.TC = item;
                        e.esquema = esquemaProteccion.id_Esquema;
                        db.Esquema_TC.Add(e);
                    }
                }

                if (TP != null)
                {
                    foreach (var item in TP)
                    {
                        Esquema_TP e = new Esquema_TP();
                        e.TP = item;
                        e.esquema = esquemaProteccion.id_Esquema;
                        db.Esquema_TP.Add(e);
                    }
                }

                if (Relevadores != null)
                {
                    foreach (var item in Relevadores)
                    {
                        Esquema_Relevador e = new Esquema_Relevador();
                        e.rele = item;
                        e.esquema = esquemaProteccion.id_Esquema;
                        db.Esquema_Rele.Add(e);
                    }
                }

                if (Funciones != null)
                {
                    var PlantillaId = (
                        from p in db.Plantillas
                        join r in db.Relevadores on p.id_Plantilla equals r.id_Plantilla
                        where r.Nro_Serie.Contains(RelevadorFunc)
                        select p.id_Plantilla
                    ).ToList();

                    foreach (var item in Funciones)
                    {
                        Plantilla_Funcion pf = new Plantilla_Funcion();
                        pf.id_Funcion = item;
                        pf.id_Plantilla = PlantillaId[0];
                        db.Plantilla_Funcion.Add(pf);
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Details", new { id = esquemaProteccion.id_Esquema });
            }
            return View(esquemaProteccion);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EsquemaProteccion esquemaProteccion = db.EsquemasProteccion.Find(id);
            if (esquemaProteccion == null)
            {
                return HttpNotFound();
            }
            Inicializar(esquemaProteccion);
            return View(esquemaProteccion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_Esquema,Nombre,Subestacion,Tipo_Equipo_Primario,Elemento_Electrico")] EsquemaProteccion esquemaProteccion,
            int instalacion, string Linea, string[] Interruptores, string[] TC, string[] TP, string[] Relevadores, string RelevadorFunc, int[] Funciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(esquemaProteccion).State = EntityState.Modified;

                if (Interruptores != null)
                {
                    db.Esquema_Desconectivo.RemoveRange(db.Esquema_Desconectivo.Where(c => c.esquema == esquemaProteccion.id_Esquema));
                    foreach (var item in Interruptores)
                    {
                        Esquema_Desconectivo e = new Esquema_Desconectivo();
                        e.desconectivo = item;
                        e.esquema = esquemaProteccion.id_Esquema;
                        db.Esquema_Desconectivo.Add(e);
                    }
                }

                if (TC != null)
                {
                    db.Esquema_TC.RemoveRange(db.Esquema_TC.Where(c => c.esquema == esquemaProteccion.id_Esquema));
                    foreach (var item in TC)
                    {
                        Esquema_TC e = new Esquema_TC();
                        e.TC = item;
                        e.esquema = esquemaProteccion.id_Esquema;
                        db.Esquema_TC.Add(e);
                    }
                }

                if (TP != null)
                {
                    db.Esquema_TP.RemoveRange(db.Esquema_TP.Where(c => c.esquema == esquemaProteccion.id_Esquema));
                    foreach (var item in TP)
                    {
                        Esquema_TP e = new Esquema_TP();
                        e.TP = item;
                        e.esquema = esquemaProteccion.id_Esquema;
                        db.Esquema_TP.Add(e);
                    }
                }

                if (Relevadores != null)
                {
                    db.Esquema_Rele.RemoveRange(db.Esquema_Rele.Where(c => c.esquema == esquemaProteccion.id_Esquema));
                    foreach (var item in Relevadores)
                    {
                        Esquema_Relevador e = new Esquema_Relevador();
                        e.rele = item;
                        e.esquema = esquemaProteccion.id_Esquema;
                        db.Esquema_Rele.Add(e);
                    }
                }

                if (Funciones != null)
                {
                    var PlantillaId = (
                        from p in db.Plantillas
                        join r in db.Relevadores on p.id_Plantilla equals r.id_Plantilla
                        where r.Nro_Serie.Contains(RelevadorFunc)
                        select p.id_Plantilla
                    ).ToList();

                    foreach (var item in Funciones)
                    {
                        Plantilla_Funcion pf = new Plantilla_Funcion();
                        pf.id_Funcion = item;
                        pf.id_Plantilla = PlantillaId[0];
                        db.Plantilla_Funcion.Add(pf);
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Details", new { id = esquemaProteccion.id_Esquema });
            }

            Inicializar(esquemaProteccion);

            return View(esquemaProteccion);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EsquemaProteccion esquemaProteccion = db.EsquemasProteccion.Find(id);
            if (esquemaProteccion == null)
            {
                return HttpNotFound();
            }
            return View(esquemaProteccion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EsquemaProteccion esquemaProteccion = db.EsquemasProteccion.Find(id);
            db.EsquemasProteccion.Remove(esquemaProteccion);
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

        #endregion

        #region Vistas Parciales AJAX

        public ActionResult VPInstalaciones(string ti)
        {
            if (ti == "Subestacion")//tipo de instalacion 1 = subestacion
            {
                var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));

                ViewBag.Subestacion = new SelectList(subestaciones, "Value", "Text");
            }

            if (ti == "Linea")//tipo de instalacion 2 = linea
            {
                var lineas = db.LineaTransmision
                .Select(c => new SelectListItem { Value = c.Codigolinea, Text = c.Codigolinea + " - " + c.NombreCircuito })
                .Union(db.CircuitoPrimario
                .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito + " - " + c.NombreAntiguo }))
                .Union(db.CircuitoSubtransmision
                .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito + " - " + c.NombreCircuito }));

                ViewBag.Linea = new SelectList(lineas, "Value", "Text");                
            }

            ViewBag.TipoInstalacion = ti;
            return PartialView("_VPInstalaciones");
        }

        public ActionResult VPInterruptores(string sub, string mult)
        {
            var interruptores = db.Desconectivos
                .Where(c => c.UbicadaEn == sub && c.TipoSeccionalizador == "4")
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo })
                .ToList();
                
            ViewBag.Interruptores = new SelectList(interruptores, "Value", "Text");
            ViewBag.Multiple = mult;
            return PartialView("_VPInterruptores");
        }

        public ActionResult VPElementoElectrico(string e, string codsub)
        {
            if (e == "barra")
            {
                var barras = (
                    from sb in db.Barras
                    join vs in db.VoltajesSistemas
                    on sb.ID_Voltaje equals vs.Id_VoltajeSistema
                    where sb.Subestacion.Contains(codsub)
                    select new SelectListItem { Value = sb.codigo, Text = sb.codigo + " - " + vs.Voltaje.ToString() }
                ).ToList();
                ViewBag.Elemento_Electrico = new SelectList(barras, "Value", "Text");
                return PartialView("_VPElementoElectrico");
            }
            else if (e == "transformador")
            {
                var transformadores = db.TransformadorTransmision
                    .Where(c => c.Codigo.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Codigo + " - " + c.Nombre })
                    .Union(db.TransformadorSubtransmision
                    .Where(c => c.Codigo.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Nombre, Text = c.Codigo + " - " + c.Nombre })
                    );

                ViewBag.Elemento_Electrico = new SelectList(transformadores, "Value", "Text");
                return PartialView("_VPElementoElectrico");
            }
            else if (e == "linea")
            {
                var lineas =
                    db.LineaSubestacion
                    .Where(c => c.Subestacion.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Circuito, Text = c.Circuito })
                    .Union
                    (
                        db.CircuitoPrimario
                        .Where(c => c.SubAlimentadora.Contains(codsub))
                        .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito })
                        .Union
                        (
                            db.CircuitoSubtransmision
                            .Where(c => c.SubestacionTransmision.Contains(codsub))
                            .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito })
                        )
                    )
                    .Union
                    (
                            db.SubestacionCabezaLinea
                            .Where(c => c.SubestacionTransmicion.Contains(codsub))
                            .Select(c => new SelectListItem { Value = c.Codigolinea, Text = c.Codigolinea })
                    )
                    .Union
                    (
                        db.CircuitoSubtransmision
                        .Where(c => c.SubestacionTransmision.Contains(codsub))
                        .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito })
                        .Union
                        (
                            db.CircuitoPrimario
                            .Where(c => c.SubAlimentadora.Contains(codsub))
                            .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito })
                        )
                    );

                ViewBag.Elemento_Electrico = new SelectList(lineas, "Value", "Text");
                return PartialView("_VPElementoElectrico");
            }
            else
            {
                return PartialView("_VPElementoElectrico");
            }
        }

        public ActionResult Transformadores(string codsub)
        {
            var tc = db.TransformadoresCorriente
                    .Where(c => c.CodSub.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Nro_Serie, Text = c.Nro_Serie });
            ViewBag.TC = new SelectList(tc, "Value", "Text");

            var tp = db.TransformadoresPotencial
                    .Where(c => c.CodSub.Contains(codsub))
                    .Select(c => new SelectListItem { Value = c.Nro_Serie, Text = c.Nro_Serie });
            ViewBag.TP = new SelectList(tp, "Value", "Text");

            return PartialView("_Transformadores");
        }

        public ActionResult Funciones(string NroSerie)
        {
            var funcIds = (
                from rf in db.Relevador_Funcion                
                where rf.Nro_Serie_R.Contains(NroSerie)
                select rf.id_Funcion
            ).ToList();

            var allfunc = db.Funciones.ToList().OrderBy(c => c.Descripcion);

            ViewBag.Funciones = new MultiSelectList(allfunc, "Id_Esquema", "Descripcion", funcIds);

            return PartialView("_Funciones");
        }

        public string GuardarFunciones(string rele, int[] func)
        {
            db.Relevador_Funcion.RemoveRange(db.Relevador_Funcion.Where(c => c.Nro_Serie_R == rele));
            foreach (var item in func)
            {
                Relevador_Funcion rf = new Relevador_Funcion();
                rf.Nro_Serie_R = rele;
                rf.id_Funcion = item;
                db.Relevador_Funcion.Add(rf);
                db.SaveChanges();
            }
            return "Se han realizado los cambios correctamente";
        }
        
        #endregion

        public void Inicializar()
        {
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));
            
            var lineas = db.LineaTransmision
                .Select(c => new SelectListItem { Value = c.Codigolinea, Text = c.Codigolinea + " - " + c.NombreCircuito })
                .Union(db.CircuitoPrimario
                .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito + " - " + c.NombreAntiguo }))
                .Union(db.CircuitoSubtransmision
                .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito + " - " + c.NombreCircuito }));

            ViewBag.Subestacion = new SelectList(subestaciones, "Value", "Text");
            ViewBag.Linea = new SelectList(lineas, "Value", "Text");
            ViewBag.Interruptores = new SelectList(db.Desconectivos.ToList(), "Codigo", "Codigo");
            ViewBag.Relevadores = new SelectList(db.Relevadores.ToList(), "Nro_Serie", "Nro_Serie");
            ViewBag.RelevadorFunc = new SelectList(db.Relevadores.ToList(), "Nro_Serie", "Nro_Serie");
            ViewBag.TC = new SelectList(db.TransformadoresCorriente.ToList(), "Nro_Serie", "Nro_Serie");
            ViewBag.TP = new SelectList(db.TransformadoresPotencial.ToList(), "Nro_Serie", "Nro_Serie");
        }

        public void Inicializar(EsquemaProteccion esquemaProteccion)
        {
            var subestaciones = db.Subestacion
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion })
                .Union(db.SubestacionTransmision
                .Select(c => new SelectListItem { Value = c.Codigo, Text = c.Codigo + " - " + c.NombreSubestacion }));   
            var lineas = db.LineaTransmision
                .Select(c => new SelectListItem { Value = c.Codigolinea, Text = c.Codigolinea + " - " + c.NombreCircuito })
                .Union(db.CircuitoPrimario
                .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito + " - " + c.NombreAntiguo }))
                .Union(db.CircuitoSubtransmision
                .Select(c => new SelectListItem { Value = c.CodigoCircuito, Text = c.CodigoCircuito + " - " + c.NombreCircuito }));
            string IdInstalacion = esquemaProteccion.Subestacion;
            ViewBag.Subestacion = new SelectList(subestaciones, "Value", "Text", IdInstalacion);
            ViewBag.Linea = new SelectList(lineas, "Value", "Text", IdInstalacion);

            ViewBag.Tipo_Equipo_Primario = esquemaProteccion.Tipo_Equipo_Primario;
            ViewBag.Elemento_Electrico = esquemaProteccion.Elemento_Electrico;

            string IdInterruptor = (
                from ed in db.Esquema_Desconectivo
                where ed.esquema.Equals(esquemaProteccion.id_Esquema)
                select ed.desconectivo
                ).FirstOrDefault();
            ViewBag.Interruptores = new SelectList(db.Desconectivos.ToList(), "Codigo", "Codigo",IdInterruptor);

            string IdTC = (
                from e in db.Esquema_TC
                where e.esquema.Equals(esquemaProteccion.id_Esquema)
                select e.TC
                ).FirstOrDefault();
            ViewBag.TC = new SelectList(db.TransformadoresCorriente.ToList(), "Nro_Serie", "Nro_Serie", IdTC);

            string IdTP = (
                from e in db.Esquema_TP
                where e.esquema.Equals(esquemaProteccion.id_Esquema)
                select e.TP
                ).FirstOrDefault();
            ViewBag.TP = new SelectList(db.TransformadoresPotencial.ToList(), "Nro_Serie", "Nro_Serie", IdTP);

            string IdRele = (
                from e in db.Esquema_Rele
                where e.esquema.Equals(esquemaProteccion.id_Esquema)
                select e.rele
                ).FirstOrDefault();
            ViewBag.Relevadores = new SelectList(db.Relevadores.ToList(), "Nro_Serie", "Nro_Serie", IdRele);

            ViewBag.RelevadorFunc = new SelectList(db.Relevadores.ToList(), "Nro_Serie", "Nro_Serie");           
        }
        
        
    }
}