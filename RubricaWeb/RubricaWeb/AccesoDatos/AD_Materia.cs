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

                string consulta = @"INSERT INTO Materias VALUES(@nombreMateria,@idCurso, @Activo)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@nombreMateria", nuevaMateria.NombreMateria);
                cmd.Parameters.AddWithValue("@idCurso", nuevaMateria.IdCurso);
                cmd.Parameters.AddWithValue("@Activo", 1);

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

                string consulta = @"SELECT m.materia , c.nombreCurso, D.apellidoDocente + ' , ' + D.nombreDocente AS 'DOCENTE', m.idMateria, m.idCurso, c.orden
                                    FROM Materias M
                                    LEFT JOIN Cursos C ON c.idCurso=m.idCurso
									left JOIN Docentes_por_Materias DM ON DM.idMateria=M.idMateria 
									left JOIN  Docentes D ON D.idDocente=DM.idDocente
                                    WHERE m.activo='True'
                                    
									
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
                        itemsLista.docente = dr["Docente"].ToString();
                        itemsLista.idMateria = int.Parse(dr["idMateria"].ToString());
                        itemsLista.idCurso = int.Parse(dr["idCurso"].ToString());
                        itemsLista.orden = int.Parse(dr["orden"].ToString());


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
                                    where idCurso= @idCurso
                                    order by 2
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

        public static bool EliminarMateria(int idMateria)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"UPDATE Materias
                                    SET[Activo] = 'False'
                                    WHERE idMAteria = @IdMateria";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdMateria", idMateria);


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

                string consulta = @"SELECT M.materia, C.nombreCurso
                                    FROM Docentes_por_Materias DM
                                    JOIN Materias M ON M.idMateria=DM.idMateria
                                    JOIN Docentes DO ON DO.idDocente=DM.idDocente
                                    JOIN Cursos C ON C.idCurso=M.idCurso
                                    WHERE DO.idDocente=@idDocente;
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