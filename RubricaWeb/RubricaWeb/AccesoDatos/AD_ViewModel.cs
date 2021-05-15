using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;

namespace RubricaWeb.AccesoDatos
{
    public class AD_ViewModel
    {

        public static List<VM_Curso> ListaDeCursos()
        {
            List<VM_Curso> resultado = new List<VM_Curso>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT * FROM Cursos
                                    ORDER BY 3 ASC";
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
                        VM_Curso curso = new VM_Curso();
                        curso.IdCurso = int.Parse(dr["idCurso"].ToString());
                        curso.NombreCurso = dr["nombreCurso"].ToString();                        

                        resultado.Add(curso);
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

        public static List<VM_Rol> ListaDeRoles()
        {
            List<VM_Rol> resultado = new List<VM_Rol>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT * FROM Roles";
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
                        VM_Rol rol = new VM_Rol();
                        rol.IdRol = int.Parse(dr["idRol"].ToString());
                        rol.Rol = dr["descripcionRol"].ToString();

                        resultado.Add(rol);
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


        public static bool cargarDocentesXMateria(VM_DocenteXMateria nuevaCarga)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"INSERT INTO Docentes_por_Materias VALUES(@idMateria,@idDocente, @Activo)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idMateria", nuevaCarga.idMateria);
                cmd.Parameters.AddWithValue("@idDocente", nuevaCarga.idDocente);
                cmd.Parameters.AddWithValue("@Activo", "true");

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