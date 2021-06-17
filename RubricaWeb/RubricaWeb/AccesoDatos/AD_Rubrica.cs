using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;

namespace RubricaWeb.AccesoDatos
{
    public class AD_Rubrica
    {
        public static bool AgregarNuevoTema(VM_Tema nuevoTema)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"INSERT INTO Temas VALUES(@idMateria ,@idNroTema, @descripcionTema, @aprendizaje, 
                                    @descripcionCriterio1, 
                                    @descripcionCriterio2, 
                                    @descripcionCriterio3, 
                                    @descripcionCriterio4, 
                                    @descripcionCriterio5, 
                                    @descripcionCriterio6)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idMateria", nuevoTema.IdMateria);
                cmd.Parameters.AddWithValue("@idNroTema", nuevoTema.IdTema);
                cmd.Parameters.AddWithValue("@descripcionTema", nuevoTema.DescripcionTema);
                cmd.Parameters.AddWithValue("@aprendizaje", nuevoTema.Aprendizaje);

                if (nuevoTema.DescripcionCriterio1 is null)
                {
                    cmd.Parameters.AddWithValue("@descripcionCriterio1", DBNull.Value);

                }
                else cmd.Parameters.AddWithValue("@descripcionCriterio1", nuevoTema.DescripcionCriterio1);

                if (nuevoTema.DescripcionCriterio2 is null)
                {
                    cmd.Parameters.AddWithValue("@descripcionCriterio2", DBNull.Value); ;

                }
                else cmd.Parameters.AddWithValue("@descripcionCriterio2", nuevoTema.DescripcionCriterio2); ;

                if (nuevoTema.DescripcionCriterio3 is null)
                {
                    cmd.Parameters.AddWithValue("@descripcionCriterio3", DBNull.Value);
                }
                else cmd.Parameters.AddWithValue("@descripcionCriterio3", nuevoTema.DescripcionCriterio3);

                if (nuevoTema.DescripcionCriterio4 is null)
                {
                    cmd.Parameters.AddWithValue("@descripcionCriterio4", DBNull.Value);
                }
                else cmd.Parameters.AddWithValue("@descripcionCriterio4", nuevoTema.DescripcionCriterio4);

                if (nuevoTema.DescripcionCriterio5 is null)
                {
                    cmd.Parameters.AddWithValue("@descripcionCriterio5", DBNull.Value);
                }
                else cmd.Parameters.AddWithValue("@descripcionCriterio5", nuevoTema.DescripcionCriterio5);

                if (nuevoTema.DescripcionCriterio6 is null)
                {
                    cmd.Parameters.AddWithValue("@descripcionCriterio6", DBNull.Value);
                }
                else cmd.Parameters.AddWithValue("@descripcionCriterio6", nuevoTema.DescripcionCriterio6);





                //cmd.Parameters.AddWithValue("@descripcionCriterio1", nuevoTema.DescripcionCriterio1);
                //cmd.Parameters.AddWithValue("@descripcionCriterio2", nuevoTema.DescripcionCriterio2);
                //cmd.Parameters.AddWithValue("@descripcionCriterio3", nuevoTema.DescripcionCriterio3);
                //cmd.Parameters.AddWithValue("@descripcionCriterio4", nuevoTema.DescripcionCriterio4);
                //cmd.Parameters.AddWithValue("@descripcionCriterio5", nuevoTema.DescripcionCriterio5);
                //cmd.Parameters.AddWithValue("@descripcionCriterio6", DBNull.Value);


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

