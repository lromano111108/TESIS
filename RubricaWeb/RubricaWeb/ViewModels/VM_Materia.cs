using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubricaWeb.ViewModels
{
    public class VM_Materia
    {
        public string materia { get; set; }
        public string curso { get; set; }
        public string docente { get; set; }
        public int  idMateria { get; set; }
        public int idCurso { get; set; }
        public int orden { get; set; }
        public int horas { get; set; }

        public int idDocente { get; set; }

        public int idEstudiante { get; set; }
    }
}