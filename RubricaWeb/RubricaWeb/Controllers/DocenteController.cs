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
    public class DocenteController : Controller
    {
        // GET: Docente
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CargaDocente()
        {
            List<VM_Rol> listaCursos = AD_ViewModel.ListaDeRoles();
            List<SelectListItem> items = listaCursos.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.Rol,
                    Value = i.IdRol.ToString(),

                    Selected = false
                };
            });
            ViewBag.items = items;
            return View();

        }











    }
}