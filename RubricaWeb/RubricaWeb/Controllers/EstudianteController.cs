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
            bool esCompleto = false;
            if (ModelState.IsValid)
            {
                bool resultado = AD_Estudiante.AgregarEstudiante(model);

                if (resultado)
                {

                    return RedirectToAction("ListadoEstudiantes", "Estudiante", new {idCurso = model.IdCurso , esCompleto});
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

        public ActionResult ListadoEstudiantes(int idCurso, bool esCompleto)
        {

            if (esCompleto)
            {
                List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantes();
                ViewBag.listaEstudiantes = lista;
                return View();
            }
            else
            {
                List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantesXId(idCurso);
                ViewBag.listaEstudiantes = lista;
                return View();
            }
           
        }



        public ActionResult MostrarDatosEstudiante(int idEstudiante)
        {
            VM_Estudiante resultado = AD_Estudiante.ObtenerEstudianteXId(idEstudiante);
            List<VM_Materia> lista = AD_Estudiante.ListaMateriaPorEstudiante(idEstudiante);
            ViewBag.listaMaterias = lista;
            //ViewBag.estudiante = resultado;

            return View(resultado);

        }

        public ActionResult EditarEstudiante(int IdEstudiante)
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


            Estudiante resultado = AD_Estudiante.EstudianteParaEditar(IdEstudiante);
            return View(resultado);

        }



        [HttpPost]
        public ActionResult EditarEstudiante(Estudiante model)
        {

            if (ModelState.IsValid)
            {
                bool resultado = AD_Estudiante.ActualizarDatosEstudiante(model);
                if (resultado)
                {

                    return RedirectToAction("MostrarDatosEstudiante", "Estudiante", new { idEstudiante = model.IdEstudiante });
                }
                else
                {
                    return View(model);
                }
            }
            return View();

        }




















    }





}

