using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RubricaWeb.Models;
using RubricaWeb.ViewModels;
using System.Data;

namespace RubricaWeb.AccesoDatos
{
    public class AD_Reportes
    {
        public static string graficoReporte1(int idMateria)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);


            SqlCommand cmd = new SqlCommand();



            string consulta = @"SELECT (
                                          SELECT COUNT( epm.condicion )
                                          FROM Estudiantes_por_Materia epm
                                          WHERE epm.condicion = 0
                                          AND idMateria = @idMateria
                                         ) AS DESAPROBADOS, (
                                          SELECT COUNT( epm.condicion )
                                           FROM Estudiantes_por_Materia epm
                                          WHERE epm.condicion =1
                                          AND idMateria = @idMateria
                                        ) AS APROBADOS ;
                                     ";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idMateria", idMateria);


            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = consulta;
            cn.Open();
            cmd.Connection = cn;


            System.Data.DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            cn.Close();


            string strDatos;


            string columna1 = "Desaprobados";
            string columna2 = "Aprobados";

            strDatos = "[['Desaprobados', 'Aprobados'],";//encabezado

            foreach (DataRow dr in Datos.Rows)
            {


                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + columna1 + "'" + "," + dr[0];
                strDatos = strDatos + "],";
                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + columna2 + "'" + "," + dr[1];
                strDatos = strDatos + "]";




            }

            strDatos = strDatos + "]";

            return strDatos;
        }


        public static string GraficoCantidadTemasAdeudadosEstudiante(int idMateria)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);


            SqlCommand cmd = new SqlCommand();



            string consulta = @"select E.apellidoEstudiante+' '+E.nombreEstudiante AS 'NOMBRE' ,count(vce.nota) as 'cant'
                                from Valoracion_Criterios_Estudiantes vce
                                JOIN TEMAS T ON T.idTema=VCE.idTema
                                LEFT JOIN ESTUDIANTES E ON E.idEstudiante=VCE.idEstudiante
                                where T.idMateria=@idMateria
                                AND VCE.NOTA<7
                                group by E.apellidoEstudiante+' '+E.nombreEstudiante 
                                order by 2 desc
                                     ";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idMateria", idMateria);


            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = consulta;
            cn.Open();
            cmd.Connection = cn;


            System.Data.DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            cn.Close();


            //string strDatos;


            //string columna1 = "NOMBRE";
            //string columna2 = "CANTIDAD TEMAS ADEUDADOS";
            //string columna3 = "N3";
            //string columna4 = "N4";
            //string columna5 = "N5";
            //string columna6 = "N6";
            //string columna7 = "N7";
            //string columna8 = "N8";
            //string columna9 = "N9";
            //string columna10 = "N10";
            //string columna11 = "N11";
            //string columna12 = "N12";


            //strDatos = "[['Element', 'Density', { role: 'style' }],";//encabezado

            //foreach (DataRow dr in Datos.Rows)
            //{


            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna1 + "'" + "," + dr[1] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna2 + "'" + "," + dr[2] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna3 + "'" + "," + dr[3] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna4 + "'" + "," + dr[4] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna5 + "'" + "," + dr[5] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    ///////////////////////////// 
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna6 + "'" + "," + dr[6] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna7 + "'" + "," + dr[7] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna8 + "'" + "," + dr[8] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna9 + "'" + "," + dr[9] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna10 + "'" + "," + dr[10] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna11 + "'" + "," + dr[11] + "," + "'#b87333'";
            //    strDatos = strDatos + "],";
            //    /////////////////////////////
            //    strDatos = strDatos + "[";
            //    strDatos = strDatos + "'" + columna12 + "'" + "," + dr[12] + "," + "'#b87333'";
            //    strDatos = strDatos + "]";

            //}

            //strDatos = strDatos + "]";

            //return strDatos;


            string strDatos;

            strDatos = "[['Nombre', 'Cantidad Temas Adeudados'],";

            foreach (DataRow dr in Datos.Rows)
            {

                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + (dr[0].ToString()).Replace(",", ".") + "'" + "," + (dr[1].ToString()).Replace(",", ".");
                strDatos = strDatos + "],";


            }

            strDatos = strDatos + "]";

            return strDatos;
        }


        public static string GraficoPromediosMaterias(int idEstudiante)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);


            SqlCommand cmd = new SqlCommand();

            //Emitir listado de Materias con Promedio Final.

            string consulta = @"SELECT M.materia,

                                CAST( ROUND((AVG(VCE.nota)*2),0,0)/2 AS DECIMAL(16, 2)) AS 'promedio'

                                FROM Valoracion_Criterios_Estudiantes VCE
                                JOIN Temas T ON T.idTema=VCE.idTema
                                JOIN MATERIAS M ON M.idMateria= T.idMateria
                                WHERE vce.idEstudiante=@IdEstudiante
                                GROUP BY M.materia, t.idMateria;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdEstudiante", idEstudiante);


            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = consulta;
            cn.Open();
            cmd.Connection = cn;


            DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            cn.Close();

            string strDatos;

            strDatos = "[['Materia', 'Promedio'],";

            foreach (DataRow dr in Datos.Rows)
            {

                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + (dr[0].ToString()).Replace("," , ".") + "'" + "," + (dr[1].ToString()).Replace("," , ".");
                strDatos = strDatos + "],";


            }

            strDatos = strDatos + "]";

            return strDatos;
        }

        public static string GraficoTortaAprobadoEstudiante(int idEstudiante)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);


            SqlCommand cmd = new SqlCommand();


            //puede haber error si la materia no esta activa//
            string consulta = @"SELECT (
                                          SELECT COUNT( epm.condicion )
                                          FROM Estudiantes_por_Materia epm
                                          WHERE epm.condicion = 0
                                          AND idEstudiante = @idEstudiante
                                         ) AS DESAPROBADOS, (
                                          SELECT COUNT( epm.condicion )
                                           FROM Estudiantes_por_Materia epm
                                          WHERE epm.condicion =1
                                          AND idEstudiante = @idEstudiante
                                        ) AS APROBADOS ;
                                     ";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);


            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = consulta;
            cn.Open();
            cmd.Connection = cn;


            System.Data.DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            cn.Close();


            string strDatos;


            string columna1 = "Desaprobadas";
            string columna2 = "Aprobadas";

            strDatos = "[['Materias Desaprobadas', 'Materias Aprobadas'],";//encabezado

            foreach (DataRow dr in Datos.Rows)
            {

                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + columna1 + "'" + "," + dr[0];
                strDatos = strDatos + "],";
                strDatos = strDatos + "[";
                strDatos = strDatos + "'" + columna2 + "'" + "," + dr[1];
                strDatos = strDatos + "]";

            }

            strDatos = strDatos + "]";

            return strDatos;
        }


        public static List<VM_ReporteEstudiante> ResumenMateriasAdeudadas(VM_Estudiante modelo)
        {
            List<VM_ReporteEstudiante> resultado = new List<VM_ReporteEstudiante>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = @"SELECT M.materia, T.idNroTema, T.descripcionTema  FROM Valoracion_Criterios_Estudiantes VCE
                                    JOIN TEMAS T ON T.idTema=VCE.idTema
                                    JOIN MATERIAS M ON M.idMateria=T.idMateria
                                    where idEstudiante=@idEstudiante
                                    AND idCurso=@idCurso
                                    AND nota <7
                                    ORDER BY 1,2";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEstudiante", modelo.IdEstudiante);
                cmd.Parameters.AddWithValue("@idCurso", modelo.IdCurso);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                SqlDataReader dr = cmd.ExecuteReader();

            

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_ReporteEstudiante itemsLista = new VM_ReporteEstudiante();

                        itemsLista.materia = dr["Materia"].ToString();
                        itemsLista.descripcionTema = dr["descripcionTema"].ToString();                      
                        itemsLista.idNroTema = int.Parse(dr["idNroTema"].ToString());

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

        public static string GraficoRendimientoEstudiante(VM_Estudiante modelo)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "sp_libretaEstudiante";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //string consulta = @"EXEC sp_InsertarDocenteXMateria (@idMateria, @idDocente)";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@idEstudiante", modelo.IdEstudiante));
            cmd.Parameters.Add(new SqlParameter("@idCurso", modelo.IdCurso));

            cmd.Connection = cn;


            System.Data.DataTable Datos = new DataTable();
            Datos.Load(cmd.ExecuteReader());
            cn.Close();



            List<string> materias = new List<string>();
            foreach (DataRow dr in Datos.Rows)
            {
                materias.Add("'" + dr[0].ToString() + "'");
            }
            string encabezado = String.Join(",", materias);
            string cadena = "[['EVALUACION'," + encabezado + "],";


          
            string columna2 = "['N1',";
            string columna3 = "['N2',";
            string columna4 = "['N3',";
            string columna5 = "['N4',";
            string columna6 = "['N5',";
            string columna7 = "['N6',";
            string columna8 = "['N7',";
            string columna9 = "['N8',";
            string columna10 = "['N9',";
            string columna11 = "['N10',";
            string columna12 = "['N11',";
            string columna13 = "['N12',";
           

            List<string> linea1 = new List<string>();
            List<string> linea2 = new List<string>();
            List<string> linea3 = new List<string>();
            List<string> linea4 = new List<string>();
            List<string> linea5 = new List<string>();
            List<string> linea6 = new List<string>();
            List<string> linea7 = new List<string>();
            List<string> linea8 = new List<string>();
            List<string> linea9 = new List<string>();
            List<string> linea10 = new List<string>();
            List<string> linea11 = new List<string>();
            List<string> linea12 = new List<string>();



            foreach (DataRow dr in Datos.Rows)
            {

                linea1.Add(dr[1].ToString());
                linea2.Add(dr[2].ToString());
                linea3.Add(dr[3].ToString());
                linea4.Add(dr[4].ToString());
                linea5.Add(dr[5].ToString());
                linea6.Add(dr[6].ToString());
                linea7.Add(dr[7].ToString());
                linea8.Add(dr[8].ToString());
                linea9.Add(dr[9].ToString());
                linea10.Add(dr[10].ToString());
                linea11.Add(dr[11].ToString());
                linea12.Add(dr[12].ToString());
                ///SI FUERA STRING EL CAMPO ///
                ///
                /// 
                //linea1.Add("'" + dr[1].ToString() + "'");
                //linea2.Add("'" + dr[2].ToString() + "'");
                //linea3.Add("'" + dr[3].ToString() + "'");
                //linea4.Add("'" + dr[4].ToString() + "'");
                //linea5.Add("'" + dr[5].ToString() + "'");
                //linea6.Add("'" + dr[6].ToString() + "'");
                //linea7.Add("'" + dr[7].ToString() + "'");
                //linea8.Add("'" + dr[8].ToString() + "'");
                //linea9.Add("'" + dr[9].ToString() + "'");
                //linea10.Add("'" + dr[10].ToString() + "'");
                //linea11.Add("'" + dr[11].ToString() + "'");
                //linea12.Add("'" + dr[12].ToString() + "'");
                /// 
                ///linea12.Add("'" + dr[12].ToString() + "'");
            }

            string posicion1 = columna2 + String.Join(",", linea1) + "],";
            string posicion2 = columna3 + String.Join(",", linea2) + "],";
            string posicion3 = columna4 + String.Join(",", linea3) + "],";
            string posicion4 = columna5 + String.Join(",", linea4) + "],";
            string posicion5 = columna6 + String.Join(",", linea5) + "],";
            string posicion6 = columna7 + String.Join(",", linea6) + "],";
            string posicion7 = columna8 + String.Join(",", linea7) + "],";
            string posicion8 = columna9 + String.Join(",", linea8) + "],";
            string posicion9 = columna10 + String.Join(",", linea9) + "],";
            string posicion10 = columna11 + String.Join(",", linea10) + "],";
            string posicion11 = columna12 + String.Join(",", linea11) + "],";
            /////ULTIMA POSICION CIERRA EL CORCHETE///////
            string posicion12 = columna13 + String.Join(",", linea12) + "]]";




            cadena = cadena + posicion1 + posicion2 + posicion3 + posicion4 + posicion5 + posicion6
                     + posicion7 + posicion8 + posicion9 + posicion10 + posicion11 + posicion12;
            //cadena = cadena + strDatos + "]";

            return cadena;
        }


        public static List<VM_LibretaEstudiante> ObtenerNotasLibreta(Estudiante modelo)
        {
            List<VM_LibretaEstudiante> resultado = new List<VM_LibretaEstudiante>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();

            SqlConnection cn = new SqlConnection(cadenaConexion);

            try
            {
                cn.Open();
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = "sp_libretaEstudiante";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //string consulta = @"EXEC sp_InsertarDocenteXMateria (@idMateria, @idDocente)";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@idEstudiante", modelo.IdEstudiante));
                cmd.Parameters.Add(new SqlParameter("@idCurso", modelo.IdCurso));

                SqlDataReader dr = cmd.ExecuteReader();


                if (dr != null)
                {
                    while (dr.Read())
                    {
                        VM_LibretaEstudiante itemsLista = new VM_LibretaEstudiante();

                        itemsLista.Materia = dr["Materia"].ToString();
                        itemsLista.Curso = dr["Curso"].ToString();
                        itemsLista.Condicion = bool.Parse(dr["CONDICION"].ToString());

                        itemsLista.Nota1 = double.Parse(dr["NOTA 1"].ToString());
                        itemsLista.Nota2 = double.Parse(dr["NOTA 2"].ToString());
                        itemsLista.Nota3 = double.Parse(dr["NOTA 3"].ToString());
                        itemsLista.Nota4 = double.Parse(dr["NOTA 4"].ToString());
                        itemsLista.Nota5 = double.Parse(dr["NOTA 5"].ToString());
                        itemsLista.Nota6 = double.Parse(dr["NOTA 6"].ToString());
                        itemsLista.Nota7 = double.Parse(dr["NOTA 7"].ToString());
                        itemsLista.Nota8 = double.Parse(dr["NOTA 8"].ToString());
                        itemsLista.Nota9 = double.Parse(dr["NOTA 9"].ToString());
                        itemsLista.Nota10 = double.Parse(dr["NOTA 10"].ToString());
                        itemsLista.Nota11 = double.Parse(dr["NOTA 11"].ToString());
                        itemsLista.Nota12 = double.Parse(dr["NOTA 12"].ToString());

                        itemsLista.idMateria = int.Parse(dr["IdMateria"].ToString());

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


        public static double CalcularPromedioLibreta(VM_LibretaEstudiante estudiante, int cantidadTemas)
        {
            double resultado = 0;

            if (cantidadTemas != 0)
            {
                double suma = estudiante.Nota1 + estudiante.Nota2 + estudiante.Nota3 +
                               estudiante.Nota4 + estudiante.Nota5 + estudiante.Nota6 +
                               estudiante.Nota7 + estudiante.Nota8 + estudiante.Nota9 +
                               estudiante.Nota10 + estudiante.Nota11 + estudiante.Nota12;


                resultado = Math.Ceiling((suma / cantidadTemas) * 2) / 2;


            }


            return resultado;
        }



        public static int CantidadMateriasDesaprobadasEstudiante(Estudiante modelo)
        {
            int contador = 0;

            List<VM_LibretaEstudiante> libreta = ObtenerNotasLibreta(modelo);

            foreach (var materia in libreta)
            {
                if (!materia.Condicion)
                {
                    contador++;
                }
            }



            return contador;







        }

    }
}