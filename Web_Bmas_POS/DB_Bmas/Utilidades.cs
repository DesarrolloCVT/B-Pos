using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DB_Bmas
{
    public class Utilidades
    {
        static DBML_DesaintDataContext DBDatos = new DBML_DesaintDataContext();
        //public static string NombreBD()
        //{
        //string ret = string.Empty;
        //var temp = (from c in DBDatos.Configuracion
        //            where c.Id_configuracion.Equals(1)
        //            select new { c.BD_NOMBRE }).FirstOrDefault();
        //ret = temp.BD_NOMBRE;
        //return ret;
        //}
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        ///// <summary>
        ///// Retorna Solo la estructura vacia de la consulta
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="varlist">Lista con Data de Tabla</param>
        ///// <returns></returns>
        //public static DataTable LINQToDataTableStruct<T>(IEnumerable<T> varlist)
        //{
        //    DataTable dtReturn = new DataTable();

        //    PropertyInfo[] oProps = null;

        //    if (varlist == null) return dtReturn;
        //    foreach (T rec in varlist)
        //    {
        //        if (oProps == null)
        //        {
        //            oProps = ((Type)rec.GetType()).GetProperties();
        //            foreach (PropertyInfo pi in oProps)
        //            {
        //                Type colType = pi.PropertyType;

        //                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
        //                == typeof(Nullable<>)))
        //                {
        //                    colType = colType.GetGenericArguments()[0];
        //                }

        //                dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
        //            }
        //        }

        //        //DataRow dr = dtReturn.NewRow();
        //        //foreach (PropertyInfo pi in oProps)
        //        //{
        //        //    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
        //        //    (rec, null);
        //        //}
        //        //dtReturn.Rows.Add(dr);
        //    }
        //    return dtReturn;
        //}
        public static string DataToHTML(DataTable dTOrigen)
        {
            string StringDataTable = "";
            if (dTOrigen.Rows.Count > 0)
            {
                StringDataTable = ConvertToHtmlFile(dTOrigen);
            }
            return StringDataTable;
        }

        public static string ConvertToHtmlFile(DataTable targetTable)
        {
            string myHtmlFile = "";
            if (targetTable == null)
            {
                throw new System.ArgumentNullException("targetTable");
            }

            StringBuilder myBuilder = new StringBuilder();
            myBuilder.Append("<div style='width:300; height:230; overflow:auto;'>");
            myBuilder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            myBuilder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
            myBuilder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn myColumn in targetTable.Columns)
            {
                myBuilder.Append("<td align='left' valign='top'>");
                myBuilder.Append("<b>");
                myBuilder.Append(myColumn.ColumnName);
                myBuilder.Append("</b>");
                myBuilder.Append("</td>");
            }
            myBuilder.Append("</tr>");
            foreach (DataRow myRow in targetTable.Rows)
            {
                myBuilder.Append("<tr align='left' valign='top'>");

                foreach (DataColumn myColumn in targetTable.Columns)
                {
                    myBuilder.Append("<td align='left' valign='top'>");
                    myBuilder.Append(myRow[myColumn.ColumnName].ToString());
                    myBuilder.Append("</td>");
                }

                myBuilder.Append("</tr>");
            }
            myBuilder.Append("</table>");
            myBuilder.Append("</div>");
            myHtmlFile = myBuilder.ToString();
            return myHtmlFile;
        }

        /// <summary>
        /// Retorna desde un HTML solo el texto
        /// </summary>
        /// <param name="source">String con el texto en formato HTML</param>
        /// <returns></returns>
        public static string StripHTML(string source)
        {
            try
            {
                string result;

                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                //result = System.Text.RegularExpressions.Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, </p>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<( )*/p([^>])*>", "\r\r", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @" ", " ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&bull;", " * ",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lsaquo;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&rsaquo;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&trade;", "(tm)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&frasl;", "/",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&lt;", "<",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&gt;", ">",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&copy;", "(c)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&reg;", "(r)",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // for testing
                //System.Text.RegularExpressions.Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                string breaks = "\r\r\r";
                // Initial replacement target string for tabs
                string tabs = "\t\t\t\t\t";
                for (int index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                // That's it.
                return result;
            }
            catch
            {
                return source;
            }
        }

        public static string DataTableToString(DataTable dt)
        {
            StringBuilder StringtoReturn = new StringBuilder();

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            StringtoReturn.Append(dt.Rows[i][j].ToString().Replace(";", " ") + ";");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            StringtoReturn.Append(dt.Rows[i][j].ToString().Replace(";", " ") + ";");
                        }

                    }
                }

                return StringtoReturn.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene una estructura tipada de datos vacia
        /// </summary>
        /// <param name="table">Incluir el objeto linq así como en el ejemplo typeof(Trucks)</param>
        /// <returns></returns>
        public static DataTable LINQToDataTable(Type table)
        {
            DataTable Dtret = new DataTable();
            var lista = (from p in table.GetProperties()
                         let columnAttribute = p.GetCustomAttributes(false).
                             OfType<System.Data.Linq.Mapping.ColumnAttribute>().SingleOrDefault()
                         where columnAttribute != null
                         select p);
            foreach (var rs in lista)
            {
                try
                {
                    Dtret.Columns.Add(new DataColumn(rs.Name, System.Type.GetType(rs.PropertyType.FullName)));
                }
                catch
                {
                    Dtret.Columns.Add(new DataColumn(rs.Name, System.Type.GetType("System.String")));
                }

            }
            return Dtret;
        }

        public static string GetEnumDescription(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes
                  (typeof(DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
    public class Validaciones
    {
        public enum TipoDeDatoaValidar { Entero = 1, Decimal, Fecha, NombrePropio, Alfanumerico, Mail, Otro, Texto, IP };
        public DataTable getValidacionesList()
        {
            DataTable Rt = new DataTable("Tipo");
            try
            {
                Rt.Columns.Add("ID");
                Rt.Columns.Add("Name");
                int i = 1;
                foreach (object item in Enum.GetValues(typeof(Validaciones.TipoDeDatoaValidar)))
                {
                    DataRow dr = Rt.NewRow();
                    dr["ID"] = i;
                    dr["Name"] = Utilidades.GetEnumDescription((Validaciones.TipoDeDatoaValidar)item);
                    Rt.Rows.Add(dr);
                    i++;
                }
                //DataRow dr2 = Rt.NewRow();
                //dr2["ID"] = 0;
                //dr2["Name"] = "Seleccione";
                //Rt.Rows.Add(dr2);
            }
            catch
            {
            }
            DataView dw = new DataView(Rt);
            dw.Sort = "Name";
            Rt = dw.ToTable();
            return Rt;
        }
        /// <summary>
        /// Valida número positivo entero
        /// </summary>
        /// <param name="strNumber">Número como string</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsNaturalNumber(String strNumber)
        {
            strNumber = strNumber.Trim();
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
            objNaturalPattern.IsMatch(strNumber);
        }

        /// <summary>
        /// Valida Positivo Entero hasta con cero
        /// </summary>
        /// <param name="strNumber">Número como string</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsWholeNumber(String strNumber)
        {
            strNumber = strNumber.Trim();
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }

        /// <summary>
        /// Valida número entero positivo o negativo
        /// </summary>
        /// <param name="strNumber">Número como string</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsInteger(String strNumber)
        {
            strNumber = strNumber.Trim();
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }

        /// <summary>
        /// Valida número positivo entero o real
        /// </summary>
        /// <param name="strNumber">Número como string</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsPositiveNumber(String strNumber)
        {
            strNumber = strNumber.Trim();
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
            objPositivePattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber);
        }

        /// <summary>
        /// Valida si es numerico con dos decimales
        /// <param name="strNumber">Número como string</param>
        /// <returns>Retorna true si supera la validación</returns>
        /// </summary>
        public static bool IsDecimal(String strNumber)
        {
            strNumber = strNumber.Trim();
            Regex objDecimalPattern = new Regex("[0-9]+(,[0-9][0-9]?)?");
            return objDecimalPattern.IsMatch(strNumber);
        }

        /// <summary>
        /// Valida si es un número valido o no
        /// </summary>
        /// <param name="strNumber">Número como string</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsNumber(String strNumber)
        {
            strNumber = strNumber.Trim();
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }

        // Function To test for Alphabets. 
        /// <summary>
        /// Valida que solo sean letras
        /// </summary>
        /// <param name="strToCheck">Cadena</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsAlpha(String strToCheck)
        {
            strToCheck = strToCheck.Trim();
            Regex objAlphaPattern = new Regex("[^a-zA-ZáéíóúÁÉÍÓÚÑ ñ]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }
        /// <summary>
        /// Valida que sea alfanumerico
        /// </summary>
        /// <param name="strToCheck">Cadena</param>
        /// <returns>Retorna true si supera la validación</returns>

        public static bool IsAlphaNumeric(String strToCheck)
        {
            strToCheck = strToCheck.Trim();
            //   Regex objAlphaNumericPattern = new Regex("[^a-zA-ZáéíóúÁÉÍÓÚÑ ñ0-9]");
            Regex objAlphaNumericPattern = new Regex("[^áéíóúÁÉÍÓÚÑ ña-zA-Z0-9]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }


        /// <summary>
        /// Valida que sea e-mail
        /// </summary>
        /// <param name="strToCheck">Cadena</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsMail(String strToCheck)
        {
            strToCheck = strToCheck.Trim();
            Regex objMailPattern = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
            return objMailPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Valida que sea una URL Valida
        /// </summary>
        /// <param name="strToCheck">Cadena</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsURL(String strToCheck)
        {
            strToCheck = strToCheck.Trim();
            Regex objAlphaNumericPattern = new Regex(@"((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Valida que sea nombre propio
        /// </summary>
        /// <param name="strToCheck">Cadena</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsNombrePropio(String strToCheck)
        {
            strToCheck = strToCheck.Trim();
            Regex objAlphaNumericPattern = new Regex(@"^[a-zA-Z áéíóúÁÉÍÓÚÑñ]+$");
            return objAlphaNumericPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Valida que sea nombre Empresa
        /// </summary>
        /// <param name="strToCheck">Cadena</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsNombreEmpresa(String strToCheck)
        {
            strToCheck = strToCheck.Trim();
            Regex objAlphaNumericPattern = new Regex(@"^[a-zA-Z áéíóúÁÉÍÓÚÑñ.]+$");
            return objAlphaNumericPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Valida que sea Fecha Valida
        /// </summary>
        /// <param name="strToCheck">Cadena</param>
        /// <returns>Retorna true si supera la validación</returns>
        public static bool IsDate(String strToCheck)
        {
            strToCheck = strToCheck.Trim();
            Regex objAlphaNumericPattern = new Regex(@"^([0]?[1-9]|[1|2][0-9]|[3][0|1])[./-]([0]?[1-9]|[1][0-2])[./-]([0-9]{4}|[0-9]{2})$");
            return objAlphaNumericPattern.IsMatch(strToCheck);
        }

        /// <summary>
        /// Obtiene el largo de los campos de un objeto LINQ o 0 si no lo encunetra
        /// </summary>
        /// <remarks>
        /// </remarks>
        public static int GetLengthLimit(object obj, string field)
        {
            int dblenint = 0;   // valor por defecto si no encuentra el largo

            Type type = obj.GetType();
            PropertyInfo prop = type.GetProperty(field);

            object[] info = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
            // Assume there is just one
            if (info.Length == 1)
            {
                ColumnAttribute ca = (ColumnAttribute)info[0];
                string dbtype = ca.DbType;

                if (dbtype.StartsWith("NChar") || dbtype.StartsWith("NVarChar") || dbtype.StartsWith("VarChar"))
                {
                    int index1 = dbtype.IndexOf("(");
                    int index2 = dbtype.IndexOf(")");
                    string dblen = dbtype.Substring(index1 + 1, index2 - index1 - 1);
                    int.TryParse(dblen, out dblenint);
                }
            }
            return dblenint;
        }

        public static void SetAutoTruncate(object obj, string field, string value)
        {
            int len = GetLengthLimit(obj, field);
            if (len == 0) throw new ApplicationException("Campo '" + field + "'no tiene el dato del largo.");

            Type type = obj.GetType();
            PropertyInfo prop = type.GetProperty(field);
            if (value.Length > len)
            {
                prop.SetValue(obj, value.Substring(0, len), null);
            }
            else
                prop.SetValue(obj, value, null);
        }

        public static bool GetCanBeNull(object obj, string field)
        {
            Type type = obj.GetType();
            PropertyInfo prop = type.GetProperty(field.Trim());
            object[] info = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
            ColumnAttribute ca = (ColumnAttribute)info[0];
            return ca.CanBeNull;
        }

        public static string GetjsFuncition(String strToCheck)
        {
            System.Text.StringBuilder js = new System.Text.StringBuilder();
            if (strToCheck == "Rut_Chile")
            {
                js.Append("function(s, e) { \n");
                js.Append("var rut = s.GetValue(); suma = 0; mul = 2; i = 0; \n");
                js.Append("if ( rut && rut.length > 0) { \n");
                js.Append("for (i = rut.length - 3; i >= 0; i--) { \n");
                js.Append("suma = suma + parseInt(rut.charAt(i)) * mul; \n");
                js.Append("mul = mul == 7 ? 2 : mul + 1; \n");
                js.Append("} \n");
                js.Append("var dvr = '' + (11 - suma % 11); \n");
                js.Append("if (dvr == '10') dvr = 'K'; else if (dvr == '11') dvr = '0'; \n");
                js.Append("if (rut.charAt(rut.length - 2) != \"-\" || rut.charAt(rut.length - 1).toUpperCase() != dvr){ \n");
                js.Append("e.isValid = false; \n");
                js.Append("e.errorText=\"Rut inválido\"; \n");
                js.Append("} else { \n");
                js.Append("e.isValid = true; \n");
                js.Append("} \n");
                js.Append("} \n");
                js.Append("else{e.isValid = false;} \n");
                js.Append("} \n");

            }
            if (strToCheck == "SSCC")
            {
                js.Append("function(s, e) \n");
                js.Append("{ \n");
                js.Append("if(s.GetValue() != '' && s.GetValue!=null) {\n");
                js.Append("var sscc = s.GetValue(); suma = 0; mul = 3; i = 0; \n");
                js.Append("var dvr; \n");
                js.Append("if  ( sscc && sscc.length == 18 && sscc!=null)  \n");
                js.Append("{ \n");
                js.Append("for (i = sscc.length - 2; i >= 0; i--) { \n");
                js.Append("mul = i % 2 == 0 ? 3 : 1; \n");
                js.Append("suma = suma + parseInt(sscc.charAt(i)) * mul; \n");
                js.Append("} \n");
                js.Append("dvr = 10 - (suma % 10); \n");
                js.Append("dvr = dvr == 10 ? 0 : dvr; \n");
                js.Append("if (sscc.charAt(sscc.length - 1) != dvr) \n");
                js.Append("{ \n");
                js.Append("e.isValid = false; \n");
                js.Append("e.errorText=\"SSCC inválido\"; \n");
                js.Append("} \n");
                js.Append(" else \n");
                js.Append("{ \n");
                js.Append("e.isValid = true; \n");
                js.Append("} \n");
                js.Append("} \n");
                js.Append("else{ if( sscc &&  sscc.length > 0 ) { e.isValid = false;}} \n");
                js.Append("} \n");
                js.Append("} \n");
            }

            if (strToCheck == "GTIN13")
            {
                js.Append("function(s, e) \n");
                js.Append("{ \n");
                js.Append("var sscc = s.GetValue(); suma = 0; mul = 3; i = 0; \n");
                js.Append("if(sscc !=null) {\n");
                js.Append("if  ( sscc && sscc.length == 13)  \n");
                js.Append("{ \n");
                js.Append("for (i = sscc.length - 2; i >= 0; i--) { \n");
                js.Append("mul = i % 2 == 0 ? 1 : 3; \n");
                js.Append("suma = suma + parseInt(sscc.charAt(i)) * mul; \n");
                js.Append("} \n");
                js.Append("var dvr = 10 - (suma % 10); \n");
                js.Append("dvr = dvr == 10 ? 0 : dvr; \n");
                js.Append("if (sscc.charAt(sscc.length - 1) != dvr) \n");
                js.Append("{ \n");
                js.Append("e.isValid = false; \n");
                js.Append("e.errorText=\"GTIN13 inválido\"; \n");
                js.Append("} \n");
                js.Append(" else \n");
                js.Append("{ \n");
                js.Append("e.isValid = true; \n");
                js.Append("} \n");
                js.Append("} \n");
                js.Append("else{ if( sscc.length > 0 ) { e.isValid = false;}} \n");
                js.Append("} \n");
                js.Append("} \n");
            }



            if (strToCheck == "GTIN14")
            {
                js.Append("function(s, e) \n");
                js.Append("{ \n");
                js.Append("var sscc = s.GetValue(); suma = 0; mul = 3; i = 0; \n");
                js.Append("if(sscc !=null) {\n");
                js.Append("if  ( sscc && sscc.length == 14)  \n");
                js.Append("{ \n");
                js.Append("for (i = sscc.length - 2; i >= 0; i--) { \n");
                js.Append("mul = i % 2 == 0 ? 3 : 1; \n");
                js.Append("suma = suma + parseInt(sscc.charAt(i)) * mul; \n");
                js.Append("} \n");
                js.Append("var dvr = 10 - (suma % 10); \n");
                js.Append("dvr = dvr == 10 ? 0 : dvr; \n");
                js.Append("if (sscc.charAt(sscc.length - 1) != dvr) \n");
                js.Append("{ \n");
                js.Append("e.isValid = false; \n");
                js.Append("e.errorText=\"GTIN14 inválido\"; \n");
                js.Append("} \n");
                js.Append(" else \n");
                js.Append("{ \n");
                js.Append("e.isValid = true; \n");
                js.Append("} \n");
                js.Append("} \n");
                js.Append("else{ if( sscc.length > 0 ) { e.isValid = false;}} \n");
                js.Append("} \n");
                js.Append("} \n");
            }



            if (strToCheck == "Container")
            {

                js.Append(" function validar_container(s, e) {\n");
                js.Append("var contenedor = s.GetValue(); suma = 0; mul = 2; i = 0; valnum = 0;\n");
                js.Append("if  ( contenedor && contenedor.length > 0)  \n");
                js.Append("{ \n");
                js.Append("for (i = contenedor.length - 3; i >= 0; i--) {\n");
                js.Append("switch (contenedor.charAt(i)) \n");
                js.Append("{\n");
                js.Append("  case '0':\n");
                js.Append("       valnum = 0;\n");
                js.Append("       break;\n");
                js.Append("  case '1':\n");
                js.Append("       valnum = 1;\n");
                js.Append("       break;\n");
                js.Append("  case '2':\n");
                js.Append("       valnum = 2;\n");
                js.Append("       break;\n");
                js.Append("  case '3':\n");
                js.Append("       valnum = 3;\n");
                js.Append("       break;\n");
                js.Append("  case '4':\n");
                js.Append("       valnum = 4;\n");
                js.Append("       break;\n");
                js.Append("  case '5':\n");
                js.Append("       valnum = 5;\n");
                js.Append("       break;\n");
                js.Append("  case '6':\n");
                js.Append("       valnum = 6;\n");
                js.Append("       break;\n");
                js.Append("  case '7':\n");
                js.Append("       valnum = 7;\n");
                js.Append("       break;\n");
                js.Append("  case '8':\n");
                js.Append("       valnum = 8;\n");
                js.Append("       break;\n");
                js.Append("  case '9':\n");
                js.Append("       valnum = 9;\n");
                js.Append("       break;\n");
                js.Append("  case 'A':\n");
                js.Append("       valnum = 10;\n");
                js.Append("       break;\n");
                js.Append("  case 'B':\n");
                js.Append("       valnum = 12;\n");
                js.Append("       break;\n");
                js.Append("  case 'C':\n");
                js.Append("       valnum = 13;\n");
                js.Append("       break;\n");
                js.Append("  case 'D':\n");
                js.Append("       valnum = 14;\n");
                js.Append("       break;\n");
                js.Append("  case 'E':\n");
                js.Append("       valnum = 15;\n");
                js.Append("       break;\n");
                js.Append("  case 'F':\n");
                js.Append("       valnum = 16;\n");
                js.Append("       break;\n");
                js.Append("  case 'G':\n");
                js.Append("       valnum = 17;\n");
                js.Append("       break;\n");
                js.Append("  case 'H':\n");
                js.Append("       valnum = 18;\n");
                js.Append("       break;\n");
                js.Append("  case 'I':\n");
                js.Append("       valnum = 19;\n");
                js.Append("       break;\n");
                js.Append("  case 'J':\n");
                js.Append("       valnum = 20;\n");
                js.Append("       break;\n");
                js.Append("  case 'K':\n");
                js.Append("       valnum = 21;\n");
                js.Append("       break;\n");
                js.Append("  case 'L':\n");
                js.Append("       valnum = 23;\n");
                js.Append("       break;\n");
                js.Append("  case 'M':\n");
                js.Append("       valnum = 24;\n");
                js.Append("       break;\n");
                js.Append("  case 'N':\n");
                js.Append("       valnum = 25;\n");
                js.Append("       break;\n");
                js.Append("  case 'O':\n");
                js.Append("       valnum = 26;\n");
                js.Append("       break;\n");
                js.Append("  case 'P':\n");
                js.Append("       valnum = 27;\n");
                js.Append("       break;\n");
                js.Append("  case 'Q':\n");
                js.Append("       valnum = 28;\n");
                js.Append("       break;\n");
                js.Append("  case 'R':\n");
                js.Append("       valnum = 29;\n");
                js.Append("       break;\n");
                js.Append("  case 'S':\n");
                js.Append("       valnum = 30;\n");
                js.Append("       break;\n");
                js.Append("  case 'T':\n");
                js.Append("       valnum = 31;\n");
                js.Append("       break;\n");
                js.Append("  case 'U':\n");
                js.Append("       valnum = 32;\n");
                js.Append("       break;\n");
                js.Append("  case 'V':\n");
                js.Append("       valnum = 34;\n");
                js.Append("       break;\n");
                js.Append("  case 'W':\n");
                js.Append("       valnum = 35;\n");
                js.Append("       break;\n");
                js.Append("  case 'X':\n");
                js.Append("       valnum = 36;\n");
                js.Append("       break;\n");
                js.Append("  case 'Y':\n");
                js.Append("       valnum = 37;\n");
                js.Append("       break;\n");
                js.Append("  case 'Z':\n");
                js.Append("       valnum = 38;\n");
                js.Append("       break;\n");
                js.Append("  }\n");
                js.Append("   suma = suma + valnum * Math.pow(2, i)\n");
                js.Append("  }\n");
                js.Append("  var dvr = '' + (suma % 11);\n");
                js.Append("  if (dvr == '10') dvr = '0';\n");
                js.Append(" if (contenedor.charAt(contenedor.length - 2) !=  \"-\" || contenedor.charAt(contenedor.length - 1).toUpperCase() != dvr)\n");
                js.Append("  e.isValid = false;\n");
                js.Append("  else\n");
                js.Append("      e.isValid = true;\n");
                js.Append("   }\n");
                js.Append("   }\n");
            }


            if (strToCheck == "DNI_España")
            {
                js.Append("function nif(s,e) {\n");

                js.Append("numero = s.substr(0,s.length-1);\n");
                js.Append("let = s.substr(dni.length-1,1);\n");
                js.Append("numero = numero % 23;\n");
                js.Append("letra='TRWAGMYFPDXBNJZSQVHLCKET';\n");
                js.Append("letra=letra.substring(numero,numero+1);\n");
                js.Append("if (letra!=let) {\n");
                js.Append(" e.isValid = false;\n");
                js.Append("}else{\n");
                js.Append("e.isValid = true;\n");
                js.Append("}\n");
                js.Append("}\n");
            }

            return js.ToString();
        }

        /// <summary>
        /// Valida Número SSCC
        /// </summary>
        /// <param name="sscc">SSCC Capturado sin identificador de aplicacion</param>
        /// <returns></returns>
        public static bool ValidSSCC(string sscc)
        {
            bool Valido = false;
            int suma = 0;
            int mul = 3;
            int i = 0;
            int dvr = 0;
            #region Validacion
            if (sscc != null && sscc.Length == 18)
            {
                #region For
                for (i = sscc.Length - 2; i >= 0; i--)
                {
                    mul = i % 2 == 0 ? 3 : 1;
                    suma = suma + Convert.ToInt32(sscc.Substring(i, 1)) * mul;
                }
                #endregion
                #region Asinga digito verificador
                dvr = 10 - (suma % 10);
                dvr = dvr == 10 ? 0 : dvr;
                #endregion
                #region Valido
                if (Convert.ToInt32(sscc.Substring(sscc.Length - 1)) != dvr)
                { Valido = false; }
                else
                { Valido = true; }
                #endregion
            }
            else
            {
                if (sscc.Length > 0)
                { Valido = false; }
                else
                { Valido = true; }
            }
            #endregion
            return Valido;
        }

        /// <summary>
        /// Entrega Checksum o digito verificador del SSCC
        /// </summary>
        /// <param name="sscc"></param>
        /// <returns></returns>
        public static string CheckSumSSCC(string sscc)
        {
            int suma = 0;
            int mul = 3;
            int i = 0;
            string dvr = "";
            #region Calculo

            if (sscc != null && sscc.Length > 0)
            {
                #region For
                for (i = sscc.Length - 1; i >= 0; i--)
                {
                    mul = i % 2 == 0 ? 3 : 1;
                    suma = suma + Convert.ToInt32(sscc.Substring(i, 1)) * mul;
                }
                #endregion
                #region Asinga digito verificador
                dvr = (10 - (suma % 10)).ToString();
                dvr = dvr == "10" ? "0" : dvr;
                #endregion

            }
            #endregion
            return dvr;
        }






        /// <summary>
        /// Estructura para recibir datos de Código GS1 128 con identificadores de aplicacion
        /// </summary>
        public struct GruposGS1_128
        {
            public string CodigoApp;
            public string DatoCod;
        }

        /// <summary>
        /// Clase para trabajo con códigos de barra
        /// </summary>
        public static class ValidaCodigoBarra
        {
            /// <summary>
            /// Retorna un código GS1 128 con los identificadores de aplicación y su valor
            /// </summary>
            /// <param name="strCodigo"></param>
            /// <returns></returns>
            public static GruposGS1_128[] getGruposGS1_128(String strCodigo)
            {
                GruposGS1_128[] Ret = new GruposGS1_128[0];
                if (ValidSSCC(strCodigo))
                {
                    int Contador = 0;
                    Match M;
                    string RegExpre = @"\x28([0-9]|[0-9][0-9]|[0-9][0-9][0-9])\x29([)^]{0}[áéíóúÁÉÍÓÚÑñ!""#$%&/=?¡¿a-zA-Z0-9]*)";
                    strCodigo = strCodigo.Trim();
                    Regex RegBusca = new Regex(RegExpre);
                    M = RegBusca.Match(strCodigo);
                    if (M.Groups.Count > 0) Array.Resize(ref Ret, M.Groups.Count);
                    while (M.Success)
                    {
                        Ret[Contador].CodigoApp = M.Groups[1].Value;
                        Ret[Contador].DatoCod = M.Groups[2].Value;
                        M = M.NextMatch();
                        Contador += 1;
                    }
                }
                return Ret;
            }
        }
    }
    public class Comunicaciones
    {
        /// <summary>
        /// Envía un PDF con el PNC Indicado
        /// </summary>
        /// <param name="PNCID">Código del PNC a Enviar</param>
        /// <returns></returns>
        public Boolean SendPNC(int PNCID)
        {
            Boolean Ret = false;
            try
            {
            }
            catch
            {
            }
            return Ret;
        }
    }
    public class ConvertEnum
    {
        public static DataTable ConvertEnumToDataTable(Type enumObj)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            int i = 1;
            foreach (string s in Enum.GetNames(enumObj))
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = i;
                dr["Name"] = s.ToString();
                dt.Rows.Add(dr);
                i++;
            }
            dt.DefaultView.Sort = "Name ASC";
            return dt;
        }
    }
}
