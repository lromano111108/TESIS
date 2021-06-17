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

                string consulta = @"SELECT  e.apellidoEstudiante + ' , ' + e.nombreEstudiante AS 'APELLIDO Y NOMBRE', e.dniEstudiante AS 'DNI' , c.nombreCurso AS 'CURSO', c.orden, e.idEstudiante, e.idCurso
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
                        itemsLista.Orden = int.Parse(dr["orden"].ToString());
                        itemsLista.IdEstudiante = int.Parse(dr["idEstudiante"].ToString());
                        itemsLista.IdCurso = int.Parse(dr["idCurso"].ToString());

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

        public static List<VM_Estudiante> ListadoEstudiantesXId(int idCurso)
        {
            List<VM_Estudiante> resultado = new List<VM_Estudiante>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT  e.apellidoEstudiante + ' , ' + e.nombreEstudiante AS 'APELLIDO Y NOMBRE', e.dniEstudiante AS 'DNI' , c.nombreCurso AS 'CURSO', c.orden, e.idEstudiante, e.idCurso
                                    FROM Estudiantes e
                                    JOIN Cursos c ON c.idCurso = e.idCurso
                                    WHERE e.activo = 1
									AND c.idCurso = @idCurso
                                    ORDER BY c.orden ASC , e.apellidoEstudiante ASC
                                    ";
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;
                cmd.Parameters.AddWithValue("@idCurso", idCurso);
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
                        itemsLista.Orden = int.Parse(dr["orden"].ToString());
                        itemsLista.IdEstudiante = int.Parse(dr["idEstudiante"].ToString());
                        itemsLista.IdCurso = int.Parse(dr["idCurso"].ToString());

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
        public static VM_Estudiante ObtenerEstudianteXId(int idEstudiante)
        {
            VM_Estudiante resultado = new VM_Estudiante();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT  e.apellidoEstudiante + ' , ' + e.nombreEstudiante AS 'APELLIDO Y NOMBRE', e.dniEstudiante AS 'DNI' , c.orden, e.idEstudiante, e.idCurso, e.activo, c.nombreCurso
                                    FROM Estudiantes e
                                    JOIN Cursos c ON c.idCurso = e.idCurso
                                    WHERE e.activo = 1
									AND e.idEstudiante= @idEstudiante";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); 

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        resultado.NombreCompleto = dr["APELLIDO Y NOMBRE"].ToString();
                        resultado.Dni = dr["DNI"].ToString();
                        resultado.Curso = dr["nombreCurso"].ToString();
                        resultado.Orden = int.Parse(dr["orden"].ToString());
                        resultado.IdEstudiante = int.Parse(dr["idEstudiante"].ToString());
                        resultado.IdCurso = int.Parse(dr["idCurso"].ToString());
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



        public static List<VM_Materia> ListaMateriaPorEstudiante(int idEstudiante)
        {
            List<VM_Materia> resultado = new List<VM_Materia>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT M.materia, C.nombreCurso
                                    FROM CURSOS C
                                    JOIN Estudiantes E ON E.idCurso= C.idCurso
                                    JOIN Materias M ON M.idCurso = C.idCurso
                                    WHERE E.idEstudiante = @idEstudiante
                                    AND M.Activo= 'True'
                                    ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);

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



        public static Estudiante EstudianteParaEditar(int idEstudiante)
        {
            Estudiante resultado = new Estudiante();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT * FROM ESTUDIANTES E
                                    where E.activo = 'true'
                                    AND E.idEstudiante= @idEstudiante
                                    ORDER BY apellidoEstudiante ASC; ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.IdEstudiante = int.Parse(dr["IdEstudiante"].ToString());
                        resultado.NombreEstudiante = dr["NombreEstudiante"].ToString();
                        resultado.ApellidoEstudiante = dr["ApellidoEstudiante"].ToString();
                        resultado.DniEstudiante = dr["DniEstudiante"].ToString();                        
                        resultado.IdCurso = int.Parse(dr["IdCurso"].ToString());

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





        public static bool ActualizarDatosEstudiante(Estudiante nuevoEstudiante)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try 
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"UPDATE [dbo].[Estudiantes]
                                   SET [dniEstudiante] = @DniEstudiante
                                      ,[nombreEstudiante] = @NombreEstudiante
                                      ,[idCurso] = @IdCurso
                                      ,[apellidoEstudiante] = @ApellidoEstudiante
                                      ,[activo] = 1
                                 WHERE idEstudiante = @IdEstudiante
                                   ";
                cmd.Parameters.Clear();//si tenia un parametro cargado, que lo limpie
                cmd.Parameters.AddWithValue("@DniEstudiante", nuevoEstudiante.DniEstudiante);
                cmd.Parameters.AddWithValue("@NombreEstudiante", nuevoEstudiante.NombreEstudiante);
                cmd.Parameters.AddWithValue("@IdCurso", nuevoEstudiante.IdCurso);
                cmd.Parameters.AddWithValue("@ApellidoEstudiante", nuevoEstudiante.ApellidoEstudiante);                
                cmd.Parameters.AddWithValue("@IdEstudiante", nuevoEstudiante.IdEstudiante);


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


        public static List<VM_Estudiante> ListadoEstudiantesCursoXID(int idCurso)
        {
            List<VM_Estudiante> resultado = new List<VM_Estudiante>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT E.apellidoEstudiante + ', ' + E.nombreEstudiante  AS 'NOMBRE ESTUDIANTE', E.idCurso, E.IdEstudiante
                                    FROM Estudiantes e
                                    JOIN CURSOS C ON C.idCurso= E.idCurso
                                    WHERE E.idCurso= @idCurso
                                    AND E.activo = 1
                                    ORDER BY E.apellidoEstudiante ASC
                                    ";
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;
                cmd.Parameters.AddWithValue("@idCurso", idCurso);
                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_Estudiante itemsLista = new VM_Estudiante();
                        itemsLista.NombreCompleto = dr["NOMBRE ESTUDIANTE"].ToString();
                        //itemsLista.Dni = dr["DNI"].ToString();
                        //itemsLista.Curso = dr["CURSO"].ToString();
                        itemsLista.IdCurso = int.Parse(dr["idCurso"].ToString());
                        itemsLista.IdEstudiante = int.Parse(dr["idEstudiante"].ToString());
                        //itemsLista.IdCurso = int.Parse(dr["idCurso"].ToString());

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