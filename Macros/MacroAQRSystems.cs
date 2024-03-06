using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using WebDriverScraperBaseLibrary;


namespace AQR
{
    class MacroAQRSystems : WebDriverScraperBaseLibrary.WebDriverScraperBase
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
            var startJourney = WaitAndGetElementVisible(By.XPath("//h2[.='Top Quality Web Scraping']/ancestor::div[@class = 'row content']//a"), 10, driver);
            WaitForElementVisible(By.XPath("//h2[.='Top Quality Web Scraping']/ancestor::div[@class = 'row content']//a"), 2000, driver);
            if (startJourney != null)
            {
                Click(By.XPath("//h2[.='Top Quality Web Scraping']/ancestor::div[@class = 'row content']//a"), driver);
            }

            // Email
            var emailElement = WaitAndGetElementVisible(By.Name("email_from"), 10, driver);
            WaitForElementVisible(By.Name("email_from"), 3000, driver);
            if (emailElement != null)
            {
                TypeWithSpeedSetting(emailElement, email, 100, driver);
            }

            // Name
            var nameElement = WaitAndGetElementVisible(By.Name("Your Name"), 1000, driver);
            WaitForElementVisible(By.Name("Your Name"), 3000, driver);
            if (nameElement != null)
            {
                TypeWithSpeedSetting(nameElement, name, 100, driver);
            }

            // Phone
            var phoneElement = WaitAndGetElementVisible(By.Name("Phone Number"), 1000, driver);
            WaitForElementVisible(By.Name("Phone Number"), 3000, driver);
            if (phoneElement != null)
            {
                ClearInput(phoneElement);
                TypeWithSpeedSetting(phoneElement, phone, 100, driver);
            }

            // Company
            var companyElement = WaitAndGetElementVisible(By.Name("Your Company"), 1000, driver);
            WaitForElementVisible(By.Name("Your Company"), 2000, driver);
            if (companyElement != null)
            {
                TypeWithSpeedSetting(companyElement, company, 100, driver);
            }

            // Job Title
            var jobTitleElement = WaitAndGetElementVisible(By.Name("Your Job Title"), 1000, driver);
            WaitForElementVisible(By.Name("Your Job Title"), 3000, driver);
            if (jobTitleElement != null)
            {
                TypeWithSpeedSetting(jobTitleElement, jobTitle, 100, driver);
            }

            // Subject
            var subjectElement = WaitAndGetElementVisible(By.Name("subject"), 1000, driver);
            WaitForElementVisible(By.Name("Your Job Title"), 3000, driver);
            if (subjectElement != null)
            {
                TypeWithSpeedSetting(subjectElement, tasks, 100, driver);
            }

            // Webs...
            IWebElement webs = driver.FindElement(By.CssSelector("input#olaplo9zubm0"));
            WaitForElementVisible(By.CssSelector("input#olaplo9zubm0"), 3000, driver);
            if (IsElementPresentAndVisible(webs))
            {
                Click(By.CssSelector("input#olaplo9zubm0"), driver);
            }

            TakeScreenShotIfEnabled(driver, "Datos", 0, 0, 1600, 800);
            TakeScreenshot(driver, "PEPE");

            // Different Webs...
            IWebElement agreementWeb = driver.FindElement(By.CssSelector("input#omhwxxkioc2a1"));
            WaitForElementVisible(By.CssSelector("input#omhwxxkioc2a1"), 3000, driver);
            if (IsElementPresentAndVisible(agreementWeb))
            {
                Click(By.CssSelector("input#omhwxxkioc2a1"), driver);
            }

            // Competitors
            IWebElement competitorsWeb = driver.FindElement(By.CssSelector("input#ovxsfwsyfson1"));
            WaitForElementVisible(By.CssSelector("input#ovxsfwsyfson1"), 3000, driver);
            if (IsElementPresentAndVisible(competitorsWeb))
            {
                Click(By.CssSelector("input#ovxsfwsyfson1"), driver);
            }

            // Captcha
            IWebElement captcha = driver.FindElement(By.CssSelector("input#o8hdcwfo9l942"));
            WaitForElementVisible(By.CssSelector("input#o8hdcwfo9l942"), 3000, driver);
            if (IsElementPresentAndVisible(captcha))
            {
                Click(By.CssSelector("input#o8hdcwfo9l942"), driver);
            }

            // Specific Country
            IWebElement specificCountry = driver.FindElement(By.CssSelector("input#orsygimovho1"));
            WaitForElementVisible(By.CssSelector("input#orsygimovho1"), 3000, driver);
            if (IsElementPresentAndVisible(specificCountry))
            {
                Click(By.CssSelector("input#orsygimovho1"), driver);
            }

