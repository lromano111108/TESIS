using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubricaWeb.ViewModels
{
    public class VM_LibretaNotasMateria
    {
        public string NombreEstudiante { get; set; }

        public double Nota1 { get; set; }

        public double Nota2 { get; set; }

        public double Nota3 { get; set; }
        public double Nota4 { get; set; }
        public double Nota5 { get; set; }
        public double Nota6 { get; set; }

        public double Nota7 { get; set; }
        public double Nota8 { get; set; }
        public double Nota9 { get; set; }
        public double Nota10 { get; set; }
        public double Nota11 { get; set; }
        public double Nota12 { get; set; }

        public double Promedio { get; set; }

        public int IdMateria { get; set; }

        public int IdTema { get; set; }


    }
}