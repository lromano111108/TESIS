using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubricaWeb.ViewModels
{
    public class VM_ReporteEstudiante
    {
        public double promedio { get; set; }
        public int idNroTema { get; set; }
        public string descripcionTema { get; set; }
        public string materia { get; set; }

    }
}