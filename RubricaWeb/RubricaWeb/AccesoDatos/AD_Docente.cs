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

        public static VM_Docente ObtenerDocenteXId(int idDocente)
        {
            VM_Docente resultado = new VM_Docente();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString(); 

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT d.nombreDocente + ' ' + D.apellidoDocente as 'Nombre Completo', D.dniDocente , D.Direccion,D.telefono,D.email,r.descripcionRol AS 'Rol Principal', d.idDocente
                                    FROM Docentes D
                                    JOIN Roles r ON r.idRol = D.idRol
                                    where d.activo = 'true'

                                    AND d.idDocente = @idDocente
                                    ORDER BY apellidoDocente ASC; ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDocente", idDocente); 

                cmd.CommandType = System.Data.CommandType.Text; 
                cmd.CommandText = consulta; 

                cn.Open(); 
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); //ejecuta la sentencia sql

                if (dr != null)
                {
                    while (dr.Read()) 
                    {

                        resultado.IdDocente = int.Parse(dr["idDocente"].ToString());
                        resultado.NombreDocente = dr["Nombre Completo"].ToString();
                        resultado.Rol = dr["Rol Principal"].ToString();                      
                        resultado.DniDocente = dr["dniDocente"].ToString();
                        resultado.Direccion = dr["Direccion"].ToString();
                        resultado.Telefono = dr["telefono"].ToString();
                        resultado.Email = dr["email"].ToString();

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




        public static bool AgregarDocente(Docente nuevoDocente)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"INSERT INTO Docentes VALUES(@dniDocente,@nombreDocente, @idRol, @apellidoDocente,1,@direccion, @telefono, @email)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@dniDocente", nuevoDocente.DniDocente);
                cmd.Parameters.AddWithValue("@nombreDocente", nuevoDocente.NombreDocente);
                cmd.Parameters.AddWithValue("@idRol", nuevoDocente.IdRol);
                cmd.Parameters.AddWithValue("@apellidoDocente", nuevoDocente.ApellidoDocente);
                cmd.Parameters.AddWithValue("@direccion", nuevoDocente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", nuevoDocente.Telefono);
                cmd.Parameters.AddWithValue("@email", nuevoDocente.Email);


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



        public static List<VM_Docente> ListadoDocentes()
        {
            List<VM_Docente> resultado = new List<VM_Docente>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT d.apellidoDocente + ', ' + D.nombreDocente as 'NOMBRE COMPLETO', D.dniDocente , r.descripcionRol Rol, d.idDocente
                                    FROM Docentes D
                                    JOIN Roles r ON r.idRol=D.idRol
                                    where d.activo= 'true'
                                    ORDER BY apellidoDocente ASC
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
                        VM_Docente itemsLista = new VM_Docente();
                        itemsLista.NombreDocente = dr["NOMBRE COMPLETO"].ToString();
                        itemsLista.DniDocente = dr["dniDocente"].ToString();
                        itemsLista.Rol = dr["Rol"].ToString();
                        itemsLista.IdDocente = int.Parse(dr["idDocente"].ToString());

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

        public static List<Docente> ComboDocentes()
        {
            List<Docente> resultado = new List<Docente>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"select idDocente , apellidoDocente + ' , ' + nombreDocente AS 'NOMBRE'
                                    from Docentes
                                    order by 2 asc
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
                        Docente itemsLista = new Docente();
                        itemsLista.NombreDocente = dr["NOMBRE"].ToString();
                        itemsLista.IdDocente = int.Parse(dr["idDocente"].ToString());
                        

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


        public static bool EliminarDocente(int idDocente)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"UPDATE Docentes
                                    SET[Activo] = 'False'
                                    WHERE idDocente = @idDocente";
                cmd.Parameters.Clear();
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

        public static bool BajaMateriaDeDocente (int idDocente, int idMateria)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"update Docentes_por_Materias
                                    SET [Activo] = 'False'
                                    WHERE idDocente=@idDocente
                                    AND idMateria= @idMateria";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idDocente", idDocente);
                cmd.Parameters.AddWithValue("@idMateria", idMateria);


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



        public static Docente DocenteParaEditar(int idDocente)
        {
            Docente resultado = new Docente();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT * FROM DOCENTES D
                                    where d.activo = 'true'
                                    AND d.idDocente = @idDocente
                                    ORDER BY apellidoDocente ASC; ";
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

                        resultado.IdDocente = int.Parse(dr["idDocente"].ToString());
                        resultado.NombreDocente = dr["nombreDocente"].ToString();
                        resultado.ApellidoDocente = dr["apellidoDocente"].ToString();
                        resultado.DniDocente = dr["dniDocente"].ToString();
                        resultado.Direccion = dr["Direccion"].ToString();
                        resultado.Telefono = dr["telefono"].ToString();
                        resultado.Email = dr["email"].ToString();
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



        public static bool ActualizarDatosDocente(Docente nuevoDocente)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString(); 

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try //va a intentar realizar la accion y sino la exepcion va a realizarla
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"UPDATE DOCENTES  
                                    SET [dniDocente] = @dniDocente
                                  ,[nombreDocente] = @nombreDocente
                                  ,[idRol] = @idRol
                                  ,[apellidoDocente] = @apellidoDocente
                                  ,[activo] = 1
                                  ,[Direccion] = @direccion
                                  ,[telefono] = @telefono
                                  ,[email] = @email
                                    WHERE idDocente = @idDocente";
                cmd.Parameters.Clear();//si tenia un parametro cargado, que lo limpie
                cmd.Parameters.AddWithValue("@dniDocente", nuevoDocente.DniDocente);
                cmd.Parameters.AddWithValue("@nombreDocente", nuevoDocente.NombreDocente);
                cmd.Parameters.AddWithValue("@idRol", nuevoDocente.IdRol);
                cmd.Parameters.AddWithValue("@apellidoDocente", nuevoDocente.ApellidoDocente);
                cmd.Parameters.AddWithValue("@direccion", nuevoDocente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", nuevoDocente.Telefono);
                cmd.Parameters.AddWithValue("@email", nuevoDocente.Email);
                cmd.Parameters.AddWithValue("@idDocente", nuevoDocente.IdDocente);


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


        public static VM_Docente ObtenerDocenteXMateria(int idMateria)
        {
            VM_Docente resultado = new VM_Docente();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT DPM.idDocente
                                    FROM Docentes_por_Materias DPM
                                    WHERE dpm.idMateria = idMateria
                                    and DPM.Activo= 'true'";
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

                        resultado.IdDocente = int.Parse(dr["idDocente"].ToString());
                        

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