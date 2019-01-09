using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using OfficeOpenXml;
using GemBox.Spreadsheet;
using Spire.Xls;
using System.Globalization;
using System.Xml.Serialization;

namespace ProyectoF
{
    static class DataManager
    {
        const string pathIEXML = "ArchivoIE.xml";
        const string pathIEJSON = "ArchivoIE.json";
        const string pathCSV = "ReporteCSV.csv";
        const string pathPDF = "ReportePDF.pdf";
        const string pathExcel = "ReporteExcel.xlsx";
        const string pathContador = "ID.dat";
        const string pathCanciones = "Canciones.json";   //Aqui pueden cambiar donde se encuentra el archivo-
        const string pathListas = "Listas.json";         //De normal, ambos archivos se encucentran en la carpeta Bin, por lo que no es necesario usar un path-
                                                         //Solo escribir el nombre del archivo.
        
        public static void RecuperarCanciones(out List<Cancion> Canciones)
        {
            string archivoCanciones = File.ReadAllText(pathCanciones);

            if (archivoCanciones.Length!=0)
                Canciones = JsonConvert.DeserializeObject<List<Cancion>>(archivoCanciones);
            else
                Canciones = new List<Cancion>();
            
        }

        public static void ImportarXML(ref List<Cancion> Canciones) //Verificar si sirve!!!!!!!!!
        {
            List<Cancion> Temporal = new List<Cancion>();
            string archivoCanciones = File.ReadAllText(pathIEXML);
            if (archivoCanciones.Length != 0)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Cancion>));
                using (TextReader reader = new StringReader(archivoCanciones))
                {
                    Temporal = (List<Cancion>)serializer.Deserialize(reader);
                }
            }
            foreach (Cancion K in Temporal)
            {
                Canciones.Add(K);
            }
        }

        public static void ImportarJson(ref List<Cancion> Canciones) //Verificar si sirve!!!!!!!!!
        {
            List<Cancion> Temporal = new List<Cancion>();
            string archivoCanciones = File.ReadAllText(pathIEJSON);
            if (archivoCanciones.Length != 0)
            {
                Temporal = JsonConvert.DeserializeObject<List<Cancion>>(archivoCanciones);
            }
            foreach (Cancion K in Temporal)
            {
                Canciones.Add(K);
            }
        }

        public static void ExportarXML(List<Cancion> Canciones)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<Cancion>));
            FileStream file = new FileStream(pathIEXML, FileMode.Create);
            writer.Serialize(file, Canciones);
        }

        public static void ExportarJson(List<Cancion> Canciones)
        {
            string strCanciones = JsonConvert.SerializeObject(Canciones);
            File.WriteAllText(pathIEJSON, strCanciones);
        }

        public static void RecuperarPlaylists(out List<Playlist> playlists)
        {
            string archivoListas = File.ReadAllText(pathListas);

            if (archivoListas.Length != 0)
                playlists = JsonConvert.DeserializeObject<List<Playlist>>(archivoListas);
            else
                playlists = new List<Playlist>();

        }

        public static void RecuperarID(out int IdCanciones, out int IdPlaylist)
        {
            string[] contador_ID = File.ReadAllLines(pathContador);

            if (contador_ID.Length == 2)
            {
                    IdCanciones = Convert.ToInt32(contador_ID[0]);
                    IdPlaylist = Convert.ToInt32(contador_ID[1]);
            }
            else
            {
                IdCanciones = 1;
                IdPlaylist = 1;
            }

        }

        public static void Grabar(List<Cancion> canciones, List<Playlist> listas, int IdCanciones, int IdListas)
        {
            //Guarda las canciones
            string strCanciones = JsonConvert.SerializeObject(canciones);
            File.WriteAllText(pathCanciones, strCanciones);

            //Guarda las listas
            string strListas = JsonConvert.SerializeObject(listas);
            File.WriteAllText(pathListas, strListas);

            //Guarda los IDs
            string[] contador_ID=new string[2];
            contador_ID[0] = IdCanciones.ToString();
            contador_ID[1] = IdListas.ToString();
            File.WriteAllLines(pathContador, contador_ID);
            
        }

        public static void ReporteExcel(List<Playlist> listas, bool exist)
        {
            int fila = 1;

            File.Delete(pathExcel);
            FileInfo spreadsheetInfo = new FileInfo(pathExcel);

            ExcelPackage pck = new ExcelPackage(spreadsheetInfo);
            var ExcelWorkSheet = pck.Workbook.Worksheets.Add("Listas de Reproduccion");

            foreach (var lista in listas)
            {
                ExcelWorkSheet.Cells["A" + fila.ToString()].Value = lista.nombre;
                fila++;
                
                foreach(var cancion in lista.Cancions)
                {
                    ExcelWorkSheet.Cells["A" + fila.ToString()].Value = cancion.Nombre;
                    ExcelWorkSheet.Cells["B" + fila.ToString()].Value = cancion.Artista;
                    ExcelWorkSheet.Cells["C" + fila.ToString()].Value = cancion.Genero;
                    ExcelWorkSheet.Cells["D" + fila.ToString()].Value = cancion.Album;
                    ExcelWorkSheet.Cells["E" + fila.ToString()].Value = cancion.Duracion.ToString();
                    ExcelWorkSheet.Cells["F" + fila.ToString()].Value = cancion.Calidad;
                    ExcelWorkSheet.Cells["G" + fila.ToString()].Value = cancion.Formato;
                    fila++;
                    
                }

                ExcelWorkSheet.Cells["A" + fila.ToString()].Value ="Total de Canciones: " + lista.Cancions.Count.ToString();
                fila += 2;
            }

            pck.Save();

            if (!exist)
            {
                File.SetAttributes(pathExcel, FileAttributes.Hidden);
            }
        }

        public static void ReportePDF(List<Playlist> listas)
        {
            ReporteExcel(listas, File.Exists(pathExcel) && (File.GetAttributes(pathExcel) & FileAttributes.Archive) == FileAttributes.Archive);
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            XlsxLoadOptions loadOptions = new XlsxLoadOptions();
            ExcelFile excel = ExcelFile.Load(pathExcel, loadOptions);

            PdfSaveOptions saveOptions = new PdfSaveOptions();
            excel.Save(pathPDF, saveOptions);
        }

        public static void ReporteCSV(List<Playlist> listas)
        {
            ReporteExcel(listas, File.Exists(pathExcel) && (File.GetAttributes(pathExcel) & FileAttributes.Archive) == FileAttributes.Archive);

            //Workbook workbook = new Workbook();

            //workbook.LoadFromFile(pathExcel);

            //Worksheet sheet = workbook.Worksheets[0];

            //sheet.SaveToFile("ReporteCSV.csv", " ", Encoding.UTF8);

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            XlsxLoadOptions loadOptions = new XlsxLoadOptions();
            ExcelFile excel = ExcelFile.Load(pathExcel, loadOptions);


            excel.Save(
                pathCSV,
                new CsvSaveOptions(CsvType.SemicolonDelimited)
                {
                    FormatProvider = CultureInfo.CurrentCulture
                });

        }


    }
}
