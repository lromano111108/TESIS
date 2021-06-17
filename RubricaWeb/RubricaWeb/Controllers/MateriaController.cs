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


        
        public ActionResult EliminarMateria(int idMateria, int idDocente)
        {
           AD_Materia.EliminarMateria(idMateria, idDocente);

           

            return RedirectToAction("ListadoMaterias", "Materia");

        }



        public ActionResult EditarMateria (VM_Materia modelo, string parametro)
        {

            return RedirectToAction("ListadoMaterias", "Materia");

        }


        public ActionResult AsignarDocente(int idMateria)
        {
            List<Docente> listaDocentes = AD_Docente.ComboDocentes();
            List<SelectListItem> itemsDocentes = listaDocentes.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NombreDocente,
                    Value = i.IdDocente.ToString(),

                    Selected = false
                };
            });
            ViewBag.itemsDocentes = itemsDocentes;

            List<Materia> listaMaterias = AD_Materia.ComboMateria();
            List<SelectListItem> itemsMaterias = listaMaterias.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NombreMateria,
                    Value = i.IdMateria.ToString(),

                    Selected = false
                };
            });
            ViewBag.itemsMaterias = itemsMaterias;



            List<VM_Curso> listaCursos = AD_ViewModel.ListaDeCursos();
            List<SelectListItem> itemsCursos = listaCursos.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NombreCurso,
                    Value = i.IdCurso.ToString(),

                    Selected = false
                };
            });
            ViewBag.itemsCursos = itemsCursos;
            return RedirectToAction("AsignarMateriasDocentes", "Docente", new { idMateria});

        }

    }
}