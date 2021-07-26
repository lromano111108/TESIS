using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Rotativa;
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
            string mensaje = "LISTADO COMPLETO DE ESTUDIANTES";

            ViewBag.Boton = esCompleto;
            ViewBag.idCurso = idCurso;
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

            if (esCompleto)
            {
                List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantes();
                ViewBag.listaEstudiantes = lista;

                ViewBag.Mensaje = mensaje;

                return View();
            }
            else
            {
                List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantesXId(idCurso);
                
                ViewBag.listaEstudiantes = lista;
                VM_Curso curso = AD_ViewModel.ObtenerCursoXId(idCurso);
                if (lista.Count==0)
                {
                    mensaje = "No Hay Estudiantes para mostrar de " + curso.NombreCurso.ToString();


                }

                else
                {
                    mensaje = lista[0].Curso;

                }
                ViewBag.Mensaje = mensaje;
                return View();
            }
           
        }



        public ActionResult MostrarDatosEstudiante(int idEstudiante)
        {
            VM_Estudiante estudiante = AD_Estudiante.ObtenerEstudianteXId(idEstudiante);
            List<VM_Materia> lista = AD_Estudiante.ListaMateriaPorEstudiante(idEstudiante);
            ViewBag.listaMaterias = lista;

            string grafico = AD_Reportes.GraficoRendimientoEstudiante(estudiante);
            List<VM_ReporteEstudiante> temasAdeudados = AD_Reportes.ResumenMateriasAdeudadas(estudiante);
            ViewBag.TemasAdeudados = temasAdeudados;

            int cantidadTemas = temasAdeudados.Count();


            ViewBag.Grafico = grafico;

            //ViewBag.estudiante = resultado;

            return View(estudiante);

        }

        public ActionResult GraficosEstudiante(int idEstudiante)
        {
            VM_Estudiante estudiante = AD_Estudiante.ObtenerEstudianteXId(idEstudiante);

            string graficoPromedio = AD_Reportes.GraficoPromediosMaterias(idEstudiante);
            ViewBag.GraficoPromedio = graficoPromedio;

            string graficoTortaEstudiante = AD_Reportes.GraficoTortaAprobadoEstudiante(idEstudiante);
            ViewBag.GraficoTortaEstudiante = graficoTortaEstudiante;


            string grafico = AD_Reportes.GraficoRendimientoEstudiante(estudiante);

            ViewBag.GraficoRendimiento = grafico;

            return View();
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

        public ActionResult ImpresionListado(int idCurso, bool esCompleto)
        {
            string mensaje = "LISTADO COMPLETO DE ESTUDIANTES";

            ViewBag.Boton = esCompleto;
            ViewBag.idCurso = idCurso;
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

            if (esCompleto)
            {
                List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantes();
                ViewBag.listaEstudiantes = lista;

                ViewBag.Mensaje = mensaje;

                return View(lista);
            }
            else
            {
                List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantesXId(idCurso);

                ViewBag.listaEstudiantes = lista;
                VM_Curso curso = AD_ViewModel.ObtenerCursoXId(idCurso);
                if (lista.Count == 0)
                {
                    mensaje = "No Hay Estudiantes para mostrar de " + curso.NombreCurso.ToString();


                }

                else
                {
                    mensaje = lista[0].Curso;

                }
                ViewBag.Mensaje = mensaje;
                ViewBag.Curso = mensaje;
                return View();
            }           


        }

        public ActionResult Print(int idCurso, bool esCompleto)
        {
            VM_Curso curso = AD_ViewModel.ObtenerCursoXId(idCurso);
         
            string nombreCurso= Regex.Replace(curso.NombreCurso, @"[^\w\s.!@$%^&*()\-\/]+", "");
            return new ActionAsPdf("ImpresionListado", new { idCurso, esCompleto}) { FileName = "Listado de Estudiantes de " + nombreCurso + ".pdf" };

        }






















    }





}

