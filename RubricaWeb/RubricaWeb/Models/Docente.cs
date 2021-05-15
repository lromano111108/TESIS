using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RubricaWeb.Models
{
    public class Docente
    {
        private int idDocente;
        private string dniDocente;
        private string nombreDocente;
        private string apellidoDocente;
        private int idRol;
        private string direccion;
        private string telefono;
        private string email;


        public int IdDocente { get => idDocente; set => idDocente = value; }

        [Required]
        public string DniDocente { get => dniDocente; set => dniDocente = value; }

        [Required] 
        public string NombreDocente { get => nombreDocente; set => nombreDocente= value; }

        [Required] 
        public string ApellidoDocente { get => apellidoDocente; set => apellidoDocente= value; }

        [Required] 
        public int IdRol { get => idRol; set => idRol= value; }
        [Required]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required]
        public string Telefono { get => telefono; set => telefono = value; }
        [Required]
        public string Email { get => email; set => email = value; }


    }
}