        public static List<VM_ComboValorCriterios> ComboValorCriterios()
        {
            List<VM_ComboValorCriterios> resultado = new List<VM_ComboValorCriterios>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT * FROM valorCriterio";
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

        public static List<VM_Tema> comboNumeroTema(int idMateria)
        {
            List<VM_Tema> resultado = new List<VM_Tema>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT idNroTema, numeroTema 
                                    FROM Numeros_Temas
                                    WHERE idNroTema NOT IN
                                    (SELECT t.idNroTema
                                    FROM temas t
                                    WHERE t.idMateria= @idMateria
                                    )";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idMateria", idMateria);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_Tema valor = new VM_Tema();
                        valor.IdTema = int.Parse(dr["idNroTema"].ToString());
                        valor.NumeroDeTema = dr["numeroTema"].ToString();

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



        public static List<VM_Tema> comboTemasCargados()
        {
            List<VM_Tema> resultado = new List<VM_Tema>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT t.idNroTema, NT.numeroTema
                                    FROM temas t
									JOIN Numeros_Temas NT on NT.idNroTema= T.idNroTema";
                //WHERE t.idMateria= @idMateria";
                cmd.Parameters.Clear();
                //cmd.Parameters.AddWithValue("@idMateria", idMateria);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_Tema valor = new VM_Tema();
                        valor.IdTema = int.Parse(dr["idNroTema"].ToString());
                        valor.NumeroDeTema = dr["numeroTema"].ToString();

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

        public static int CantidadCriteriosCargados(int idMateria, int nroTema)
        {
            int resultado = 0;

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT COUNT(DISTINCT descripcionCriterio1) + COUNT(DISTINCT descripcionCriterio2) + COUNT(DISTINCT descripcionCriterio3) +
                                    COUNT(DISTINCT descripcionCriterio4) + COUNT(DISTINCT descripcionCriterio5)+ COUNT(DISTINCT descripcionCriterio6)   AS 'CANTIDAD CRITERIOS'
                                    FROM Temas
                                    WHERE idMateria= @idMateria
                                    AND idNroTema= @idNroTema";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idMateria", idMateria);
                cmd.Parameters.AddWithValue("@idNroTema", nroTema);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); //ejecuta la sentencia sql

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado = int.Parse(dr["CANTIDAD CRITERIOS"].ToString());


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

        public static VM_Tema datosTemaACargar(int idMateria, int idNroTema)
        {
            VM_Tema resultado = new VM_Tema();

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT NT.numeroTema, T.descripcionTema, T.idTema,T.aprendizaje, T.descripcionCriterio1, 
									T.descripcionCriterio2,T.descripcionCriterio3 ,
									T.descripcionCriterio4,T.descripcionCriterio5,T.descripcionCriterio6,
									M.idCurso, T.idMateria, T.idNroTema
									FROM TEMAS T
									JOIN Numeros_Temas NT ON T.idNroTema=NT.idNroTema
									JOIN Materias M ON T.idMateria=M.idMateria
                                    WHERE T.idMateria=@idMateria
                                    AND T.idNroTema=@idNroTema; ";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@idMateria", idMateria);
                cmd.Parameters.AddWithValue("@idNroTema", idNroTema);



                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); //ejecuta la sentencia sql

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.NumeroDeTema = dr["numeroTema"].ToString();
                        resultado.DescripcionTema = dr["descripcionTema"].ToString();
                        resultado.IdTema = int.Parse(dr["idTema"].ToString());
                        resultado.Aprendizaje = dr["aprendizaje"].ToString();
                        resultado.Idcurso = int.Parse(dr["idCurso"].ToString());
                        resultado.IdMateria = int.Parse(dr["idMateria"].ToString());
                        resultado.IdNroTema = int.Parse(dr["idNroTema"].ToString());
                        resultado.DescripcionCriterio1 = dr["descripcionCriterio1"].ToString();
                        resultado.DescripcionCriterio2 = dr["descripcionCriterio2"].ToString();
                        resultado.DescripcionCriterio3 = dr["descripcionCriterio3"].ToString();
                        resultado.DescripcionCriterio4 = dr["descripcionCriterio4"].ToString();
                        resultado.DescripcionCriterio5 = dr["descripcionCriterio5"].ToString();
                        resultado.DescripcionCriterio6 = dr["descripcionCriterio6"].ToString();


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


