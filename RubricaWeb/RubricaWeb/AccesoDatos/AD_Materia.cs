using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;

namespace RubricaWeb.AccesoDatos
{
    public class AD_Materia
    {
        public static bool AgregarMateria(Materia nuevaMateria)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"INSERT INTO Materias VALUES(@nombreMateria,@idCurso)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombreMateria", nuevaMateria.NombreMateria);
                cmd.Parameters.AddWithValue("@idCurso", nuevaMateria.IdCurso);

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


        public static List<VM_Materia> ListadoMaterias()
        {
            List<VM_Materia> resultado = new List<VM_Materia>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT m.materia , c.nombreCurso
                                    FROM Materias M
                                    JOIN Cursos C ON c.idCurso=m.idCurso
                                    ORDER BY c.idCurso ASC, M.materia ASC
                                    ";
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_Materia itemsLista = new VM_Materia();
                        itemsLista.materia = dr["Materia"].ToString();
                        itemsLista.curso = dr["nombreCurso"].ToString();

                        resultado.Add(itemsLista);
                    }
                }

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