using Rotativa;
using RubricaWeb.AccesoDatos;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubricaWeb.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LibretaEstudiante(int idEstudiante)
        {
            Estudiante nuevoEstudiante = AD_Estudiante.EstudianteParaEditar(idEstudiante);

            
            List<VM_LibretaEstudiante> libreta = AD_Reportes.ObtenerNotasLibreta(nuevoEstudiante);
            ViewBag.Estudiante = nuevoEstudiante;

            VM_Estudiante estudiante = AD_Estudiante.ObtenerEstudianteXId(idEstudiante);
            List<VM_ReporteEstudiante> temasAdeudados = AD_Reportes.ResumenMateriasAdeudadas(estudiante);
            ViewBag.TemasAdeudados = temasAdeudados;
            

            foreach (var materia in libreta)
            {
                int CantidadTemasCargados = AD_Rubrica.ContarTemasMateria(materia.idMateria);
                double promedio = 0;

                promedio = AD_Reportes.CalcularPromedioLibreta(materia, CantidadTemasCargados);
                materia.Promedio = promedio;

            }

            return View(libreta);
        }




        //////IMPRESION LIBRETA ESTUDIANTE /////////
        
        public ActionResult Print(int idEstudiante)
        {
            Estudiante nuevoEstudiante = AD_Estudiante.EstudianteParaEditar(idEstudiante);

            return new ActionAsPdf("VistaImpresionLibreta", new { idEstudiante }) { FileName = "Libreta Estudiante"+"Dni"+nuevoEstudiante.IdCurso + nuevoEstudiante.DniEstudiante+nuevoEstudiante.ApellidoEstudiante + nuevoEstudiante.NombreEstudiante +".pdf" };

        }


        public ActionResult VistaImpresionLibreta(int idEstudiante)
        {
            Estudiante nuevoEstudiante = AD_Estudiante.EstudianteParaEditar(idEstudiante);
            List<VM_LibretaEstudiante> libreta = AD_Reportes.ObtenerNotasLibreta(nuevoEstudiante);
            ViewBag.Estudiante = nuevoEstudiante;


            foreach (var materia in libreta)
            {
                int CantidadTemasCargados = AD_Rubrica.ContarTemasMateria(materia.idMateria);
                double promedio = 0;

                promedio = AD_Reportes.CalcularPromedioLibreta(materia, CantidadTemasCargados);
                materia.Promedio = promedio;

            }


            VM_Estudiante estudiante = AD_Estudiante.ObtenerEstudianteXId(idEstudiante);
            List<VM_ReporteEstudiante> temasAdeudados = AD_Reportes.ResumenMateriasAdeudadas(estudiante);
            ViewBag.TemasAdeudados = temasAdeudados;

            //int cantidad = temasAdeudados.Count();

            return View(libreta);

        }
    }
}