        public static List<VM_ListadoRubrica> listaTemasMateria(int idMateria)
        {
            List<VM_ListadoRubrica> resultado = new List<VM_ListadoRubrica>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT T.*,NT.numeroTema, M.materia, C.nombreCurso
                                    FROM  TEMAS T
                                    JOIN Materias M ON T.idMateria=M.idMateria
                                    JOIN Cursos C ON C.idCurso=M.idCurso
                                    JOIN Numeros_Temas NT ON NT.idNroTema=T.idNroTema
                                    WHERE T.idMateria=@idMateria";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idMateria", idMateria);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_ListadoRubrica item = new VM_ListadoRubrica();
                        item.idMateria = int.Parse(dr["idMateria"].ToString());
                        item.idNroTema = int.Parse(dr["idNroTema"].ToString());
                        item.idTema = int.Parse(dr["idTema"].ToString());
                        item.descripcionTema = dr["descripcionTema"].ToString();
                        item.numeroTema = dr["numeroTema"].ToString();
                        item.aprendizaje = dr["aprendizaje"].ToString();
                        item.nombreMateria = dr["materia"].ToString();
                        item.nombreCurso = dr["nombreCurso"].ToString();



                        item.descripcionCriterio1 = dr["descripcionCriterio1"].ToString();
                        item.descripcionCriterio2 = dr["descripcionCriterio2"].ToString();
                        item.descripcionCriterio3 = dr["descripcionCriterio3"].ToString();
                        item.descripcionCriterio4 = dr["descripcionCriterio4"].ToString();
                        item.descripcionCriterio5 = dr["descripcionCriterio5"].ToString();
                        item.descripcionCriterio6 = dr["descripcionCriterio6"].ToString();



                        resultado.Add(item);
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



        public static bool AgregarTemaPorEstudiante(int idEstudiante, int idTema, int CantCriterios)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"INSERT INTO Valoracion_Criterios_Estudiantes 
                                    VALUES  (@idEstudiante,@idTema, @valoracionCriterio1, @valoracionCriterio2,
                                             @valoracionCriterio3,@valoracionCriterio4, @valoracionCriterio5,
                                             @valoracionCriterio6,@cantCriteriosEvaluados,@nota)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                cmd.Parameters.AddWithValue("@idTema", idTema);
                cmd.Parameters.AddWithValue("@valoracionCriterio1", 0);
                cmd.Parameters.AddWithValue("@valoracionCriterio2", 0);
                cmd.Parameters.AddWithValue("@valoracionCriterio3", 0);
                cmd.Parameters.AddWithValue("@valoracionCriterio4", 0);
                cmd.Parameters.AddWithValue("@valoracionCriterio5", 0);
                cmd.Parameters.AddWithValue("@valoracionCriterio6", 0);
                cmd.Parameters.AddWithValue("@cantCriteriosEvaluados", CantCriterios);
                cmd.Parameters.AddWithValue("@nota", 0);


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

        public static int ultimoIdTema()
        {
            int resultado = 0;

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT MAX(idTema) as 'id'
                                    FROM Temas ";
                cmd.Parameters.Clear();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); //ejecuta la sentencia sql

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado = int.Parse(dr["id"].ToString());


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

        public static double CalcularNota(VM_Rubrica modelo, int cantidadCriterios)
        {
            double resultado = 0;
            double total = 0;

            total = modelo.ValorCriterio1 + modelo.ValorCriterio2 + modelo.ValorCriterio3 +
                    modelo.ValorCriterio4 + modelo.ValorCriterio5 + modelo.ValorCriterio6;

            if (total != 0)

            {
                resultado = Math.Round((total / (4 * cantidadCriterios) * 10), 0, MidpointRounding.ToEven);

            }


            return resultado;
        }

