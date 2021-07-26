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
            List<VM_Rol> listaDocentes = AD_ViewModel.ListaDeRoles();
            List<SelectListItem> items = listaDocentes.ConvertAll(i =>
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


        [HttpPost]
        public ActionResult CargaDocente(Docente model)
        {
            if (ModelState.IsValid)
            {
                bool resultado = AD_Docente.AgregarDocente(model);

                if (resultado)
                {

                    return RedirectToAction("ListadoDocentes", "Docente");
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



        public ActionResult ListadoDocentes()
        {

            List<VM_Docente> lista = AD_Docente.ListadoDocentes();
            return View(lista);
        }



        public ActionResult AsignarMateriasDocentes(int id, string parametro)
        {
            ViewBag.id = id;
            ViewBag.parametro = parametro;




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



            if (parametro.Equals("completo") && id == 0)
            {
                return View();

            }

            else
            {
                if (parametro.Equals("docente"))
                {
                    ViewBag.Titulo="Asignar Materia";
                    VM_DocenteXMateria docente = new VM_DocenteXMateria();
                    docente.idDocente = id;
                    VM_Docente infoDocente = AD_Docente.ObtenerDocenteXId(id);
                    ViewBag.NombreDocente = infoDocente.NombreDocente;
                    return View(docente);
                }
                else if(parametro.Equals("materia"))
                {
                    ViewBag.Titulo = "Asignar Docente";

                    VM_Materia materia = AD_Materia.infoMateria(id);
                    ViewBag.Materia= materia.materia;//nombre materia
                    ViewBag.Curso= materia.curso;//noombre Curso
                    
                    VM_DocenteXMateria docenteXMateria = new VM_DocenteXMateria();
                    docenteXMateria.idMateria = id;
                    return View(docenteXMateria);
                }
            }
            return View();


        }

        [HttpPost]
        public ActionResult AsignarMateriasDocentes(VM_DocenteXMateria model)
        {
            if(AD_Materia.DocenteConMateria(model))
            {
                return RedirectToAction("CargaDocente", "Docente");
            }

           else if (ModelState.IsValid)
            {          
                bool resultado = AD_ViewModel.cargarDocentesXMateria(model);

                if (resultado)
                {
                    return RedirectToAction("ListadoDocentes", "Docente");
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

        [HttpGet]
        public JsonResult Materia(int IdCurso)
        {
            List<Materia> listaMaterias = AD_Materia.ComboMateriaId(IdCurso);
            List<SelectListItem> itemsMaterias = listaMaterias.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NombreMateria,
                    Value = i.IdMateria.ToString(),

                    Selected = false
                };
            });

            return Json(listaMaterias, JsonRequestBehavior.AllowGet);

        }


        public ActionResult EliminarDocente(int idDocente)
        {
            AD_Docente.EliminarDocente(idDocente);



            return RedirectToAction("ListadoDocentes", "Docente");

        }

        public ActionResult BajaMateriaDocente(int idDocente, int idMateria)
        {
            AD_Docente.BajaMateriaDeDocente(idDocente, idMateria);



            return RedirectToAction("MostrarDatosDocentes", "Docente", new {idDocente });

        }


        public ActionResult MostrarDatosDocentes(int idDocente)
        {
            VM_Docente resultado = AD_Docente.ObtenerDocenteXId(idDocente);
            List<VM_Materia> lista = AD_Materia.ListaMateriasPorDocentes(idDocente);
            ViewBag.listaMaterias = lista;
            ViewBag.docente = resultado;

            return View(resultado);          
            
        }
    




        public ActionResult PanelDocente(int idDocente)
        {
            VM_Docente resultado = AD_Docente.ObtenerDocenteXId(idDocente);
            List<VM_Materia> lista = AD_Materia.ListaMateriasPorDocentes(idDocente);
            ViewBag.listaMaterias = lista;
            ViewBag.docente = resultado;
            int idMateria = 0;
            foreach (var item in lista)
            {
                idMateria = item.idMateria;
            }

            List<VM_Tema> listaCursos = AD_Rubrica.comboTemasCargados();
            List<SelectListItem> itemsTemas = listaCursos.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NumeroDeTema,
                    Value = i.IdTema.ToString(),

                    Selected = false
                };
            });
            ViewBag.itemsTemas = itemsTemas;

            //List<VM_Tema> listaTemas = AD_ViewModel.ListaDeCursos();
            //List<SelectListItem> itemsTemas = listaTemas.ConvertAll(i =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = i.NombreCurso,
            //        Value = i.IdCurso.ToString(),

            //        Selected = false
            //    };
            //});
         



            return View(resultado);

        }





        public ActionResult PanelDocenteCopia(int idDocente)
        {
            VM_Docente resultado = AD_Docente.ObtenerDocenteXId(idDocente);
            List<VM_Materia> lista = AD_Materia.ListaMateriasPorDocentes(idDocente);
            ViewBag.listaMaterias = lista;
            ViewBag.docente = resultado;
            int idMateria = 0;
            foreach (var item in lista)
            {
                idMateria = item.idMateria;
            }

            List<VM_Tema> listaCursos = AD_Rubrica.comboTemasCargados();
            List<SelectListItem> itemsTemas = listaCursos.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.NumeroDeTema,
                    Value = i.IdTema.ToString(),

                    Selected = false
                };
            });
            ViewBag.itemsTemas = itemsTemas;

            //List<VM_Tema> listaTemas = AD_ViewModel.ListaDeCursos();
            //List<SelectListItem> itemsTemas = listaTemas.ConvertAll(i =>
            //{
            //    return new SelectListItem()
            //    {
            //        Text = i.NombreCurso,
            //        Value = i.IdCurso.ToString(),

            //        Selected = false
            //    };
            //});




            return View(resultado);

        }
        public ActionResult EditarDocente(int idDocente)
        {
            List<VM_Rol> listaDocentes = AD_ViewModel.ListaDeRoles();
            List<SelectListItem> items = listaDocentes.ConvertAll(i =>
            {
                return new SelectListItem()
                {
                    Text = i.Rol,
                    Value = i.IdRol.ToString(),

                    Selected = false
                };
            });
            ViewBag.items = items;


            Docente resultado = AD_Docente.DocenteParaEditar(idDocente);
            return View(resultado);
           
        }


        [HttpPost]
        public ActionResult EditarDocente(Docente model)
        {

            if (ModelState.IsValid)
            {
                bool resultado = AD_Docente.ActualizarDatosDocente(model);
                if (resultado)
                {
                   
                    return RedirectToAction("MostrarDatosDocentes", "Docente", new { idDocente = model.IdDocente});
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