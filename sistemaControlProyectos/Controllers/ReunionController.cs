﻿using sistemaControlProyectos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;


namespace sistemaControlProyectos.Controllers
{
    public class ReunionController : Controller
    {
        // GET: Reuniones

        public ActionResult Reunion()
        {
            return View();
        }

        public JsonResult Listar()
        {
            //List<SP_C_Reunion_Result> listarReunion = new List<SP_C_Reunion_Result>();

            //using (DBControlProyectoEntities db = new DBControlProyectoEntities())
            //{

            //listarReunion = db.SP_C_Reunion().ToList();
            //listarReunion = (from p in db.tblReunion select p).ToList();
            //}
            //return Json(new { data = listarReunion }, JsonRequestBehavior.AllowGet);

            List<SP_C_REUNION_Result> lista = Models.ReunionModelo.Instancia.ListarReunion();
            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Obtener(int IDReunion)
        {
            tblReunion ObtenerReunion = new tblReunion();

            using (DBControlProyectoEntities db = new DBControlProyectoEntities())
            {

                ObtenerReunion = (from p in db.tblReunion.Where(x => x.IDReunion == IDReunion)
                                    select p).FirstOrDefault();
            }

            return Json(ObtenerReunion, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(tblReunion objetoReunion)
        {
            bool respuesta = false;

            if (objetoReunion.IDReunion == 0)
            {
                respuesta = Models.ReunionModelo.Instancia.RegistrarReunion(objetoReunion);
            }
            else
            {
                respuesta = Models.ReunionModelo.Instancia.ModificarReunion(objetoReunion);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(int IDReunion)
        {
            bool respuesta = true;
            respuesta = Models.ReunionModelo.Instancia.EliminarReunion(IDReunion);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }



    }
    
}
