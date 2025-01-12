﻿using sistemaControlProyectos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistemaControlProyectos.Controllers
{
    [Route("/api/[controller]")]
    public class AreaController : Controller
    {
        // GET: Area
        private static SP_C_PROFESIONAL_Result SesionUsuario;
        private LoginController p = new LoginController();
        public ActionResult Area()
        {
            return p.MenuSession(View());
        }

        public ActionResult Organigrama()
        {
            return p.MenuSession(View());
        }

        public JsonResult Listar()
        {
            // List<tblArea> listarActividad = new List<tblArea>();

            // using (DBControlProyectoEntities db = new DBControlProyectoEntities())
            // {

            //   listarActividad = db.SP_C_AREA().ToList();
            //listarActividad = (from p in db.tblArea select p).ToList();
            //}
            //return Json(new { data = listarActividad }, JsonRequestBehavior.AllowGet);
            SesionUsuario = (SP_C_PROFESIONAL_Result)Session["profesional"];
            List<SP_C_AREA_Result> listar = AreaModelo.Instancia.ListarArea().Where(a => a.IDProyecto == SesionUsuario.IDProyectoActual).ToList();
            return Json(new { data = listar }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarAreaPadre()
        {
            SesionUsuario = (SP_C_PROFESIONAL_Result)Session["profesional"];
            List<SP_C_AREAPADRE_Result> listar = AreaModelo.Instancia.ListarAreaPadre().Where(a => a.IDProyecto == SesionUsuario.IDProyectoActual).ToList();
            return Json(new { data = listar }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Obtener(int idArea)
        {
            tblArea ObtenerArea = new tblArea();

            using (DBControlProyectoEntities db = new DBControlProyectoEntities())
            {

                ObtenerArea = (from p in db.tblArea.Where(x => x.IDArea == idArea)
                                    select p).FirstOrDefault();
            }

            return Json(ObtenerArea, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(tblArea objeto)
        {
            SesionUsuario = (SP_C_PROFESIONAL_Result)Session["profesional"];
            bool respuesta = false;

            if (objeto.IDArea == 0)
            {
                respuesta = Models.AreaModelo.Instancia.RegistrarArea(objeto, (int)SesionUsuario.IDProyectoActual);
            }
            else
            {
                respuesta = Models.AreaModelo.Instancia.ModificarArea(objeto, (int)SesionUsuario.IDProyectoActual);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Eliminar(int IDArea)
        {
            bool respuesta = true;
            respuesta = Models.AreaModelo.Instancia.EliminarArea(IDArea);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}