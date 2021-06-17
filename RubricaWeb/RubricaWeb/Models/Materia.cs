using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RubricaWeb.Models
{
    public class Materia
    {
        private int idMateria;
        private string nombreMateria;
        private int idCurso;
        private int horas;

        public int IdMateria { get => idMateria; set => idMateria = value; }

        [Required]
        public string NombreMateria { get => nombreMateria; set => nombreMateria = value; }

        [Required]
        public int IdCurso { get => idCurso; set => idCurso= value; }


        [Required]
        public int Horas { get => horas; set => horas = value; }
    }
}