using Newtonsoft.Json;
using PCM.MacExpress.Presentation.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace PCM.MacExpress.Presentation.Core.Infraestructura
{
    public class WebBaseController : Controller
    {
        public void MensajeServer(string strMensaje)
        {
            TempData["_Alerta"] = strMensaje;
        }

        public int Dame_Entero(Object xobjvalue)
        {
            if (xobjvalue == System.DBNull.Value)
            {
                return 0;
            }
            else if (xobjvalue == null)
            {
                return 0;
            }
            else if (xobjvalue.ToString().Trim() == string.Empty)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(xobjvalue);
            }
        }

        public string Dame_Texto(Object xobjvalue)
        {
            if (xobjvalue == System.DBNull.Value)
            {
                return string.Empty;
            }
            else if (xobjvalue == null)
            {
                return string.Empty;
            }
            else if (xobjvalue.ToString().Trim() == string.Empty)
            {
                return string.Empty;
            }
            else
            {
                return Convert.ToString(xobjvalue);
            }
        }

        public string DesencadenarPalabra(string PALABRA) {
            if (!(string.IsNullOrEmpty(PALABRA)))
            {
                int cont = PALABRA.IndexOf('│');
                while (cont > 0)
                {
                    PALABRA = PALABRA.Replace('│', ' ');
                    cont = PALABRA.IndexOf('│');
                }
            }

            return PALABRA;
        }

        public string GetJSON(string conrolador)//servicio
        { 
            var rutaBase = ConfigurationManager.AppSettings["rutaBase"];
            //------------------------------------GET--------------------------------
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(rutaBase + conrolador);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            httpWebRequest.Method = "GET";
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Encoding enCoding = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), enCoding);
            string sResponse = streamReader.ReadToEnd();
            httpWebResponse.Close();
            Console.WriteLine(sResponse);
            //--------------------------------------------------------------------
            return sResponse;
        }

        public List<BEEntidad> RazonSocialSunat(string controlador) /*controlador=RazonSocial?RSocial=*/
        {
            List<BEEntidad> LstBEEntidad = new List<BEEntidad>();
            String resource = "https://ws3.pide.gob.pe/Rest/Sunat/" + controlador;
            WebRequest req = WebRequest.Create(@resource);
            req.Method = "GET";

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(reader);
                    XmlNodeList multiRef = xmlDoc.GetElementsByTagName("multiRef");

                    LstBEEntidad = new List<BEEntidad>();
                    if (multiRef.Count > 0)
                    {
                        for (var i = 0; i < multiRef.Count; i++)
                        {
                            LstBEEntidad.Add(new BEEntidad() { NOMBRE= multiRef[i].ChildNodes[14].InnerText.Trim() , RUC = multiRef[i].ChildNodes[19].InnerText.Trim(), COD_UBIGEO = multiRef[i].ChildNodes[27].InnerText.Trim() });
                        }
                    }
                }
            }
            return LstBEEntidad;
        }

        public BECiudadano DniReniec(string numeroDocumento) /*controlador=RazonSocial?RSocial=*/
        {
            BECiudadano oBECiudadano = new BECiudadano();

            String resource = "https://ws5.pide.gob.pe/Rest/Reniec/Consultar?" + "nuDniConsulta=" + numeroDocumento
                           + "&nuDniUsuario=" + "41364207"
                           + "&nuRucUsuario=" + "20168999926"
                           + "&password=" + "41364207";
            //32908159
            //41364207
            WebRequest req = WebRequest.Create(@resource);
            req.Method = "GET";
            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;

            if (resp.StatusCode == HttpStatusCode.OK)
            {
                using (Stream respStream = resp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream, Encoding.UTF8);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(reader);
                    XmlNodeList persona = xmlDoc.GetElementsByTagName("datosPersona");

                    if (persona.Count > 0)
                    {
                        oBECiudadano.NOMBRE = persona[0].ChildNodes[5].InnerText;
                        oBECiudadano.APELLIDO_PATERNO = persona[0].ChildNodes[0].InnerText;
                        oBECiudadano.APELLIDO_MATERNO = persona[0].ChildNodes[1].InnerText;
                        oBECiudadano.FOTO = persona[0].ChildNodes[4].InnerText;
                        oBECiudadano.DNI = numeroDocumento;
                    }
                }
            }
            return oBECiudadano;
        }

        public string PostJSON(string conrolador, byte[] postBytes)
        {
            var rutaBase = ConfigurationManager.AppSettings["rutaBase"];
            //------------------------------------POST--------------------------------
            //byte[] postBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(oBEServicio));

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(rutaBase + conrolador);
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json; charset=UTF-8";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentLength = postBytes.Length;

            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Encoding enCoding = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), enCoding);

            string sResponse = streamReader.ReadToEnd();
            httpWebResponse.Close();
            Console.WriteLine(sResponse);
            //--------------------------------------------------------------------
            return sResponse;
        }

        public string ObtenerReporte(List<BEAtencion> data)
        {
            string quote = "\"";
            StringBuilder sb = new StringBuilder();

            string Cabecera = string.Concat("<?xml version=", quote, "1.0", quote, "?>", ControlChars.NewLine,
                                       "<?mso-application progid=", quote, "Excel.Sheet", quote, "?>", ControlChars.NewLine,
                                       "<Workbook", ControlChars.NewLine, ControlChars.Tab, ControlChars.Tab,
                                       "xmlns = ", quote, "urn:schemas-microsoft-com:office:spreadsheet", quote, " ", ControlChars.NewLine, ControlChars.Tab, ControlChars.Tab,
                                       "xmlns:o=", quote, "urn:schemas-microsoft-com:office:office", quote, ControlChars.NewLine, ControlChars.Tab, ControlChars.Tab,
                                       "xmlns:x=", quote, "urn:schemas-microsoft-com:office:excel", quote, ControlChars.NewLine, ControlChars.Tab, ControlChars.Tab,
                                       "xmlns:ss=", quote, "urn:schemas-microsoft-com:office:spreadsheet", quote, ControlChars.NewLine, ControlChars.Tab, ControlChars.Tab,
                                       "xmlns:html=", quote, "http://www.w3.org/TR/REC-html40", quote, ">", ControlChars.NewLine,
                                       "<Styles>", ControlChars.NewLine, ControlChars.Tab,
                                       "<Style ss:ID=", quote, "Header", quote, " ss:Name=", quote, "Normal", quote, ">", ControlChars.NewLine, ControlChars.Tab, ControlChars.Tab,
                                       "<Font ss:Bold=", quote, "1", quote, "/>", ControlChars.NewLine, ControlChars.Tab, ControlChars.Tab,
                                       "</Style>", ControlChars.NewLine,
                                       "</Styles>", ControlChars.NewLine);

            sb.Append(Cabecera);

            int CantidadTablas = 1;
            for(int index=0; index< CantidadTablas; index++) {
                sb.Append("<Worksheet ss:Name=" + quote + "AtencionesPorSede" + quote + ">" + ControlChars.NewLine + ControlChars.Tab);
                
                sb.Append("<Table>" + ControlChars.NewLine + ControlChars.Tab + ControlChars.Tab);
                
                sb.Append("<Row>" + ControlChars.NewLine + ControlChars.Tab);
                sb.Append("<Cell ss:StyleID=" + quote + "Header" + quote + "><Data ss:Type=" + quote + "String" + quote + ">Municipalidad</Data></Cell>" + ControlChars.NewLine);
                sb.Append("<Cell ss:StyleID=" + quote + "Header" + quote + "><Data ss:Type=" + quote + "String" + quote + ">Sede</Data></Cell>" + ControlChars.NewLine);
                sb.Append("<Cell ss:StyleID=" + quote + "Header" + quote + "><Data ss:Type=" + quote + "String" + quote + ">Nombre</Data></Cell>" + ControlChars.NewLine);
                sb.Append("<Cell ss:StyleID=" + quote + "Header" + quote + "><Data ss:Type=" + quote + "String" + quote + ">DNI</Data></Cell>" + ControlChars.NewLine);
                sb.Append("<Cell ss:StyleID=" + quote + "Header" + quote + "><Data ss:Type=" + quote + "String" + quote + ">Telefono</Data></Cell>" + ControlChars.NewLine);
                sb.Append("<Cell ss:StyleID=" + quote + "Header" + quote + "><Data ss:Type=" + quote + "String" + quote + ">Servicio</Data></Cell>" + ControlChars.NewLine);
                sb.Append("<Cell ss:StyleID=" + quote + "Header" + quote + "><Data ss:Type=" + quote + "String" + quote + ">Hora Inicio</Data></Cell>" + ControlChars.NewLine);
                sb.Append("<Cell ss:StyleID=" + quote + "Header" + quote + "><Data ss:Type=" + quote + "String" + quote + ">Hora Fin</Data></Cell>" + ControlChars.NewLine);
                sb.Append("</Row>" + ControlChars.NewLine);

                foreach(var item in data) 
                {
                    sb.Append("<Row>" + ControlChars.NewLine);
                    sb.Append("<Cell><Data ss:Type=" + quote + "String" + quote + ">" + item.MUNICIPALIDAD + "</Data></Cell>" + ControlChars.NewLine);
                    sb.Append("<Cell><Data ss:Type=" + quote + "String" + quote + ">" + item.SEDE + "</Data></Cell>" + ControlChars.NewLine);
                    sb.Append("<Cell><Data ss:Type=" + quote + "String" + quote + ">" + item.NOMBRE + "</Data></Cell>" + ControlChars.NewLine);
                    sb.Append("<Cell><Data ss:Type=" + quote + "String" + quote + ">" + item.DNI + "</Data></Cell>" + ControlChars.NewLine);
                    sb.Append("<Cell><Data ss:Type=" + quote + "String" + quote + ">" + item.TELEFONO + "</Data></Cell>" + ControlChars.NewLine);
                    sb.Append("<Cell><Data ss:Type=" + quote + "String" + quote + ">" + item.SERVICIO + "</Data></Cell>" + ControlChars.NewLine);
                    sb.Append("<Cell><Data ss:Type=" + quote + "String" + quote + ">" + item.HORA_INICIO + "</Data></Cell>" + ControlChars.NewLine);
                    sb.Append("<Cell><Data ss:Type=" + quote + "String" + quote + ">" + item.HORA_FIN + "</Data></Cell>" + ControlChars.NewLine);
                    sb.Append("</Row>" + ControlChars.NewLine);
                }

                sb.Append("</Table>" + ControlChars.NewLine);

                sb.Append(" </Worksheet>" + ControlChars.NewLine);
            }

            sb.Append("</Workbook>" + ControlChars.NewLine);
            return sb.ToString();
        }

        public string GenerateStringHTML(string Titulo, List<BEAtencion> data, int Tipo) {
            StringBuilder HTML_TEXT_ = new StringBuilder();

            HTML_TEXT_.Append("<HTML xmlns:o='urn:schemas-microsoft-com:office:office'" + Environment.NewLine);
            HTML_TEXT_.Append("xmlns:x='urn:schemas-microsoft-com:office:excel'" + Environment.NewLine);
            HTML_TEXT_.Append("xmlns:ss='urn:schemas-microsoft-com:office:spreadsheet'" + Environment.NewLine);
            HTML_TEXT_.Append("xmlns='http://www.w3.org/TR/REC-html40'>" + Environment.NewLine);
            
            HTML_TEXT_.Append("<HEAD>" + Environment.NewLine);
            HTML_TEXT_.Append("<meta http-equiv=Content-Type content='text/html; charset=utf-8'>" + Environment.NewLine);
            HTML_TEXT_.Append("<meta name=ProgId content=Excel.Sheet>" + Environment.NewLine);
            HTML_TEXT_.Append("<meta name=Generator content='Microsoft Excel 11'>" + Environment.NewLine);
            HTML_TEXT_.Append("<!--[if gte mso 9]><xml>" + Environment.NewLine);
            HTML_TEXT_.Append("<o:DocumentProperties>" + Environment.NewLine);
            HTML_TEXT_.Append("<o:Author>FRANKLIN</o:Author>" + Environment.NewLine);
            HTML_TEXT_.Append("<o:LastAuthor>GODOY AMASIFUEN</o:LastAuthor>" + Environment.NewLine);
            HTML_TEXT_.Append("<o:Company>PCM</o:Company>" + Environment.NewLine);
            HTML_TEXT_.Append("</o:DocumentProperties>" + Environment.NewLine);
            HTML_TEXT_.Append("</xml><![endif]-->" + Environment.NewLine);

            HTML_TEXT_.Append("<STYLE type='text/css'>" + Environment.NewLine);

            HTML_TEXT_.Append("table");
            HTML_TEXT_.Append("{");
            HTML_TEXT_.Append("}");
            HTML_TEXT_.Append(Environment.NewLine);

            HTML_TEXT_.Append("@page");
            HTML_TEXT_.Append("{");
            HTML_TEXT_.Append("mso-header-data:'&Z&\\0022Arial\\,Negrita\\0022&8Aplicativo MacExpres';");
            HTML_TEXT_.Append("mso-footer-data:'&Z&\\0022Arial\\,Negrita\\0022&8PCM&C&8&F - &H&D&8Página &P de &\\#';");
            HTML_TEXT_.Append("margin:.39in .2in .39in .2in;");
            HTML_TEXT_.Append("mso-header-margin:0in;");
            HTML_TEXT_.Append("mso-footer-margin:0in;");
            HTML_TEXT_.Append("mso-page-orientation:landscape;");
            HTML_TEXT_.Append("mso-horizontal-page-align:center;");
            HTML_TEXT_.Append("}");
            HTML_TEXT_.Append(Environment.NewLine);
            
            HTML_TEXT_.Append(".x01");
            HTML_TEXT_.Append("{");
            HTML_TEXT_.Append("font-family:Arial;");
            HTML_TEXT_.Append("font-size:12.0pt;");
            HTML_TEXT_.Append("font-weight:bold;");
            HTML_TEXT_.Append("text-align:center;");
            HTML_TEXT_.Append("vertical-align:middle;");
            HTML_TEXT_.Append("}");
            HTML_TEXT_.Append(Environment.NewLine);
            
            HTML_TEXT_.Append(".x04");
            HTML_TEXT_.Append("{");
            HTML_TEXT_.Append("font-family:Arial;");
            HTML_TEXT_.Append("font-size:8.0pt;");
            HTML_TEXT_.Append("font-weight:bold;");
            HTML_TEXT_.Append("text-align:center;");
            HTML_TEXT_.Append("vertical-align:middle;");
            HTML_TEXT_.Append("border-top:.5pt solid windowtext;");
            HTML_TEXT_.Append("border-right:.5pt solid windowtext;");
            HTML_TEXT_.Append("border-bottom:.5pt solid windowtext;");
            HTML_TEXT_.Append("border-left:.5pt solid windowtext;");
            HTML_TEXT_.Append("background:#C7C7C7;");
            //HTML_TEXT_.Append("white-space:normal;");
            HTML_TEXT_.Append("}");
            HTML_TEXT_.Append(Environment.NewLine);

            HTML_TEXT_.Append(".n03");
            HTML_TEXT_.Append("{");
            HTML_TEXT_.Append("font-family:Arial;");
            HTML_TEXT_.Append("font-size:8.0pt;");
            HTML_TEXT_.Append("text-align:left;");
            HTML_TEXT_.Append("vertical-align:middle;");
            HTML_TEXT_.Append("border-top:.5pt solid windowtext;");
            HTML_TEXT_.Append("border-right:.5pt solid windowtext;");
            HTML_TEXT_.Append("border-bottom:.5pt solid windowtext;");
            HTML_TEXT_.Append("border-left:.5pt solid windowtext;");
            HTML_TEXT_.Append("white-space:normal;");
            HTML_TEXT_.Append("mso-number-format:\\@;");
            HTML_TEXT_.Append("}");
            HTML_TEXT_.Append(Environment.NewLine);
            HTML_TEXT_.Append("</STYLE>" + Environment.NewLine);

            HTML_TEXT_.Append("<!--[if gte mso 9]><xml>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ExcelWorkbook>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ExcelWorksheets>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ExcelWorksheet>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:Name>AtencionesPorSede</x:Name>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:WorksheetOptions>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:DefaultColWidth>10</x:DefaultColWidth>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:Print>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:FitHeight>0</x:FitHeight>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ValidPrinterInfo/>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:PaperSizeIndex>9</x:PaperSizeIndex>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:Scale>46</x:Scale>" + Environment.NewLine);
            HTML_TEXT_.Append("</x:Print>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:Selected/>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ProtectContents>False</x:ProtectContents>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ProtectObjects>False</x:ProtectObjects>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ProtectScenarios>False</x:ProtectScenarios>" + Environment.NewLine);
            HTML_TEXT_.Append("</x:WorksheetOptions>" + Environment.NewLine);
            HTML_TEXT_.Append("</x:ExcelWorksheet>" + Environment.NewLine);
            HTML_TEXT_.Append("</x:ExcelWorksheets>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ProtectStructure>False</x:ProtectStructure>" + Environment.NewLine);
            HTML_TEXT_.Append("<x:ProtectWindows>False</x:ProtectWindows>" + Environment.NewLine);
            HTML_TEXT_.Append("</x:ExcelWorkbook>" + Environment.NewLine);
            HTML_TEXT_.Append("</xml><![endif]-->" + Environment.NewLine);

            HTML_TEXT_.Append("</HEAD>" + Environment.NewLine);
            HTML_TEXT_.Append("<BODY>" + Environment.NewLine);
            HTML_TEXT_.Append("<TABLE border=0 cellpadding=0 cellspacing=0>");
            HTML_TEXT_.Append("<TR><TD></TD></TR>");

            HTML_TEXT_.Append("<TR>");
            HTML_TEXT_.Append("<TD class='x01' colspan=9>" + Titulo + "</TD>");
            HTML_TEXT_.Append("</TR>");
            HTML_TEXT_.Append("<TR>");
            HTML_TEXT_.Append("</TR>");

            
            HTML_TEXT_.Append("<TR>");
           
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=200>MUNICIPLIDAD</TD>");
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=100>SEDE</TD>");
            if (Tipo == 2)
            {
                HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=100>DEPARTAMENTO</TD>");
                HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=100>PROVINCIA</TD>");
                HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=100>DISTRITO</TD>");
            }
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=200>NOMBRE</TD>");
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=80>D.N.I</TD>");
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=120>TELEFONO</TD>");
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=240>SERVICIO</TD>");
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=120>FECHA</TD>");
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=120>HORA INICIO</TD>");
            HTML_TEXT_.Append("<TD rowspan=2 class='x04' width=120>HORA FIN</TD>");
            HTML_TEXT_.Append("</TR>");
            HTML_TEXT_.Append("<TR>");
            HTML_TEXT_.Append("</TR>");

            foreach (var item in data)
            {
                HTML_TEXT_.Append("<TR>");
                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.MUNICIPALIDAD + "</TD>");
                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.SEDE + "</TD>");
                if (Tipo == 2)
                {
                    HTML_TEXT_.Append("<TD class='n03' width=75>" + item.DEPARTAMENTO + "</TD>");
                    HTML_TEXT_.Append("<TD class='n03' width=75>" + item.PROVINCIA + "</TD>");
                    HTML_TEXT_.Append("<TD class='n03' width=75>" + item.DISTRITO + "</TD>");
                }

                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.NOMBRE + "</TD>");
                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.DNI + "</TD>");
                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.TELEFONO + "</TD>");
                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.SERVICIO + "</TD>");
                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.FECHA + "</TD>");
                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.HORA_INICIO + "</TD>");
                HTML_TEXT_.Append("<TD class='n03' width=75>" + item.HORA_FIN + "</TD>");
                HTML_TEXT_.Append("</TR>");
            }

            HTML_TEXT_.Append("</TABLE>" + Environment.NewLine);
            HTML_TEXT_.Append("</BODY>" + Environment.NewLine);
            HTML_TEXT_.Append("</HTML>");

            return HTML_TEXT_.ToString();
        }

        public sealed class ControlChars
        {
            public const char Back = '\b';
            public const char Cr = '\r';
            public const string CrLf = "\r\n";
            public const char FormFeed = '\f';
            public const char Lf = '\n';
            public const string NewLine = "\r\n";
            public const char NullChar = '\0';
            public const char Quote = '"';
            public const char Tab = '\t';
            public const char VerticalTab = '\v';
        }

    }
}
