﻿using System;
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
    public class TransformadoresCorrienteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TransformadoresCorriente
        public ActionResult Index()
        {
            return View(db.TransformadoresCorriente.Include(c=> c.VoltajeSistema)/*.Include(c => c.Plantilla).Include(c => c.Plantilla.Fabricante)*/.ToList());
        }        
    }
}
