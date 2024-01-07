using Macros;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;


namespace Macros
{
    class MacroAQRSystems
    {
        static void Main(string[] args)
        {
            using (var driver = new EdgeDriver())
            {
                try
                {
                    driver.Url = "https://www.aqrsystems.com/services/web-scraping";
                    driver.Manage().Window.Maximize();

                    // Set implicit wait
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    // Go to the form
                    FillForm(driver);

                    // Other actions...
                }
                finally
                {
                    driver.Quit();
                }
            }
        }

        private static void FillForm(IWebDriver driver)
        {
            ScraperBase scraper = new ScraperBase();

            //Data
            string email = "javipiazza94@gmail.com";
            string name = "Javier Puente Piazza";
            string phone = "618809725";
            string company = "AQR Systems";
            string jobTitle = "Intern";
            string tasks = "Web Scraping Practice";
            string message = "¡Hola José!\r\n\r\nEspero que este mensaje te encuentre bien. Quería contarte que durante estas festividades navideñas," +
                " estoy dedicando tiempo a perfeccionar mis habilidades en el mundo del scraping desde la comodidad de mi hogar. " +
                "He desarrollado una macro local que me está ayudando en este proceso de entrenamiento.\r\n\r\n" +
                "Este mensaje no es relevante para ti, no te preocupes, simplemente ignóralo. " +
                "Sin embargo, no quería perder la oportunidad de desearte " +
                "¡Feliz Navidad y un próspero 2024! Que esta época esté llena de alegría, paz y momentos especiales junto a tus seres queridos.\r\n\r\n" +
                "¡Saludos y mis mejores deseos para ti en el próximo año!. Mensaje escrito por Alladin, IA de BlackRock, la que se quedará con tu empresa en 2030";

            //Start your journey
            var startJourney = scraper.WaitAndGetElementVisible(By.XPath("//h2[.='Top Quality Web Scraping']/ancestor::div[@class = 'row content']//a"), 10, driver);
            scraper.WaitForElementVisible(By.XPath("//h2[.='Top Quality Web Scraping']/ancestor::div[@class = 'row content']//a"), 2000, driver);
            if (startJourney != null)
            {
                scraper.Click(By.XPath("//h2[.='Top Quality Web Scraping']/ancestor::div[@class = 'row content']//a"), driver);
            }

            // Email
            var emailElement = scraper.WaitAndGetElementVisible(By.Name("email_from"), 10, driver);
            scraper.WaitForElementVisible(By.Name("email_from"), 3000, driver);
            if (emailElement != null)
            {
                scraper.TypeWithSpeedSetting(emailElement, email, 100, driver);
            }

            // Name
            var nameElement = scraper.WaitAndGetElementVisible(By.Name("Your Name"), 1000, driver);
            scraper.WaitForElementVisible(By.Name("Your Name"), 3000, driver);
            if (nameElement != null)
            {
                scraper.TypeWithSpeedSetting(nameElement, name, 100, driver);
            }

            // Phone
            var phoneElement = scraper.WaitAndGetElementVisible(By.Name("Phone Number"), 1000, driver);
            scraper.WaitForElementVisible(By.Name("Phone Number"), 3000, driver);
            if (phoneElement != null)
            {
                scraper.ClearInput(phoneElement);
                scraper.TypeWithSpeedSetting(phoneElement, phone, 100, driver);
            }

            // Company
            var companyElement = scraper.WaitAndGetElementVisible(By.Name("Your Company"), 1000, driver);
            scraper.WaitForElementVisible(By.Name("Your Company"), 2000, driver);
            if (companyElement != null)
            {
                scraper.TypeWithSpeedSetting(companyElement, company, 100, driver);
            }

            // Job Title
            var jobTitleElement = scraper.WaitAndGetElementVisible(By.Name("Your Job Title"), 1000, driver);
            scraper.WaitForElementVisible(By.Name("Your Job Title"), 3000, driver);
            if (jobTitleElement != null)
            {
                scraper.TypeWithSpeedSetting(jobTitleElement, jobTitle, 100, driver);
            }

            // Subject
            var subjectElement = scraper.WaitAndGetElementVisible(By.Name("subject"), 1000, driver);
            scraper.WaitForElementVisible(By.Name("Your Job Title"), 3000, driver);
            if (subjectElement != null)
            {
                scraper.TypeWithSpeedSetting(subjectElement, tasks, 100, driver);
            }

            // Webs...
            IWebElement webs = driver.FindElement(By.CssSelector("input#olaplo9zubm0"));
            scraper.WaitForElementVisible(By.CssSelector("input#olaplo9zubm0"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(webs))
            {
                scraper.Click(By.CssSelector("input#olaplo9zubm0"), driver);
            }

            // Different Webs...
            IWebElement agreementWeb = driver.FindElement(By.CssSelector("input#omhwxxkioc2a1"));
            scraper.WaitForElementVisible(By.CssSelector("input#omhwxxkioc2a1"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(agreementWeb))
            {
                scraper.Click(By.CssSelector("input#omhwxxkioc2a1"), driver);
            }

            // Competitors
            IWebElement competitorsWeb = driver.FindElement(By.CssSelector("input#ovxsfwsyfson1"));
            scraper.WaitForElementVisible(By.CssSelector("input#ovxsfwsyfson1"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(competitorsWeb))
            {
                scraper.Click(By.CssSelector("input#ovxsfwsyfson1"), driver);
            }

            // Captcha
            IWebElement captcha = driver.FindElement(By.CssSelector("input#o8hdcwfo9l942"));
            scraper.WaitForElementVisible(By.CssSelector("input#o8hdcwfo9l942"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(captcha))
            {
                scraper.Click(By.CssSelector("input#o8hdcwfo9l942"), driver);
            }

            // Specific Country
            IWebElement specificCountry = driver.FindElement(By.CssSelector("input#orsygimovho1"));
            scraper.WaitForElementVisible(By.CssSelector("input#orsygimovho1"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(specificCountry))
            {
                scraper.Click(By.CssSelector("input#orsygimovho1"), driver);
            }

            // Scraping illegal
            IWebElement sIllegal = driver.FindElement(By.CssSelector("input#ocqx7hcfo6or2"));
            scraper.WaitForElementVisible(By.CssSelector("input#ocqx7hcfo6or2"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(sIllegal))
            {
                scraper.Click(By.CssSelector("input#ocqx7hcfo6or2"), driver);
            }

            // Parametrized collection
            IWebElement paraCollectl = driver.FindElement(By.CssSelector("input#ob6y3fb92dwl0"));
            scraper.WaitForElementVisible(By.CssSelector("input#ob6y3fb92dwl0"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(paraCollectl))
            {
                scraper.Click(By.CssSelector("input#ob6y3fb92dwl0"), driver);
            }

            // Run collection
            IWebElement runCollect = driver.FindElement(By.CssSelector("input#ov8nb0rro6w0"));
            scraper.WaitForElementVisible(By.CssSelector("input#ov8nb0rro6w0"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(runCollect))
            {
                scraper.Click(By.CssSelector("input#ov8nb0rro6w0"), driver);
            }

            // Schedule data
            IWebElement scheduleData = driver.FindElement(By.CssSelector("input#otnvrl1uprq4"));
            scraper.WaitForElementVisible(By.CssSelector("input#otnvrl1uprq4"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(scheduleData))
            {
                scraper.Click(By.CssSelector("input#otnvrl1uprq4"), driver);
            }    
            
            // Schedule data
            IWebElement collectData = driver.FindElement(By.CssSelector("input#ox7qxnxndxwd2"));
            scraper.WaitForElementVisible(By.CssSelector("input#ox7qxnxndxwd2"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(collectData))
            {
                scraper.Click(By.CssSelector("input#ox7qxnxndxwd2"), driver);
            }

            //  Start date data
            IWebElement startDateData = driver.FindElement(By.Name("When do you need data collection to start?"));
            scraper.WaitForElementVisible(By.Name("When do you need data collection to start?"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(startDateData))
            {
                DateTime date = DateTime.Now;
                string fechaFormateada = date.ToString("yyyy-MM-dd");
                startDateData.SendKeys(fechaFormateada);
            }

            //  finish date data
            IWebElement finishDateData = driver.FindElement(By.Name("When do you need data collection to finish?"));
            scraper.WaitForElementVisible(By.Name("When do you need data collection to finish?"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(finishDateData))
            {
                DateTime date = DateTime.Now.AddYears(1);
                string fechaFormateada = date.ToString("yyyy-MM-dd");
                finishDateData.SendKeys(fechaFormateada);
            }

            //  Budget date data
            IWebElement budgetDateData = driver.FindElement(By.Name("When do you need an answer with a budget?"));
             scraper.WaitForElementVisible(By.Name("When do you need an answer with a budget?"), 3000, driver);
             if (scraper.IsElementPresentAndVisible(budgetDateData))
             {
                DateTime date = DateTime.Now.AddMonths(3);
                string fechaFormateada = date.ToString("yyyy-MM-dd");
                 budgetDateData.SendKeys(fechaFormateada);

              }

            // Type of data
            IWebElement typeData = driver.FindElement(By.CssSelector("input#o4eiuqx0lul3"));
            scraper.WaitForElementVisible(By.CssSelector("input#o4eiuqx0lul3"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(typeData))
            {
                scraper.Click(By.CssSelector("input#o4eiuqx0lul3"), driver);
            }

            // Message
            var messageScraped = scraper.WaitAndGetElementVisible(By.Name("Need to describe the project in more detail?"), 10, driver);
            scraper.WaitForElementVisible(By.Name("Need to describe the project in more detail?"), 3000, driver);
            if (emailElement != null)
            {
                scraper.TypeWithSpeedSetting(messageScraped, message, 100, driver);
            }

            // Type of data
            IWebElement submit = driver.FindElement(By.CssSelector("a[role='button']"));
            scraper.WaitForElementVisible(By.CssSelector("a[role='button']"), 3000, driver);
            if (scraper.IsElementPresentAndVisible(submit))
            {
                scraper.Click(By.CssSelector("a[role='button']"), driver);
            }

            #region Helpers

            #endregion

        }
    }
}
