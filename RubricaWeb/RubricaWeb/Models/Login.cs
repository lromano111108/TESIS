using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RubricaWeb.Models
{
    public class Login
    {
        private string usuario;
        private string password;
        private string password2;
        private int idUsuario;


        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Password { get => password; set => password = value; }
        public string Password2 { get => password2; set => password2 = value; }





    }
}