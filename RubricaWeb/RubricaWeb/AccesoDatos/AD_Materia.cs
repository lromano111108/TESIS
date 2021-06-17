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

                string consulta = @"INSERT INTO Materias VALUES(@nombreMateria,@idCurso, @Activo, @horas)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombreMateria", nuevaMateria.NombreMateria);
                cmd.Parameters.AddWithValue("@idCurso", nuevaMateria.IdCurso);
                cmd.Parameters.AddWithValue("@Activo", 1);
                cmd.Parameters.AddWithValue("@horas", nuevaMateria.Horas);

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

                string consulta = @"SELECT m.materia , c.nombreCurso, D.apellidoDocente + ' , ' + D.nombreDocente AS 'DOCENTE', m.idMateria, m.idCurso, c.orden, m.horas, D.idDocente
                                    FROM Materias M
                                    LEFT JOIN Cursos C ON c.idCurso=m.idCurso
									left JOIN Docentes_por_Materias DM ON DM.idMateria=M.idMateria 
									left JOIN  Docentes D ON D.idDocente=DM.idDocente
                                    WHERE m.activo='True'																	                                 																		
									EXCEPT 
									SELECT m.materia , c.nombreCurso, D.apellidoDocente + ' , ' + D.nombreDocente AS 'DOCENTE', m.idMateria, m.idCurso, c.orden, m.horas , D.idDocente
                                    FROM Materias M
                                    LEFT JOIN Cursos C ON c.idCurso=m.idCurso
									left JOIN Docentes_por_Materias DM ON DM.idMateria=M.idMateria 
									left JOIN  Docentes D ON D.idDocente=DM.idDocente
                                    WHERE DM.activo='False'
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
                        itemsLista.docente = dr["Docente"].ToString();
                        itemsLista.idMateria = int.Parse(dr["idMateria"].ToString());
                        itemsLista.idCurso = int.Parse(dr["idCurso"].ToString());
                        itemsLista.orden = int.Parse(dr["orden"].ToString());
                        itemsLista.horas = int.Parse(dr["horas"].ToString());
                        string idDocente = dr["idDocente"].ToString();
                        if (String.IsNullOrEmpty(idDocente))
                        {
                            itemsLista.idDocente = 0;
                        }
                        else
                        {
                            itemsLista.idDocente = int.Parse(idDocente);
                        }
                        


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


        public static List<Materia> ComboMateria()
        {
            List<Materia> resultado = new List<Materia>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT idMateria, materia
                                    FROM Materias
                                    order by 2
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
                        Materia itemsLista = new Materia();
                        itemsLista.NombreMateria = dr["Materia"].ToString();
                        itemsLista.IdMateria = int.Parse(dr["idMateria"].ToString());

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



        public static List<Materia> ComboMateriaId(int id)
        {
            List<Materia> resultado = new List<Materia>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT idMateria, materia
                                    FROM Materias
                                    WHERE idCurso= @idCurso
                                    EXCEPT
                                    SELECT DM.idMateria, m.materia
                                    FROM Docentes_por_Materias DM
                                    JOIN Materias M ON m.idMateria = DM.idMateria
                                    JOIN Cursos C ON m.idCurso = c.idCurso
                                    WHERE DM.Activo='true'
                                    AND m.idCurso = @idCurso 
                                    ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idCurso", id);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        Materia itemsLista = new Materia();
                        itemsLista.NombreMateria = dr["Materia"].ToString();
                        itemsLista.IdMateria = int.Parse(dr["idMateria"].ToString());

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

        public static bool EliminarMateria(int idMateria, int idDocente)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"EXEC sp_eliminarMateria @idMateria= '@idMateria', @idDocente = '@idDocente'";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idMateria", idMateria);
                cmd.Parameters.AddWithValue("@idDocente", idDocente);



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

        public static List<VM_Materia> ListaMateriasPorDocentes(int idDocente)
        {
            List<VM_Materia> resultado = new List<VM_Materia>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT M.materia, C.nombreCurso, M.idMateria, C.idCurso
                                    FROM Docentes_por_Materias DM
                                    JOIN Materias M ON M.idMateria=DM.idMateria
                                    JOIN Docentes DO ON DO.idDocente=DM.idDocente
                                    JOIN Cursos C ON C.idCurso=M.idCurso
                                    WHERE DO.idDocente=@idDocente
                                    AND DM.Activo = 'true'
                                    ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDocente", idDocente);

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
                        itemsLista.idMateria = int.Parse(dr["idMateria"].ToString());
                        itemsLista.idCurso = int.Parse(dr["idCurso"].ToString());


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

        public static bool DocenteConMateria(VM_DocenteXMateria modelo)
        {
            VM_DocenteXMateria resultado = new VM_DocenteXMateria();
            bool existe = false; //no devuelve nada
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT *
                                    FROM Docentes_por_Materias
                                    WHERE idDocente= @idDocente
                                    AND idMateria=@idMateria; ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDocente", modelo.idDocente);
                cmd.Parameters.AddWithValue("@idMateria", modelo.idMateria);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); //ejecuta la sentencia sql

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.idDocente = int.Parse(dr["idDocente"].ToString());
                        resultado.idMateria = int.Parse(dr["idMateria"].ToString());
                        
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
            if ((resultado.idDocente & resultado.idMateria) !=0)
            {
                existe = true;
            }
            return existe;
        }

        public static VM_Materia datosMateria(int idMateria)
        {
            VM_Materia resultado = new VM_Materia();
            
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT M.idMateria, M.idCurso, M.Materia, c.nombreCurso
                                    FROM Materias M
                                    JOIN Cursos C ON c.idCurso=m.idCurso
                                    WHERE idMateria = @idMateria
                                    AND activo = 'true' ";
                cmd.Parameters.Clear();
              
                cmd.Parameters.AddWithValue("@idMateria", idMateria);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); //ejecuta la sentencia sql

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.idCurso = int.Parse(dr["idCurso"].ToString());
                        resultado.idMateria = int.Parse(dr["idMateria"].ToString());
                        resultado.materia = dr["Materia"].ToString();
                        resultado.curso = dr["nombreCurso"].ToString();


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



    }
}