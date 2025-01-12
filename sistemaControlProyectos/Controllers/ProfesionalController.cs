﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistemaControlProyectos.Models;
namespace sistemaControlProyectos.Controllers
{
    public class ProfesionalController : Controller
    {
        // GET: Profesional
        private LoginController p = new LoginController();

        public ActionResult Profesional()
        {
            return p.MenuSession(View());
        }
        public JsonResult Listar()
        {
            //List<SP_C_PROFESIONALRESPONSABLE_Result> listarActividad = new List<SP_C_PROFESIONALRESPONSABLE_Result>();

            //using (DBControlProyectoEntities db = new DBControlProyectoEntities())
            //{

            //listarActividad = db.SP_C_PROFESIONALRESPONSABLE().ToList();
            //listarActividad = (from p in db.tblActividad select p).ToList();
            //}
            //return Json(new { data = listarActividad }, JsonRequestBehavior.AllowGet);

          
            List<SP_C_PROFESIONAL_Result> listar =ProfesionalModelo.instancia.ListarProfesional();
            return Json(new { data = listar }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Obtener(string dni)
        {
            tblProfesional oPersona = new tblProfesional();

            using (DBControlProyectoEntities db = new DBControlProyectoEntities())
            {

                oPersona = (from p in db.tblProfesional.Where(x => x.DNI == dni)
                            select p).FirstOrDefault();
            }

            return Json(oPersona, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(tblProfesional objeto)
        {
            bool respuesta = true;

            if (objeto.IDProfesional == 0)
            {
                
                respuesta = ProfesionalModelo.instancia.RegistrarProfesional(objeto);
            }
            else
            {
                respuesta = ProfesionalModelo.instancia.ModificarProfesional(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Eliminar(int idprofesional)
        {
            bool respuesta = true;
            respuesta = ProfesionalModelo.instancia.EliminarProfesional(idprofesional);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}