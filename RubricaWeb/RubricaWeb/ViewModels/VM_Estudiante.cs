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


        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public string Curso { get => curso; set => curso = value; }
        public string Dni { get => dni; set => dni = value; }



    }
}