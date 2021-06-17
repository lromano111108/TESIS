using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;


namespace RubricaWeb.AccesoDatos
{
    public class AD_Login
    {
        public static Docente comprobarQueHayDocente(Login modelo)
        {
            Docente resultado = new Docente();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT idDocente, idRol FROM DOCENTES 
                                    where dniDocente = @usuario ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@usuario", modelo.Usuario);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.IdDocente = int.Parse(dr["idDocente"].ToString());                        
                        resultado.IdRol = int.Parse(dr["idRol"].ToString());

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                cn.Close();
            }

            return resultado;
        }


        public static bool AgregarPassword(Login modelo)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"UPDATE [dbo].[Docentes]
                                   SET password = @password
                                      
                                 WHERE dniDocente = @usuario
                                   ";
                cmd.Parameters.Clear();//si tenia un parametro cargado, que lo limpie
                cmd.Parameters.AddWithValue("@password", modelo.Password);
                cmd.Parameters.AddWithValue("@usuario", modelo.Usuario);
                //cmd.Parameters.AddWithValue("@IdCurso", nuevoEstudiante.IdCurso);
               

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                cn.Close();
            }

            return resultado;
        }


        public static Docente Ingresar(Login modelo)
        {
            Docente resultado = new Docente();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT idDocente, idRol FROM DOCENTES 
                                    where dniDocente = @usuario
									AND password = @password ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@usuario", modelo.Usuario);
                cmd.Parameters.AddWithValue("@password", modelo.Password);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.IdDocente = int.Parse(dr["idDocente"].ToString());
                        resultado.IdRol = int.Parse(dr["idRol"].ToString());

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                cn.Close();
            }

            return resultado;
        }

    }












}
