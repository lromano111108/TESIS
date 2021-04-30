using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubricaWeb.ViewModels
{
    public class VM_Curso
    {
        private int idCurso;
        private string nombreCurso;
        public int IdCurso { get => idCurso; set => idCurso = value; }

        public string NombreCurso { get => nombreCurso; set => nombreCurso = value; }

    }
}