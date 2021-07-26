using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RubricaWeb.AccesoDatos;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;

namespace RubricaWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Home(int idDocente, int idRol )
        {

            System.Web.HttpContext.Current.Session.Add("idDocente", idDocente);
            System.Web.HttpContext.Current.Session.Add("idRol", idRol);
            //Session.Clear();


            if (idRol == 1)
            {
                return RedirectToAction("PanelDocente", "Docente", new { idDocente});
            }
            else if (idRol== 2)
            {              
                VM_Docente docente = AD_Docente.ObtenerDocenteXId(idDocente);
                ViewBag.Nombre = docente.NombreDocente;
                return View();
            }
            
           
            return View();
        }




        public ActionResult PreguntasFrecuentes()
        {


            return View();
        }

    }
}