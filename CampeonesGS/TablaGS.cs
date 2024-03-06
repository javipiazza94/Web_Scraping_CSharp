using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.IO;

namespace GrandSlamCampeones
{
    class CampeonesGS : WebDriverScraperBaseLibrary.WebDriverScraperBase
    {
        public class TablaGS
        {
            // Atributos globales
            public string Año { get; set; }
            public string GanadorAO { get; set; }
            public string NacionalidadAO { get; set; }
            public string GanadorRG { get; set; }
            public string NacionalidadRG { get; set; }
            public string GanadorWB { get; set; }
            public string NacionalidadWB { get; set; }
            public string GanadorUSO { get; set; }
            public string NacionalidadUSO { get; set; }
        }

        static void Main(string[] args)
        {
            using (var driver = new EdgeDriver())
            {
                try
                {
                    OpenURL("https://es.wikipedia.org/wiki/Anexo:Campeones_de_torneos_de_Grand_Slam_(individual_masculino)", driver);
                    driver.Manage().Window.Maximize();

                    // Set implicit wait
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
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
            string tablaSelector = "//table[contains(@class, 'sortable') and contains(@class, 'wikitable') and contains(@class, 'jquery-tablesorter')][1]";
            List<TablaGS> campeones = new List<TablaGS>();

            IWebElement tabla = driver.FindElement(By.XPath(tablaSelector));
            ScrollWindowToElement(tabla, LocationInWindow.TOP, driver);

            IList<IWebElement> filas = tabla.FindElements(By.XPath(".//tbody//tr"));

            foreach (var fila in filas)
            {
                TablaGS liga = new TablaGS();

                IList<IWebElement> celdas = fila.FindElements(By.XPath(".//td"));

                if (celdas.Count == 5)
                {
                    liga.Año = celdas[0].Text;
                    ScrollWindowToElement(celdas[0], LocationInWindow.MIDDLE, driver);

                    liga.GanadorAO = celdas[1].Text;
                    if (!string.IsNullOrEmpty(liga.GanadorAO))
                    {
                        if (liga.GanadorAO.Equals("Cancelado por 2 Guerra Mundial") || liga.GanadorAO.Equals("Cancelado por 1 Guerra Mundial") || liga.GanadorAO.Equals("no hubo competición"))
                        {
                            liga.NacionalidadAO = celdas[1].Text;
                        }
                        else
                        {
                            string flagAO = GetAttribute(fila.FindElement(By.XPath(".//td[2]//img")), "alt").Replace("Bandera del ", "").Replace("Bandera de ", "");
                            liga.NacionalidadAO = flagAO;
                        }
                    }

                    liga.GanadorRG = celdas[2].Text;
                    if (!string.IsNullOrEmpty(liga.GanadorRG))
                    {
                        if (liga.GanadorRG.Equals("Cancelado por 2 Guerra Mundial") || liga.GanadorRG.Equals("Cancelado por 1 Guerra Mundial"))
                        {
                            liga.NacionalidadRG = celdas[2].Text;
                        }
                        else
                        {
                            string flagRG = GetAttribute(fila.FindElement(By.XPath(".//td[3]//img")), "alt").Replace("Bandera del ", "").Replace("Bandera de ", "");
                            liga.NacionalidadRG = flagRG;
                        }
                    }

                    liga.GanadorWB = celdas[3].Text;
                    if (!string.IsNullOrEmpty(liga.GanadorWB))
                    {
                        if (liga.GanadorWB.Equals("Cancelado por 2 Guerra Mundial") || liga.GanadorWB.Equals("Cancelado por 1 Guerra Mundial") || liga.GanadorWB.Equals("cancelado por COVID-19"))
                        {
                            liga.NacionalidadWB = celdas[3].Text;
                        }
                        else
                        {
                            string flagWB = GetAttribute(fila.FindElement(By.XPath(".//td[4]//img")), "alt").Replace("Bandera del ", "").Replace("Bandera de ", "");
                            liga.NacionalidadWB = flagWB;
                        }
                    }

                    liga.GanadorUSO = celdas[4].Text;
                    if (!string.IsNullOrEmpty(liga.GanadorUSO))
                    {
                        if (liga.GanadorUSO.Equals("Cancelado por 2 Guerra Mundial") || liga.GanadorUSO.Equals("Cancelado por 1 Guerra Mundial"))
                        {
                            liga.NacionalidadUSO = celdas[4].Text;
                        }
                        else
                        {
                            string flagUSO = GetAttribute(fila.FindElement(By.XPath(".//td[5]//img")), "alt").Replace("Bandera del ", "").Replace("Bandera de ", "");
                            liga.NacionalidadUSO = flagUSO;
                        }
                    }

                    campeones.Add(liga);
                }
            }

            TakePartialScreenshotsWithScrolling("Tabla de los ganadores de Grand Slam", driver);

            GuardarTabla(campeones);
        }

        private static void GuardarTabla(List<TablaGS> ganadores)
        {
            string ruta = "C:\\Users\\puent\\OneDrive\\Escritorio\\Javi Bootcamp\\Web_Scraping_CSharp\\CSVs\\Ganadores_Grand_Slam.csv";

            using (StreamWriter escritura = new StreamWriter(ruta))
            {
                // Escribimos el nombre de las columnas de la tabla
                string[] nombreColumnas = {
                    "Año", "Ganador AO", "Nacionalidad AO", "Ganador RG", "Nacionalidad RG",
                    "Ganador WB", "Nacionalidad WB", "Ganador USO", "Nacionalidad USO"
                };

                string encabezado = string.Join(",", nombreColumnas);
                escritura.WriteLine(encabezado);

                // Escribimos cada fila de la lista
                foreach (var ganador in ganadores)
                {
                    escritura.WriteLine($"{ganador.Año},{ganador.GanadorAO},{ganador.NacionalidadAO}," +
                                        $"{ganador.GanadorRG},{ganador.NacionalidadRG},{ganador.GanadorWB}," +
                                        $"{ganador.NacionalidadWB},{ganador.GanadorUSO},{ganador.NacionalidadUSO}");
                }
            }
        }
    }
}
