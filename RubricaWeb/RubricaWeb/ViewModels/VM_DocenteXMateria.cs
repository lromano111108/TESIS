using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RubricaWeb.ViewModels
{
    public class VM_DocenteXMateria
    {
        [Required]
        public int idDocente { get; set; }

        [Required]
        public int idMateria { get; set; }
    }
}