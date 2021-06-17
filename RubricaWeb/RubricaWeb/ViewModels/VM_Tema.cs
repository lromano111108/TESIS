using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubricaWeb.ViewModels
{
    public class VM_Tema
    {
        private int idTema { get; set; }
        private int idNroTema { get; set; }
        private string numeroDeTema { get; set; }

        private string descripcionTema { get; set; }

        private int idMateria { get; set; }
        private string aprendizaje { get; set; }

        private int idCurso { get; set; }
        //private string nombreMateria { get; set ; }

        private string descripcionCriterio1 { get; set; }
        private string descripcionCriterio2 { get; set; }
        private string descripcionCriterio3 { get; set; }
        private string descripcionCriterio4 { get; set; }
        private string descripcionCriterio5 { get; set; }
        private string descripcionCriterio6 { get; set; }






        /////////////////////////////////////////////
        public int IdTema { get => idTema; set => idTema = value; }
        public int IdNroTema { get => idNroTema; set => idNroTema = value; }
        public string NumeroDeTema { get => numeroDeTema; set => numeroDeTema = value; }

        public string DescripcionTema { get => descripcionTema; set => descripcionTema = value; }
        public string Aprendizaje { get => aprendizaje; set => aprendizaje = value; }

        //public string NombreMateria { get => nombreMateria; set => nombreMateria = value; }

        public int Idcurso { get => idCurso; set => idCurso = value; }
        public int IdMateria { get => idMateria; set => idMateria = value; }

        public string DescripcionCriterio1 { get => descripcionCriterio1; set => descripcionCriterio1 = value; }

        public string DescripcionCriterio2 { get => descripcionCriterio2; set => descripcionCriterio2= value; }
        public string DescripcionCriterio3 { get => descripcionCriterio3; set => descripcionCriterio3 = value; }
        public string DescripcionCriterio4 { get => descripcionCriterio4; set => descripcionCriterio4 = value; }
        public string DescripcionCriterio5 { get => descripcionCriterio5; set => descripcionCriterio5 = value; }
        public string DescripcionCriterio6 { get => descripcionCriterio6; set => descripcionCriterio6 = value; }



    }
}