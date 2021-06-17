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
            ViewBag.error = "";
            return View();
        }



        [HttpPost]
        public ActionResult Login(Login modelo)
        {
            Docente controlar = AD_Login.Ingresar(modelo);

            if (controlar.IdDocente != 0 || controlar.IdRol != 0)
            {
                modelo.IdUsuario = controlar.IdDocente;
                AD_Login.AgregarPassword(modelo);

                return RedirectToAction("PanelDocente", "Docente", new { controlar.IdDocente, controlar.IdRol });
            }

            ViewBag.error = "Usuario o Contraseña Incorrectos";
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
            if (modelo.Password == modelo.Password2)
            {

                Docente controlar = AD_Login.comprobarQueHayDocente(modelo);

                if (controlar.IdDocente != 0 || controlar.IdRol != 0)
                {
                    modelo.IdUsuario = controlar.IdDocente;
                    AD_Login.AgregarPassword(modelo);

                    return RedirectToAction("MostrarDatosDocentes", "Docente", new { controlar.IdDocente, controlar.IdRol });
                }

            }

            else
            {
                ViewBag.mensaje = "Debe Ingresar 2 Contraseñas Iguales";
                return View(modelo);
               

            }



            return View();
        }




    }
}