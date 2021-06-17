using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubricaWeb.ViewModels
{
    public class VM_Estudiante
    {
        private string nombreCompleto;
        private string curso;
        private string dni;
        private int idEstudiante;
        private int orden;
        private int idCurso;

        //public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        //public string Curso { get => curso; set => curso = value; }
        //public string Dni { get => dni; set => dni = value; }

        //public int IdEstudiante { get => idEstudiante; set => idEstudiante = value; }
        //public int Orden { get => orden; set => orden = value; }

        //public int IdCurso { get => idCurso; set => idCurso = value; }

        public string NombreCompleto { get; set ; }
        public string Curso { get; set; }
        public string Dni { get; set; }

        public int IdEstudiante { get; set; }
        public int Orden { get; set; }

        public int IdCurso { get; set; }
        }
}