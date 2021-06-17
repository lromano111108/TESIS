using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubricaWeb.ViewModels
{
    public class VM_Rubrica
    {
        private int idNroTema { get; set; }
        //private string nroCriterio { get; set; }


        private string descripcionTema { get; set; }
        private string numerotema { get; set; }
        private double nota { get; set; }
        private int idEstudiante { get; set; }
        private int idMateria { get; set; }
        private string nombreMateria { get; set; }

        private string nombreEstudiante { get; set; }

        private int idTema { get; set; }
        
        

        private int valorCriterio1 { get; set; }

        private int valorCriterio2 { get; set; }

        private int valorCriterio3 { get; set; }

        private int valorCriterio4 { get; set; }

        private int valorCriterio5 { get; set; }

        private int valorCriterio6 { get; set; }




        public int IdNroTema { get => idNroTema; set => idNroTema = value; }
        //public string NroCriterio { get => nroCriterio; set => nroCriterio = value; }

        public string DescripcionTema { get => descripcionTema; set => descripcionTema = value; }
        public string Numerotema { get => numerotema; set => numerotema = value; }
        public double Nota { get => nota; set => nota = value; }

        ///////////////////////////////////////////

        public int IdMateria { get => idMateria; set => idMateria = value; }
        public string NombreMateria { get => nombreMateria; set => nombreMateria = value; }
        public string NombreEstudiante { get => nombreEstudiante; set => nombreEstudiante = value; }

        public int IdEstudiante { get => idEstudiante; set => idEstudiante = value; }
        public int IdTema { get => idTema; set => idTema = value; }



        /////////////////////////////////////////////////////////////////////////////////
        public int ValorCriterio1 { get => valorCriterio1; set => valorCriterio1 = value; }
        public int ValorCriterio2 { get => valorCriterio2; set => valorCriterio2 = value; }
        public int ValorCriterio3 { get => valorCriterio3; set => valorCriterio3 = value; }
        public int ValorCriterio4 { get => valorCriterio4; set => valorCriterio4 = value; }
        public int ValorCriterio5 { get => valorCriterio5; set => valorCriterio5 = value; }
        public int ValorCriterio6 { get => valorCriterio6; set => valorCriterio6 = value; }

        //////////////////////////////////////////////////////////////////////////////////////




    }
}