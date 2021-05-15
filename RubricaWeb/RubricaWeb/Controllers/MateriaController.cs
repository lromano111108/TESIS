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
    public class MateriaController : Controller
    {
        // GET: Materia
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult CargarMateria()
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
        public ActionResult CargarMateria(Materia model)
        {
            if (ModelState.IsValid)
            {
                bool resultado = AD_Materia.AgregarMateria(model);

                if (resultado)
                {

                    return RedirectToAction("ListadoMaterias", "Materia");
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


        public ActionResult ListadoMaterias()
        {

            List<VM_Materia> lista = AD_Materia.ListadoMaterias ();
            return View(lista);
        }


        
        public ActionResult EliminarMateria(int idMateria)
        {
           AD_Materia.EliminarMateria(idMateria);

           

            return RedirectToAction("ListadoMaterias", "Materia");

        }



        public ActionResult EditarMateria (VM_Materia modelo, string parametro)
        {

            return RedirectToAction("ListadoMaterias", "Materia");

        }


    }
}