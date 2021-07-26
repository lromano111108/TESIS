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
        public static VM_Curso ObtenerCursoXId(int idCurso)
        {
            VM_Curso resultado = new VM_Curso();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT idCurso, nombreCurso FROM CURSOS
                                    WHERE idCurso=@idCurso
                                    ; ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idCurso", idCurso);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); //ejecuta la sentencia sql

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.IdCurso = int.Parse(dr["IdCurso"].ToString());
                        resultado.NombreCurso = dr["NombreCurso"].ToString();
                        

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            finally //si hay o no hay error haga un connexion.close -SIEMPRE CERRAMOS LA CONEXION!!!!
            {
                cn.Close();
            }

            return resultado;
        }

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
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "sp_InsertarDocenteXMateria";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //string consulta = @"EXEC sp_InsertarDocenteXMateria (@idMateria, @idDocente)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter ("@idMateria", nuevaCarga.idMateria));
                cmd.Parameters.Add(new SqlParameter("@idDocente", nuevaCarga.idDocente));

              

                                

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

        public static List<VM_ComboValorCriterios> ComboValorCriterios()
        {
            List<VM_ComboValorCriterios> resultado = new List<VM_ComboValorCriterios>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT * FROM ValoresCriterios";
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
                        VM_ComboValorCriterios valor = new VM_ComboValorCriterios();
                        valor.IdValor = int.Parse(dr["idValor"].ToString());
                        valor.ValorCriterio = int.Parse(dr["valor"].ToString());

                        resultado.Add(valor);
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