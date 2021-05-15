using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;

namespace RubricaWeb.AccesoDatos
{
    public class AD_Estudiante
    {
        public static bool AgregarEstudiante(Estudiante nuevoEstudiante)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"INSERT INTO Estudiantes VALUES(@dniEstudiante,@nombreEstudiante,@idCurso,@apellidoEstudiante, 1 )";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@dniEstudiante", nuevoEstudiante.DniEstudiante);
                cmd.Parameters.AddWithValue("@nombreEstudiante", nuevoEstudiante.NombreEstudiante);
                cmd.Parameters.AddWithValue("@apellidoEstudiante", nuevoEstudiante.ApellidoEstudiante);
                cmd.Parameters.AddWithValue("@idCurso", nuevoEstudiante.IdCurso);

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

        public static List<VM_Estudiante> ListadoEstudiantes()
        {
            List<VM_Estudiante> resultado = new List<VM_Estudiante>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT  e.apellidoEstudiante + ' , ' + e.nombreEstudiante AS 'APELLIDO Y NOMBRE', e.dniEstudiante AS 'DNI' , c.nombreCurso AS 'CURSO'
                                    FROM Estudiantes e
                                    JOIN Cursos c ON c.idCurso = e.idCurso
                                    WHERE e.activo = 1
                                    ORDER BY e.idCurso ASC , e.apellidoEstudiante ASC
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
                        VM_Estudiante itemsLista = new VM_Estudiante();
                        itemsLista.NombreCompleto = dr["APELLIDO Y NOMBRE"].ToString();
                        itemsLista.Dni = dr["DNI"].ToString();
                        itemsLista.Curso = dr["CURSO"].ToString();

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