            // Scraping illegal
            IWebElement sIllegal = driver.FindElement(By.CssSelector("input#ocqx7hcfo6or2"));
            WaitForElementVisible(By.CssSelector("input#ocqx7hcfo6or2"), 3000, driver);
            if (IsElementPresentAndVisible(sIllegal))
            {
                Click(By.CssSelector("input#ocqx7hcfo6or2"), driver);
            }

            // Parametrized collection
            IWebElement paraCollectl = driver.FindElement(By.CssSelector("input#ob6y3fb92dwl0"));
            WaitForElementVisible(By.CssSelector("input#ob6y3fb92dwl0"), 3000, driver);
            if (IsElementPresentAndVisible(paraCollectl))
            {
                Click(By.CssSelector("input#ob6y3fb92dwl0"), driver);
            }

            // Run collection
            IWebElement runCollect = driver.FindElement(By.CssSelector("input#ov8nb0rro6w0"));
            WaitForElementVisible(By.CssSelector("input#ov8nb0rro6w0"), 3000, driver);
            if (IsElementPresentAndVisible(runCollect))
            {
                Click(By.CssSelector("input#ov8nb0rro6w0"), driver);
            }

            // Schedule data
            IWebElement scheduleData = driver.FindElement(By.CssSelector("input#otnvrl1uprq4"));
            WaitForElementVisible(By.CssSelector("input#otnvrl1uprq4"), 3000, driver);
            if (IsElementPresentAndVisible(scheduleData))
            {
                Click(By.CssSelector("input#otnvrl1uprq4"), driver);
            }    
            
            // Schedule data
            IWebElement collectData = driver.FindElement(By.CssSelector("input#ox7qxnxndxwd2"));
            WaitForElementVisible(By.CssSelector("input#ox7qxnxndxwd2"), 3000, driver);
            if (IsElementPresentAndVisible(collectData))
            {
                Click(By.CssSelector("input#ox7qxnxndxwd2"), driver);
            }

            //  Start date data
            IWebElement startDateData = driver.FindElement(By.Name("When do you need data collection to start?"));
            WaitForElementVisible(By.Name("When do you need data collection to start?"), 3000, driver);
            if (IsElementPresentAndVisible(startDateData))
            {
                DateTime date = DateTime.Now;
                string fechaFormateada = date.ToString("yyyy-MM-dd");
                startDateData.SendKeys(fechaFormateada);
            }

            //  finish date data
            IWebElement finishDateData = driver.FindElement(By.Name("When do you need data collection to finish?"));
            WaitForElementVisible(By.Name("When do you need data collection to finish?"), 3000, driver);
            if (IsElementPresentAndVisible(finishDateData))
            {
                DateTime date = DateTime.Now.AddYears(1);
                string fechaFormateada = date.ToString("yyyy-MM-dd");
                finishDateData.SendKeys(fechaFormateada);
            }

            //  Budget date data
            IWebElement budgetDateData = driver.FindElement(By.Name("When do you need an answer with a budget?"));
             WaitForElementVisible(By.Name("When do you need an answer with a budget?"), 3000, driver);
             if (IsElementPresentAndVisible(budgetDateData))
             {
                DateTime date = DateTime.Now.AddMonths(3);
                string fechaFormateada = date.ToString("yyyy-MM-dd");
                 budgetDateData.SendKeys(fechaFormateada);

              }

            // Type of data
            IWebElement typeData = driver.FindElement(By.CssSelector("input#o4eiuqx0lul3"));
            WaitForElementVisible(By.CssSelector("input#o4eiuqx0lul3"), 3000, driver);
            if (IsElementPresentAndVisible(typeData))
            {
                Click(By.CssSelector("input#o4eiuqx0lul3"), driver);
            }

            // Message
            var messageScraped = WaitAndGetElementVisible(By.Name("Need to describe the project in more detail?"), 10, driver);
            WaitForElementVisible(By.Name("Need to describe the project in more detail?"), 3000, driver);
            if (emailElement != null)
            {
                TypeWithSpeedSetting(messageScraped, message, 100, driver);
            }

            // Type of data
            IWebElement submit = driver.FindElement(By.CssSelector("a[role='button']"));
            WaitForElementVisible(By.CssSelector("a[role='button']"), 3000, driver);
            if (IsElementPresentAndVisible(submit))
            {
                Click(By.CssSelector("a[role='button']"), driver);
            }

            #region Helpers

            #endregion

        }
    }
}
