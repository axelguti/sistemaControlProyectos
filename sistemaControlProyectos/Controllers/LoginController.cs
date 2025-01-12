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
            if (SesionUsuario.nombre.Length > 6 )
            {
                SesionUsuario.nombre = SesionUsuario.nombre.Substring(0, 6);
            }
            if (SesionUsuario.apellidos.Length > 9)
            {
                SesionUsuario.apellidos = SesionUsuario.apellidos.Substring(0,9);
            }
            viewResult.ViewBag.NombreUsuario = SesionUsuario.nombre + " " + SesionUsuario.apellidos.ToUpper();
            viewResult.ViewBag.IDCargo = SesionUsuario.IDCargo;
            viewResult.ViewBag.Cargo = SesionUsuario.nomCargo;
            SP_C_PROYECTO_Result proyecto = ProyectosModelo.Instancia.ListarProyecto().Where(p => p.IDProyecto == SesionUsuario.IDProyectoActual).FirstOrDefault();
            viewResult.ViewBag.IDUsuario = SesionUsuario.IDProfesional;
            viewResult.ViewBag.proyecto = proyecto.titProyecto;
            viewResult.ViewBag.IDProyectoActual = proyecto.IDProyecto;
            SP_C_PROFESIONALIMAGENUSUARIO_Result usuario = UsuarioModelo.instancia.ListarUsuarioImagen(SesionUsuario.DNI).FirstOrDefault();
            if (usuario.usrImagen == null)
            {
                viewResult.ViewBag.ImagenUsuario = "../imagenes/user.jpg";
            }
            else
            {
                viewResult.ViewBag.ImagenUsuario = usuario.usrImagen;
            }

            return viewResult;
        }

    }
}