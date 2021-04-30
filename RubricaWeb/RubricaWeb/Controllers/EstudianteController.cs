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
    public class EstudianteController : Controller
    {
        // GET: Estudiante
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CargaEstudiante()
        {
            List<VM_Curso> listaCursos = AD_ViewModel.ListaDeCursos();
            List<SelectListItem> items = listaCursos.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NombreCurso,
                    Value = i.IdCurso.ToString(),

                    Selected = false
                };
            });
            ViewBag.items = items;
            return View();


        }

        [HttpPost]
        public ActionResult CargaEstudiante(Estudiante model)
        {
            if (ModelState.IsValid)
            {
                bool resultado = AD_Estudiante.AgregarEstudiante(model);

                if (resultado)
                {

                    return RedirectToAction("ListadoEstudiantes", "Estudiante");
                }
                else
                {
                    return View(model);

                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult ListadoEstudiantes()
        {
            
            List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantes();
            return View(lista);
        }



    }

   


}

