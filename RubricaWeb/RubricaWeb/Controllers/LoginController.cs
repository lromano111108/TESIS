using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RubricaWeb.Models;
using RubricaWeb.AccesoDatos;

namespace RubricaWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            ViewBag.Mensaje = "Ingresar DNI y Contraseña";
            return View();
        }



        [HttpPost]
        public ActionResult Login(Login modelo)
        {
            Docente controlar = AD_Login.Ingresar(modelo);

            if (controlar.IdDocente != 0 || controlar.IdRol != 0)
            {
               
                modelo.IdUsuario = controlar.IdDocente;
                //AD_Login.AgregarPassword(modelo);

                return RedirectToAction("Home", "Home", new { controlar.IdDocente, controlar.IdRol });
            }

            ViewBag.Mensaje = "Usuario o Contraseña Incorrectos";
            return View(modelo);
        }


        public ActionResult Registrar()
        {
            ViewBag.mensaje = "";
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Login modelo)
        {
            Docente controlar = AD_Login.comprobarQueHayDocente(modelo);

            if (controlar.IdDocente != 0 & controlar.IdRol != 0)
            {
                if (modelo.Password == modelo.Password2)
                {
                    modelo.IdUsuario = controlar.IdDocente;
                    AD_Login.AgregarPassword(modelo);

                    return RedirectToAction("Home", "Home", new { controlar.IdDocente, controlar.IdRol });
                }
                else
                {
                    ViewBag.mensaje = "Debe Ingresar 2 Contraseñas Iguales";
                    return View(modelo);
                }
            }
            else
            {
                ViewBag.mensaje = "El usuario es incorrecto";
                return View(modelo);
            }
        }







    }
}
