using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RubricaWeb.Models;
using RubricaWeb.AccesoDatos;
using RubricaWeb.ViewModels;

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

            int idMateria = modelo[1].IdMateria;
            int idNroTema = modelo[1].IdNroTema;
            int idTema = modelo[1].IdTema;

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




        public ActionResult LibretaMateria(int idMateria)
        {
            List<VM_LibretaNotasMateria> lista = AD_Rubrica.ListadoNotasPorMateria(idMateria);

            return View(lista);



            
        }

    }
}