        public static List<VM_Rubrica> ListadoEstudiantesParaRubrica(int idTema)
        {
            List<VM_Rubrica> resultado = new List<VM_Rubrica>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT E.apellidoEstudiante + ', ' + E.nombreEstudiante  AS 'NOMBRE ESTUDIANTE', VCE.* , T.idNroTema, t.idMateria
                                    FROM Valoracion_Criterios_Estudiantes VCE
                                    JOIN ESTUDIANTES E ON E.idEstudiante=VCE.idEstudiante
									JOIN Temas T on t.idTema=VCE.idTema
                                    WHERE VCE.idTema	= @idTema
                                    ORDER BY 1 ASC ";
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;
                cmd.Parameters.AddWithValue("@idTema", idTema);
                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_Rubrica itemsLista = new VM_Rubrica();
                        itemsLista.NombreEstudiante = dr["NOMBRE ESTUDIANTE"].ToString();
                        itemsLista.IdTema = int.Parse(dr["IdTema"].ToString());
                        itemsLista.IdNroTema = int.Parse(dr["IdNroTema"].ToString());
                        itemsLista.IdMateria = int.Parse(dr["idMateria"].ToString());
                        itemsLista.Nota = double.Parse(dr["nota"].ToString());

                        itemsLista.ValorCriterio1 = int.Parse(dr["valoracionCriterio1"].ToString());
                        itemsLista.ValorCriterio2 = int.Parse(dr["valoracionCriterio2"].ToString());
                        itemsLista.ValorCriterio3 = int.Parse(dr["valoracionCriterio3"].ToString());
                        itemsLista.ValorCriterio4 = int.Parse(dr["valoracionCriterio4"].ToString());
                        itemsLista.ValorCriterio5 = int.Parse(dr["valoracionCriterio5"].ToString());
                        itemsLista.ValorCriterio6 = int.Parse(dr["valoracionCriterio6"].ToString());


                        //itemsLista.Dni = dr["DNI"].ToString();
                        //itemsLista.Curso = dr["CURSO"].ToString();
                        //itemsLista.IdCurso = int.Parse(dr["idCurso"].ToString());
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




        public static bool GuardarValoracionTema(VM_Rubrica valoracion)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try //va a intentar realizar la accion y sino la exepcion va a realizarla
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"UPDATE Valoracion_Criterios_Estudiantes SET
                                    
                                  [valoracionCriterio1]= @valoracionCriterio1
                                  ,[valoracionCriterio2]= @valoracionCriterio2
                                  ,[valoracionCriterio3]= @valoracionCriterio3
                                  ,[valoracionCriterio4]= @valoracionCriterio4
                                  ,[valoracionCriterio5]= @valoracionCriterio5
                                  ,[valoracionCriterio6]= @valoracionCriterio6
                                  ,[nota]= @nota
                                   WHERE [idEstudiante] = @idEstudiante
                                   AND  [idTema] = @idTema";
                cmd.Parameters.Clear();//si tenia un parametro cargado, que lo limpie
                cmd.Parameters.AddWithValue("@valoracionCriterio1", valoracion.ValorCriterio1);
                cmd.Parameters.AddWithValue("@valoracionCriterio2", valoracion.ValorCriterio2);
                cmd.Parameters.AddWithValue("@valoracionCriterio3", valoracion.ValorCriterio3);
                cmd.Parameters.AddWithValue("@valoracionCriterio4", valoracion.ValorCriterio4);
                cmd.Parameters.AddWithValue("@valoracionCriterio5", valoracion.ValorCriterio5);
                cmd.Parameters.AddWithValue("@valoracionCriterio6", valoracion.ValorCriterio6);

                cmd.Parameters.AddWithValue("@nota", valoracion.Nota);

                cmd.Parameters.AddWithValue("@idEstudiante", valoracion.IdEstudiante);
                cmd.Parameters.AddWithValue("@idTema", valoracion.IdTema);


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


        public static int ContarCriteriosTemas(int idTema)
        {
            int resultado = 0;

            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT COUNT(DISTINCT descripcionCriterio1) 
                                    + COUNT(DISTINCT descripcionCriterio2)
                                    + COUNT(DISTINCT descripcionCriterio3)
                                    + COUNT(DISTINCT descripcionCriterio4)
                                    + COUNT(DISTINCT descripcionCriterio5)
                                    + COUNT(DISTINCT descripcionCriterio6) AS 'Cantidad Criterios'
                                    FROM Temas
                                    WHERE idTema=@IdTema
                                    ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idTema", idTema);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader(); //ejecuta la sentencia sql

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado = int.Parse(dr["Cantidad Criterios"].ToString());


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




        public static VM_DocenteXMateria ObtenerIdMateriaIdDocente(int idMateria)
        {
            VM_DocenteXMateria resultado = new VM_DocenteXMateria();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT M.idMateria, D.idDocente
                                    FROM Materias M
                                    JOIN Docentes_por_Materias DPM ON DPM.idMateria=M.idMateria
                                    JOIN DOCENTES D ON D.idDocente=DPM.idDocente
                                    WHERE M.idMateria=@idMateria";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idMateria", idMateria);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        resultado.idDocente = int.Parse(dr["idDocente"].ToString());
                        resultado.idMateria = int.Parse(dr["idMateria"].ToString());
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

        public static List<VM_LibretaNotasMateria> ListadoNotasPorMateria(int idMateria)
        {
            List<VM_LibretaNotasMateria> resultado = new List<VM_LibretaNotasMateria>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT E.apellidoEstudiante + ', ' + E.nombreEstudiante  AS 'NOMBRE ESTUDIANTE', VCE1.nota AS 'NOTA 1', 
                                                                        (SELECT VCE2.nota FROM Valoracion_Criterios_Estudiantes VCE2
	                                                                        JOIN TEMAS T2 ON VCE2.idTema=T2.idTema
	                                                                        WHERE VCE2.idEstudiante=VCE1.idEstudiante										
	                                                                        and T2.idNroTema=2
	                                                                        and T2.idMateria=@idMateria) as 'NOTA 2',
                                                                        (SELECT VCE3.nota FROM Valoracion_Criterios_Estudiantes VCE3
	                                                                        JOIN TEMAS T3 ON VCE3.idTema=T3.idTema
	                                                                        WHERE VCE3.idEstudiante=VCE1.idEstudiante										
	                                                                        and T3.idNroTema=3
	                                                                        and T3.idMateria=@idMateria) as 'NOTA 3',
                                                                        (SELECT VCE4.nota FROM Valoracion_Criterios_Estudiantes VCE4
	                                                                        JOIN TEMAS T4 ON VCE4.idTema=T4.idTema
	                                                                        WHERE VCE4.idEstudiante=VCE1.idEstudiante										
	                                                                        and T4.idNroTema=4
	                                                                        and T4.idMateria=@idMateria) as 'NOTA 4',
                                                                        (SELECT VCE5.nota FROM Valoracion_Criterios_Estudiantes VCE5
	                                                                        JOIN TEMAS T5 ON VCE5.idTema=T5.idTema
	                                                                        WHERE VCE5.idEstudiante=VCE1.idEstudiante										
	                                                                        and T5.idNroTema=5
	                                                                        and T5.idMateria=@idMateria) as 'NOTA 5',								
                                                                        (SELECT VCE6.nota FROM Valoracion_Criterios_Estudiantes VCE6
	                                                                        JOIN TEMAS T6 ON VCE6.idTema=T6.idTema
	                                                                        WHERE VCE6.idEstudiante=VCE1.idEstudiante
	                                                                        and T6.idNroTema=6
	                                                                        and T6.idMateria=@idMateria) as 'NOTA 6' ,
	                                t1.idMateria, T1.idTema
                                    FROM Valoracion_Criterios_Estudiantes VCE1
                                    JOIN ESTUDIANTES E ON E.idEstudiante=VCE1.idEstudiante
                                    JOIN Temas T1 on t1.idTema=VCE1.idTema
                                    WHERE T1.idNroTema	= 1
                                    and t1.idMateria=@idMateria
                                    ORDER BY T1.idNroTema ASC, 1 ASC ";
                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;
                cmd.Parameters.AddWithValue("@idMateria", idMateria);
                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_LibretaNotasMateria itemsLista = new VM_LibretaNotasMateria();
                       
                        itemsLista.NombreEstudiante = dr["NOMBRE ESTUDIANTE"].ToString();
                        itemsLista.IdTema = int.Parse(dr["IdTema"].ToString());
                        itemsLista.IdMateria = int.Parse(dr["idMateria"].ToString());

                        itemsLista.Nota1 = double.Parse(dr["NOTA 1"].ToString());
                        itemsLista.Nota2 = double.Parse(dr["NOTA 2"].ToString());
                        itemsLista.Nota3 = double.Parse(dr["NOTA 3"].ToString());
                        itemsLista.Nota4 = double.Parse(dr["NOTA 4"].ToString());
                        itemsLista.Nota5 = double.Parse(dr["NOTA 5"].ToString());
                        itemsLista.Nota6 = double.Parse(dr["NOTA 6"].ToString());



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