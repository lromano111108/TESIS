using RubricaWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace RubricaWeb.Models
{
    public class Estudiante
    {
        private int idEstudiante;
        private string nombreEstudiante;
        private string apellidoEstudiante;
        private int idCurso;
        private string dniEstudiante;
  
        public int IdEstudiante { get => idEstudiante; set => idEstudiante = value; }

        [Required]
        public string DniEstudiante { get => dniEstudiante; set => dniEstudiante = value; }

        [Required]
        public string NombreEstudiante { get => nombreEstudiante; set => nombreEstudiante = value; }

        [Required]
        public string ApellidoEstudiante { get => apellidoEstudiante; set => apellidoEstudiante = value; }

        [Required]
        public int IdCurso { get => idCurso; set => idCurso = value; }

        

        



    }
}