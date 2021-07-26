using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RubricaWeb.Models;
using RubricaWeb.AccesoDatos;
using RubricaWeb.ViewModels;
using System.Data;
using Rotativa;

namespace RubricaWeb.Controllers
{
    public class RubricaController : Controller
    {
        // GET: Rubrica
        public ActionResult Rubrica(int idMateria, int idNroTema, int idTema)
        {
            VM_Materia nuevaMateria = AD_Materia.datosMateria(idMateria);

            List<VM_Rubrica> lista = AD_Rubrica.ListadoEstudiantesParaRubrica(idTema);
            ViewBag.listaEstudiantes = lista;


            VM_Tema tema = AD_Rubrica.datosTemaACargar(idMateria, idNroTema);
            ViewBag.datosTema = tema;

            string grafico = AD_Reportes.graficoReporte1(idMateria);


            //DataTable graficoConTabla = AD_Reportes.graficoReporteConTabla(idMateria);

            //ViewBag.GraficoConTabla = graficoConTabla;

            ViewBag.Grafico = grafico;
            ViewBag.NombreMateria = nuevaMateria.materia;
            ViewBag.NombreCurso = nuevaMateria.curso;
            List<VM_ComboValorCriterios> valorCriterio = AD_ViewModel.ComboValorCriterios();
            List<SelectListItem> combo = valorCriterio.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.ValorCriterio.ToString(),
                    Value = i.IdValor.ToString(),

                    Selected = false
                };
            });

            ViewBag.comboCriterios = combo;

            return View(lista);
        }

        [HttpPost]
        public ActionResult Rubrica(List<VM_Rubrica> modelo)
        {
            int CantCriterios = AD_Rubrica.ContarCriteriosTemas(modelo[0].IdTema);

            foreach (var valoracion in modelo)
            {
                double nota = AD_Rubrica.CalcularNota(valoracion, CantCriterios);
                valoracion.Nota = nota;
                AD_Rubrica.GuardarValoracionTema(valoracion);
               
            }

            int idMateria = modelo[0].IdMateria;
            int idNroTema = modelo[0].IdNroTema;
            int idTema = modelo[0].IdTema;

            return RedirectToAction("Rubrica", "Rubrica", new { idMateria, idNroTema, idTema });
            //return View(idMateria,idNroTema,idTema);
        }



        public ActionResult CargaTemas(int idCurso, int idDocente, int idMateria)
        {
           
            VM_Materia nuevaMateria = AD_Materia.datosMateria(idMateria);

            List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantesCursoXID(nuevaMateria.idCurso);
            ViewBag.listaEstudiantes = lista;

            ViewBag.NombreMateria = nuevaMateria.materia;
            ViewBag.NombreCurso = nuevaMateria.curso;
            ViewBag.IdDocente = idDocente;


            List<VM_Tema> valorCriterio = AD_Rubrica.comboNumeroTema(idMateria);
            List<SelectListItem> combo = valorCriterio.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NumeroDeTema.ToString(),
                    Value = i.IdTema.ToString(),

                    Selected = false
                };
            });

            ViewBag.comboCriterios = combo;

            return View();
        }



        [HttpPost]
        public ActionResult CargaTemas(VM_Tema model)
        {                  
            bool resultado = AD_Rubrica.AgregarNuevoTema(model);

            if (resultado)
            {

                VM_Docente docente = AD_Docente.ObtenerDocenteXMateria(model.IdMateria);

                int idTema = AD_Rubrica.ultimoIdTema();
                int CantCriterios = AD_Rubrica.ContarCriteriosTemas(idTema);


                List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantesCursoXID(model.Idcurso);
                foreach(var estudiante in lista){

                    AD_Rubrica.AgregarTemaPorEstudiante(estudiante.IdEstudiante, idTema,CantCriterios);

                }
                return RedirectToAction("ListadoTemasCargados", "Rubrica", new { model.IdMateria });
            }
            else
            {
                return View(model);

            }
        }

        public ActionResult EditarCargaTemas(int IdTema, int idDocente)
        {
            VM_Tema Tema = AD_Rubrica.ObtenerTemaRubrica(IdTema);

            VM_Materia materia = AD_Materia.datosMateria(Tema.IdMateria);

            ViewBag.NombreMateria = materia.materia;
            ViewBag.NombreCurso = materia.curso;
            ViewBag.IdDocente = idDocente;


            List<VM_Tema> valorCriterio = AD_Rubrica.comboNumeroTema(Tema.IdMateria);
            List<SelectListItem> combo = valorCriterio.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NumeroDeTema.ToString(),
                    Value = i.IdTema.ToString(),

                    Selected = false
                };
            });

            ViewBag.comboCriterios = combo;

            return View(Tema);
        }

        [HttpPost]
        public ActionResult EditarCargaTemas(VM_Tema model)
        {
            bool resultado = AD_Rubrica.EditarTema(model);

            if (resultado)
            {

                VM_Docente docente = AD_Docente.ObtenerDocenteXMateria(model.IdMateria);

                int idTema = model.IdTema;
                int CantCriteriosActualizado = AD_Rubrica.ContarCriteriosTemas(idTema);


                List<VM_Estudiante> lista = AD_Estudiante.ListadoEstudiantesCursoXID(model.Idcurso);
          
                foreach (var estudiante in lista)
                {
                    
                    AD_Rubrica.AgregarTemaPorEstudiante(estudiante.IdEstudiante, idTema, CantCriteriosActualizado);


                }



                return RedirectToAction("ListadoTemasCargados", "Rubrica", new { model.IdMateria });
            }
            else
            {
                return View(model);

            }
        }
        public ActionResult ListadoTemasCargados(int idMateria)
        {
            List<VM_ListadoRubrica> lista = AD_Rubrica.listaTemasMateria(idMateria);
            ViewBag.listaTemas = lista;

            VM_DocenteXMateria datosDocente = AD_Rubrica.ObtenerIdMateriaIdDocente(idMateria);

            ViewBag.datosDocente = datosDocente;

            VM_Materia nuevaMateria = AD_Materia.datosMateria(idMateria);
            ViewBag.NombreMateria = nuevaMateria.materia;
            ViewBag.NombreCurso = nuevaMateria.curso;
            ViewBag.idCurso = nuevaMateria.idCurso;

            return View();
        }


        public ActionResult cargarRubrica(VM_Rubrica[] lista)
        {
            return RedirectToAction("Login", "Login");
        }




        public ActionResult LibretaMateria(int idMateria, int pagina)
        {
            List<VM_LibretaNotasMateria> lista = AD_Rubrica.ListadoNotasPorMateria(idMateria);
            VM_Materia materia = AD_Materia.datosMateria(idMateria);
            ViewBag.Materia = materia;

           
            string tabla = AD_Reportes.graficoReporte1(idMateria);
            ViewBag.Grafico = tabla;
           

            string graficoBarra= AD_Reportes.GraficoCantidadTemasAdeudadosEstudiante(idMateria);
            ViewBag.GraficoBarra = graficoBarra;

            //ViewBag.Numero2 = numero2;
            int CantidadTemasCargados = AD_Rubrica.ContarTemasMateria(idMateria);
            double promedio = 0;

            foreach(var estudiante in lista)
            {

                promedio = AD_Rubrica.CalcularPromedio(estudiante, CantidadTemasCargados);
                estudiante.Promedio = promedio;

               
            }

            if (pagina == 2)
            {
                return RedirectToAction("LibretaMateriaP2", "Rubrica", new { idMateria });
                
            }
            return View(lista);
         
        }


        public ActionResult LibretaCualificaciones(int idMateria, int pagina)
        {
            List<VM_LibretaNotasMateria> lista = AD_Rubrica.ListadoNotasPorMateria(idMateria);
            VM_Materia materia = AD_Materia.datosMateria(idMateria);
            ViewBag.Materia = materia;
            int CantidadTemasCargados = AD_Rubrica.ContarTemasMateria(idMateria);
            double promedio = 0;

            foreach (var estudiante in lista)
            {

                promedio = AD_Rubrica.CalcularPromedio(estudiante, CantidadTemasCargados);
                estudiante.Promedio = promedio;


            }


            if (pagina == 2)
            {
                return RedirectToAction("LibretaCualificacionesP2", "Rubrica", new { idMateria });

            }
            return View(lista);

          

        }


        public ActionResult LibretaCualificacionesP2(int idMateria)
        {
            List<VM_LibretaNotasMateria> lista = AD_Rubrica.ListadoNotasPorMateria(idMateria);
            VM_Materia materia = AD_Materia.datosMateria(idMateria);
            ViewBag.Materia = materia;
            int CantidadTemasCargados = AD_Rubrica.ContarTemasMateria(idMateria);
            double promedio = 0;

            foreach (var estudiante in lista)
            {

                promedio = AD_Rubrica.CalcularPromedio(estudiante, CantidadTemasCargados);
                estudiante.Promedio = promedio;


            }

            return View(lista);

        }

        public ActionResult LibretaEstudiante()
        {
            return View();
        }



        public ActionResult LibretaMateriaP2(int idMateria)
        {
            List<VM_LibretaNotasMateria> lista = AD_Rubrica.ListadoNotasPorMateria(idMateria);
            VM_Materia materia = AD_Materia.datosMateria(idMateria);
            ViewBag.Materia = materia;
            int CantidadTemasCargados = AD_Rubrica.ContarTemasMateria(idMateria);
            double promedio = 0;

            foreach (var estudiante in lista)
            {

                promedio = AD_Rubrica.CalcularPromedio(estudiante, CantidadTemasCargados);
                estudiante.Promedio = promedio;


            }



            return View(lista);

        }


         public ActionResult Print(int idMAteria, int pagina)
        {
            

            return new ActionAsPdf("VistaReportePDF", new {idMAteria,pagina }) {FileName="Libreta Notas.pdf"};

        }


        public ActionResult VistaReportePDF(int idMateria, int pagina)
        {
            List<VM_LibretaNotasMateria> lista = AD_Rubrica.ListadoNotasPorMateria(idMateria);
            VM_Materia materia = AD_Materia.datosMateria(idMateria);
            ViewBag.Materia = materia;


            string tabla = AD_Reportes.graficoReporte1(idMateria);
            ViewBag.Grafico = tabla;


            //ViewBag.Numero2 = numero2;
            int CantidadTemasCargados = AD_Rubrica.ContarTemasMateria(idMateria);
            double promedio = 0;

            foreach (var estudiante in lista)
            {

                promedio = AD_Rubrica.CalcularPromedio(estudiante, CantidadTemasCargados);
                estudiante.Promedio = promedio;


            }

            if (pagina == 2)
            {
                return RedirectToAction("LibretaMateriaP2", "Rubrica", new { idMateria });

            }
            return View(lista);

        }
    }
}