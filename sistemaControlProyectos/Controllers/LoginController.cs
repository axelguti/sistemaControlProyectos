﻿using sistemaControlProyectos.Models;
using System.Linq;
using System.Web.Mvc;

namespace sistemaControlProyectos.Controllers
{
    public class LoginController : Controller
    {
        private static SP_C_PROFESIONAL_Result SesionUsuario;

        // GET: Login
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {

            SP_C_PROFESIONAL_Result listar = ProfesionalModelo.instancia.ListarProfesional().Where(u => u.DNI == username && u.contraseña == password).FirstOrDefault();
            if (listar == null)
            {
                ViewBag.Error = "Usuario o contraseña Invalida";
                return View();
            }
            Session["usuario"] = listar;
            return RedirectToAction("ProyectosInicio", "Proyectos");
        }

        internal ActionResult MenuSession(ViewResult viewResult)
        {
            SesionUsuario = (SP_C_PROFESIONAL_Result)System.Web.HttpContext.Current.Session["profesional"];
            string CortarNombre = SesionUsuario.nombre.Substring(0,3);
            viewResult.ViewBag.NombreUsuario = CortarNombre + " " + SesionUsuario.apellidos;
            viewResult.ViewBag.IDCargo = SesionUsuario.IDCargo;
            viewResult.ViewBag.Cargo = SesionUsuario.nomCargo;
            SP_C_PROYECTO_Result proyecto = ProyectosModelo.Instancia.ListarProyecto().Where(p => p.IDProyecto == SesionUsuario.IDProyectoActual).FirstOrDefault();
            viewResult.ViewBag.IDUsuario = SesionUsuario.IDProfesional;
            viewResult.ViewBag.proyecto = proyecto.titProyecto;
            viewResult.ViewBag.IDProyectoActual = proyecto.IDProyecto;

            return viewResult;
        }

        public SP_C_PROFESIONAL_Result ListarSession()
        {
            SesionUsuario = (SP_C_PROFESIONAL_Result)Session["profesional"];
            ViewBag.NombreUsuario = SesionUsuario.nombre + " " + SesionUsuario.apellidos;
            ViewBag.IDCargo = SesionUsuario.IDCargo;
            ViewBag.Cargo = SesionUsuario.nomCargo;
            SP_C_PROYECTO_Result proyecto = ProyectosModelo.Instancia.ListarProyecto().Where(p => p.IDProyecto == SesionUsuario.IDProyectoActual).FirstOrDefault();
            ViewBag.IDUsuario = SesionUsuario.IDProfesional;
            ViewBag.proyecto = proyecto.titProyecto;
            ViewBag.IDProyectoActual = proyecto.IDProyecto;

            return SesionUsuario;
        }
    }
}