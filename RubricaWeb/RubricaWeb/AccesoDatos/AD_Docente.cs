using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;

namespace RubricaWeb.AccesoDatos
{
    public class AD_Docente
    {
        public static bool AgregarDocente(Docente nuevoDocente)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"INSERT INTO Materias VALUES(@dniDocente,@nombreDocente, @idRol, @apellidoDocente)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@dniDocente", nuevoDocente.DniDocente);
                cmd.Parameters.AddWithValue("@nombreDocente", nuevoDocente.NombreDocente);
                cmd.Parameters.AddWithValue("@idRol", nuevoDocente.IdRol);
                cmd.Parameters.AddWithValue("@apellidoDocente", nuevoDocente.ApellidoDocente);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                cn.Close();
            }

            return resultado;
        }











    }
}