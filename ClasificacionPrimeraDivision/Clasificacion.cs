
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using static System.Collections.Specialized.BitVector32;
using WebDriverScraperBaseLibrary;

namespace LFP
{
    class Clasificacion : WebDriverScraperBaseLibrary.WebDriverScraperBase
    {
        public class PrimeraDivision
        {
            // Atributos globales
            public string NombreEquipo { get; set; }
            public string PosicionLiga { get; set; }
            public string Puntos { get; set; }
            public string Partidos { get; set; }
            public string GolesAFavor { get; set; }
            public string GolesEnContra { get; set; }
            public string Victorias { get; set; }
            public string Derrotas { get; set; }
            public string Empates { get; set; }

            // Atributos para partidos en casa
            public string PuntosCasa { get; set; }
            public string PartidosCasa { get; set; }
            public string GolesAFavorCasa { get; set; }
            public string GolesEnContraCasa { get; set; }
            public string VictoriasCasa { get; set; }
            public string DerrotasCasa { get; set; }
            public string EmpatesCasa { get; set; }

            // Atributos para partidos fuera de casa
            public string PuntosFuera { get; set; }
            public string PartidosFuera { get; set; }
            public string GolesAFavorFuera { get; set; }
            public string GolesEnContraFuera { get; set; }
            public string VictoriasFuera { get; set; }
            public string DerrotasFuera { get; set; }
            public string EmpatesFuera { get; set; }
        }

        static void Main(string[] args)
        {
            using (var driver = new EdgeDriver())
            {
                try
                {
                    OpenURL("https://resultados.as.com/resultados/futbol/primera/clasificacion/", driver);
                    driver.Manage().Window.Maximize();

                    // Set implicit wait
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

                    // Go to the form
                    if (WaitForElementVisible(By.XPath("//a[contains( text(), 'Aceptar y continuar gratis')]"), 10, driver))
                    {
                        Click(By.XPath("//a[contains( text(), 'Aceptar y continuar gratis')]"), driver);
                    }                 
                    Sleep(2000);
                    ScrapeTabla(driver);

                    // Other actions...
                }
                finally
                {
                    driver.Quit();
                }
            }
        }

        private static void ScrapeTabla(EdgeDriver driver)
        {
            string tablaLocalizador = "//table[@class='tabla-datos table-hover']//tbody//tr";
            List<PrimeraDivision> equipos = new List<PrimeraDivision>();

            var tabla = driver.FindElements(By.XPath(tablaLocalizador));
            ScrollWindowToElement(By.XPath(tablaLocalizador), LocationInWindow.TOP, driver);

            foreach (var posicion in tabla)
            {
                PrimeraDivision liga = new PrimeraDivision(); 

                // Datos para partidos totales
                liga.PosicionLiga = posicion.FindElement(By.XPath(".//span[@class='pos']")).Text;
                liga.NombreEquipo = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']//a//span[@class='nombre-equipo']")).Text;
                liga.Puntos = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[1]")).Text;
                liga.Partidos = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[2]")).Text;
                liga.Victorias = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[3]")).Text;
                liga.Empates = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[4]")).Text;
                liga.Derrotas = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[5]")).Text;
                liga.GolesAFavor = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[6]")).Text;
                liga.GolesEnContra = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[7]")).Text;
                // Datos para partidos en casa
                liga.PuntosCasa = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[8]")).Text;
                liga.PartidosCasa = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[9]")).Text;
                liga.VictoriasCasa = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[10]")).Text;
                liga.EmpatesCasa = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[11]")).Text;
                liga.DerrotasCasa = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[12]")).Text;
                liga.GolesAFavorCasa = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[13]")).Text;
                liga.GolesEnContraCasa = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[14]")).Text;
                // Datos para partidos fuera de casa
                liga.PuntosFuera = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[15]")).Text;
                liga.PartidosFuera = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[16]")).Text;
                liga.VictoriasFuera = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[17]")).Text;
                liga.EmpatesFuera = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[18]")).Text;
                liga.DerrotasFuera = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[19]")).Text;
                liga.GolesAFavorFuera = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[20]")).Text;
                liga.GolesEnContraFuera = posicion.FindElement(By.XPath(".//th[@class='cont-nombre-equipo']/following-sibling::td[21]")).Text;

                //Guardamos cada fila en una lista
                equipos.Add(liga);
               
            }
            //Hacemos el screenshot parcial de la tabla para comprobarla
            TakePartialScreenshotsWithScrolling("Clasificacion de Primera Division en la 2023-2024", driver);

            //Guardamos la tabla en un csv
            GuardarTabla(equipos);
        }
        private static void GuardarTabla(List<PrimeraDivision> equipos)
        {
            string ruta = "C:\\Users\\puent\\OneDrive\\Escritorio\\Javi Bootcamp\\Web_Scraping_CSharp\\CSVs\\ClasificacionLiga2023-2024.csv";

            using (StreamWriter escritura = new StreamWriter(ruta))
            {
                // Escribimos el nombre de las columnas de la tabla
                string[] columnNames = {
                "Posicion Liga", "Nombre Equipo", "Puntos", "Partidos", "Victorias", "Empates", "Derrotas",
                "Goles A Favor", "Goles En Contra", "Puntos Casa", "Partidos Casa", "Victorias Casa", "Empates Casa",
                "Derrotas Casa", "Goles A Favor Casa", "Goles En Contra Casa", "Punto sFuera", "Partidos Fuera",
                "Victorias Fuera", "Empates Fuera", "Derrotas Fuera", "Goles A Favor Fuera", "Goles En Contra Fuera"
                };

                string headerRow = string.Join(",", columnNames);
                escritura.WriteLine(headerRow);

                //Escribimos cada fila de la lista
                foreach (var equipo in equipos)
                {
                    escritura.WriteLine($"{equipo.PosicionLiga},{equipo.NombreEquipo},{equipo.Puntos},{equipo.Partidos}," +
                                    $"{equipo.Victorias},{equipo.Empates},{equipo.Derrotas},{equipo.GolesAFavor},{equipo.GolesEnContra}," + // Datos para partidos totales
                                     $"{equipo.PuntosCasa},{equipo.PartidosCasa},{equipo.VictoriasCasa},{equipo.EmpatesCasa},{equipo.DerrotasCasa}," +
                                     $"{equipo.GolesAFavorCasa},{equipo.GolesEnContraCasa}," + // Datos para partidos en casa
                                     $"{equipo.PuntosFuera},{equipo.PartidosFuera},{equipo.VictoriasFuera},{equipo.EmpatesFuera},{equipo.DerrotasFuera}," +
                                     $"{equipo.GolesAFavorFuera},{equipo.GolesEnContraFuera}"); // Datos para partidos fuera de casa
                }
            }

        }

    }
}

