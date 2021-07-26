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


        public ActionResult ModificarMateria(int idMateria)
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

            Materia materia = AD_Materia.MateriaXId(idMateria);
            return View(materia);

           
        } 
        
        
        [HttpPost]
        public ActionResult ModificarMateria(Materia model)
        {
            if (ModelState.IsValid)
            {
                bool resultado = AD_Materia.EditarMateria(model);


            }
            return RedirectToAction("ListadoMaterias", "Materia", new { idCurso = model.IdCurso, esCompleto = false });

        }


        [HttpPost]
        public ActionResult CargarMateria(Materia model)
        {
            if (ModelState.IsValid)
            {
                bool resultado = AD_Materia.AgregarMateria(model);

                if (resultado)
                {
                    int idCurso = model.IdCurso;
                    List<VM_Estudiante> listaCurso = AD_Estudiante.ListadoEstudiantesCursoXID(idCurso);

                    int idMateria = AD_Materia.ultimoIdMateria();

                    foreach (var estudiante in listaCurso)
                    {
                       AD_Estudiante.AgregarEstudianteXMateria(estudiante.IdEstudiante,idMateria);

                    }

                    return RedirectToAction("ListadoMaterias", "Materia", new { idCurso = idCurso, esCompleto = false });
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


        public ActionResult ListadoMaterias(int idCurso, bool esCompleto)
        {
            string mensaje = "LISTADO COMPLETO DE Materias";
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
                List<VM_Materia> lista = AD_Materia.ListadoMaterias(idCurso);
                ViewBag.Mensaje = mensaje;
                return View(lista);
            }

            else
            {
                List<VM_Materia> lista = AD_Materia.ListadoMaterias(idCurso);
                
                if (lista.Count==0)
                {
                    lista = new List<VM_Materia>();
                   mensaje="El Curso Seleccionado No Posee Materias Asignadas";

                }
                else
                {
                    mensaje = lista[0].curso;
                }
                
                ViewBag.Mensaje = mensaje;
                return View(lista);
            }
        }


        
        public ActionResult EliminarMateria(int idMateria, int idDocente)
        {
           AD_Materia.EliminarMateria(idMateria, idDocente);

           

            return RedirectToAction("ListadoMaterias", "Materia", new { idCurso = 0, esCompleto = true });

        }



        public ActionResult EditarMateria (VM_Materia modelo, string parametro)
        {

            return RedirectToAction("ListadoMaterias", "Materia", new { idCurso = 0, esCompleto = true });

        }




        public ActionResult insertarEstudianteXMateria(int idCurso, int idMateria)
        {
            
            List<VM_Estudiante> listaCurso = AD_Estudiante.ListadoEstudiantesCursoXID(idCurso);

            foreach (var estudiante in listaCurso)
            {
                AD_Estudiante.AgregarEstudianteXMateria(estudiante.IdEstudiante, idMateria);

            }
            return RedirectToAction("ListadoMaterias", "Materia", new { idCurso, esCompleto = true });

            

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