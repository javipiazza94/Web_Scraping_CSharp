using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text;
using System.Drawing;

using System.Drawing.Imaging;

namespace WebDriverScraperBaseLibrary
{
    public class WebDriverScraperBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LabyrinthWebDriverScraperBase"/> class.
        /// </summary>
        public WebDriverScraperBase()
            : base()
        {

        }

        /// <summary>
        /// Opens the URL.
        /// </summary>
        /// <param name="URL">The URL.</param>
        public static void OpenURL(string URL, IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Navigate().GoToUrl(URL);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberSeconds"></param>
        public static void Sleep(int numberSeconds)
        {
            Thread.Sleep(numberSeconds);
        }

        /// <summary>
        /// Gets the body text.
        /// </summary>
        /// <returns></returns>
        public static String GetBodyText(IWebDriver driver)
        {
            bool present = false;
            return driver.FindElement(By.TagName("body")).Text;
        }


        /// <summary>
        /// Method to manage stale element exception
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <param name="times">Times to try</param>
        public static void ManageStaleElementException(Action action, int times)
        {
            bool done = false;
            int count = 0;

            while (done.Equals(false) && count < times)
            {
                try
                {
                    action.Invoke();
                    done = true;
                }
                catch (Exception e)
                {
                    if (e.GetType().Name.Equals("StaleElementReferenceException"))
                    {
                        Thread.Sleep(1000);
                        count++;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
        }

        /// <summary>
        /// Setups Make screenshots by cuts
        /// </summary>
        /// <param name="cssSelector">The element located by css.</param>
        /// <param name="message">Name of Screenshot.</param>
        public static void TakePartialScreenshotsWithScrolling(string message, IWebDriver driver)
        {
            int current = 0;
            int height = GetWindowSize(driver).Height;
            int numberOfScreenshots = (height / 800) + (height % 800 > 0 ? 1 : 0);

            if (height <= 800)
            {
                TakeScreenshot(driver, message);
                return;
            }

            // Go to top
            RunScript($"window.scrollTo(0, 0);", driver);

            // Following screenshots
            for (int i = 0; i < numberOfScreenshots; i++)
            {
                TakeScreenshot(driver, $"{message}. Part {i + 1}");
                ScrollPageDown(driver, 800);
                current += 800;
                if (current > height)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static Size GetWindowSize(IWebDriver driver)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            long width = (long)jsExecutor.ExecuteScript("return window.innerWidth;");
            long height = (long)jsExecutor.ExecuteScript("return window.innerHeight;");
            return new Size((int)width, (int)height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="yOffset"></param>
        public static void ScrollPageDown(IWebDriver driver, int yOffset)
        {
            RunScript($"window.scrollTo(0, {yOffset});", driver);
        }

        public static void TakeScreenShotIfEnabled(IWebDriver driver, string message, int x, int y, int width, int height)
        {
            // Tomar el screenshot si el driver es válido y las coordenadas son válidas
            if (driver != null && x >= 0 && y >= 0 && width > 0 && height > 0)
            {
                // Convertir el WebDriver a tipo ITakesScreenshot
                ITakesScreenshot takesScreenshotDriver = driver as ITakesScreenshot;

                // Tomar el screenshot como tipo Screenshot
                Screenshot screenshot = takesScreenshotDriver.GetScreenshot();

                // Crear un rectángulo con las coordenadas proporcionadas
                Rectangle cropArea = new Rectangle(x, y, width, height);

                // Recortar el screenshot usando las coordenadas proporcionadas
                Bitmap croppedImage = CropImage(screenshot.AsByteArray, cropArea);

                // Guardar el screenshot recortado en una ubicación específica o procesarlo según sea necesario
                croppedImage.Save($"C:\\Users\\puent\\OneDrive\\Escritorio\\Javi Bootcamp\\Web_Scraping_CSharp\\Imgs\\{message}.png", ImageFormat.Png);
            }
        }

        // Método para recortar una imagen utilizando un área específica
        public static Bitmap CropImage(byte[] imageBytes, Rectangle cropArea)
        {
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Bitmap originalImage = new Bitmap(ms);
                Bitmap croppedImage = originalImage.Clone(cropArea, originalImage.PixelFormat);
                return croppedImage;
            }
        }


        /// <summary>
        /// Takes an entire page screen shot if the param has been enabled at config file. Screenshots are saved 
        /// at Desktop\SeleniumScreenShots\YYYY_MM\[AssignmentID]\[ScreenShotCounter].PNG
        /// <br/>Version 0.9.8 - Added HTML feature. If the param "SeleniumSendHTMLAtSuccess" is enabled, when you call this function
        /// also the HTML page will be saved
        /// </summary>
        public static void TakeScreenshot(IWebDriver driver, string message)
        {
            // Tomar la captura de pantalla y guardarla en el archivo especificado
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            string path = $"C:\\Users\\puent\\OneDrive\\Escritorio\\Javi Bootcamp\\Web_Scraping_CSharp\\Imgs\\{message}.png";
            screenshot.SaveAsFile(path);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public static void ClearInput(IWebElement element)
        {
            element.Click();
            element.Clear();
            element.SendKeys(Keys.Control + "A");
            element.SendKeys(Keys.Backspace);
        }

        /// <summary>
        /// Remove extra white spaces: doble space or more and return single space. 
        /// Useful to standardize texts before to check for matches
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveExtraWhiteSpaces(string value)
        {
            var parts = value.Split(' ');
            var stringBuilder = new StringBuilder();

            foreach (var part in parts)
            {
                if (!string.IsNullOrEmpty(part) && !string.IsNullOrWhiteSpace(part))
                {
                    if (part.Length.Equals(1) && char.IsPunctuation(part, 0) && stringBuilder.Length > 0)
                    {
                        // adds the punctuation character without previous space. Eg: Bristol, England. 
                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    }
                    stringBuilder.AppendFormat("{0} ", part);
                }
            }

            return stringBuilder.ToString().TrimEnd(' ');
        }

        /// <summary>
        /// Types into the specified by element.
        /// </summary>
        /// <param name="by">The by element.</param>
        /// <param name="text">The text.</param>
        public static void Type(By by, String text, IWebDriver driver)
        {
            ScrollWindowToElement(by, LocationInWindow.MIDDLE, driver);
            IWebElement inputElement = driver.FindElement(by);
            ClearInput(inputElement);
            inputElement.SendKeys(text);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="text"></param>
        /// <param name="speed"></param>
        /// <param name="driver"></param>
        public static void TypeWithSpeedSetting(IWebElement input, string text, int speed, IWebDriver driver)
        {
            string[] textArray = text.ToCharArray().Select(c => c.ToString()).ToArray();

            var actions = new Actions(driver);

            input.Click();
            input.Clear();

            foreach (var c in textArray)
            {
                actions.SendKeys(c);
                actions.Perform();
                Thread.Sleep(new Random().Next((int)(0.75 * speed), (int)(1.5 * speed)));
            }
        }

        /// <summary>
        /// For inputs that require a focus to trigger some events this method is quite helpful. Only when element is present and visible.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <param name="text">The text.</param>
        public static void TypeWithFocusIfPresentAndVisible(By byElement, String text, IWebDriver driver)
        {
            if (IsElementPresentAndVisible(byElement, driver))
            {
                Type(byElement, text, driver);
                driver.SwitchTo().Window(driver.CurrentWindowHandle);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="by"></param>
        /// <param name="maxSeconds"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement WaitAndGetElementVisible(By by, int maxSeconds, IWebDriver driver)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxSeconds));
                wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

                IWebElement webElement = wait.Until(d =>
                {
                    var element = d.FindElement(by);
                    return (element != null && element.Displayed) ? element : null;
                });

                return webElement;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="elementBy"></param>
        /// <param name="maxSeconds"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool WaitForElementVisible(By elementBy, int maxmiliSeconds, IWebDriver driver)
        {
            Thread.Sleep(maxmiliSeconds);
            IWebElement webElement = WaitAndGetElementVisible(elementBy, maxmiliSeconds, driver);
            if (webElement != null)
                return webElement.Displayed;
            else
                return false;
        }

        /// <summary>
        /// Waits for all elements of a list and their texts
        /// </summary>
        /// <param name="locator">The common locator for the list elements</param>
        /// <param name="maxTime">Max waiting time</param>
        public static void WaitForAllListElements(By locator, int maxTime, IWebDriver driver)
        {
            int timeCount = 0;
            int previousCount = -1;
            ReadOnlyCollection<IWebElement> listElements;

            // Wait for the first element of the list
            WaitForElementVisible(locator, 5000, driver);

            // The list of elements
            listElements = driver.FindElements(locator);

            while ((listElements.Count != previousCount ||
                listElements.Select(c => c.Text).ToList().Any(t => string.IsNullOrEmpty(t))) &&
                timeCount <= maxTime)
            {
                Thread.Sleep(1000);
                previousCount = listElements.Count;
                listElements = driver.FindElements(locator);
                timeCount++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool IsElementPresentAndVisible(IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }


        /// <summary>Determines whether [is element present and visible and not editable] [the specified by].</summary>
        /// <param name="by">The by.</param>
        /// <returns>
        ///   <c>true</c> if [is element present and visible and not editable] [the specified by]; otherwise, <c>false</c>.</returns>
        public static bool IsElementPresentAndVisibleAndNotEditable(By by, IWebDriver driver)
        {
            bool presentAndVisibleAndNotEditable = false;

            try
            {
                IWebElement element = driver.FindElement(by);
                presentAndVisibleAndNotEditable = element.Displayed && !element.Enabled;
            }
            catch (NoSuchElementException)
            {
            }

            return presentAndVisibleAndNotEditable;
        }



        /// <summary>Determines whether [is element present and visible and editable] [the specified by].</summary>
        /// <param name="by">The by.</param>
        /// <returns>
        ///   <c>true</c> if [is element present and visible and editable] [the specified by]; otherwise, <c>false</c>.</returns>
        public static bool IsElementPresentAndVisibleAndEditable(By by, IWebDriver driver)
        {
            bool presentAndVisibleAndEditable = false;

            try
            {
                IWebElement element = driver.FindElement(by);
                presentAndVisibleAndEditable = element.Displayed && element.Enabled;
            }
            catch (NoSuchElementException)
            {
            }

            return presentAndVisibleAndEditable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public static bool IsElementPresent(By by, IWebDriver driver)
        {
            bool present = false;

            try
            {
                present = driver.FindElement(by) != null;
            }
            catch (NoSuchElementException)
            {
            }

            return present;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static bool IsElementPresentAndVisible(By by, IWebDriver driver)
        {
            bool presentAndVisible = false;

            try
            {
                presentAndVisible = driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
            }
            catch (StaleElementReferenceException)
            {
            }

            return presentAndVisible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by"></param>
        /// <param name="maxSeconds"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public static IWebElement WaitAndGetElementPresent(By by, int maxSeconds, IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxSeconds));

            try
            {
                return wait.Until(d => d.FindElement(by));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        /// <summary>
        /// Runs the script.
        /// </summary>
        /// <param name="script">The script.</param>
        public static void RunScript(String script, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }

        /// <summary>
        /// Runs the script with parameters.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="args">The arguments.</param>
        public static void RunScriptWithParameters(String script, object[] args, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <returns></returns>
        public static String GetText(By byElement, IWebDriver driver)
        {
            return driver.FindElement(byElement).Text;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static String GetAttribute(By byElement, String attribute, IWebDriver driver)
        {
            return driver.FindElement(byElement).GetAttribute(attribute);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string GetAttribute(IWebElement element, string attribute)
        {
            return element.GetAttribute(attribute);
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attr">The attribute.</param>
        public static void RemoveAttribute(IWebElement element, String attr, IWebDriver driver)
        {
            RunScriptWithParameters($"arguments[0].removeAttribute('{attr}');", new object[] { element }, driver);
        }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attr">The attribute.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static void SetAttribute(IWebElement element, String attr, String value, IWebDriver driver)
        {
            RunScriptWithParameters($"arguments[0].setAttribute('{attr}', '{value}');", new object[] { element }, driver);
        }

        /// <summary>Gets the text if present.</summary>
        /// <param name="byElement">The by element.</param>
        /// <returns></returns>
        public static String GetTextIfPresent(By byElement, IWebDriver driver)
        {
            IWebElement element = WaitAndGetElementPresent(byElement, 1, driver);
            return element?.Text ?? "";
        }

        /// <summary>Gets the attribute if present.</summary>
        /// <param name="byElement">The by element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public static String GetAttributeIfPresent(By byElement, String attribute, IWebDriver driver)
        {
            IWebElement element = WaitAndGetElementPresent(byElement, 1, driver);
            return element?.GetAttribute(attribute) ?? "";
        }
        /// <summary>
        /// Determines whether [is text present and the text node visible] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static bool IsTextPresentAndVisible(String text, IWebElement driver)
        {
            bool presentAndVisible = false;

            try
            {
                // [21/09/2022] To handle texts with single quote. Eg: ...something's gone wrong here.
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.XPath($"//*[contains(text(),\"{text}\")]"));
                presentAndVisible = elements.Any(c => c.Displayed);

                return presentAndVisible;
            }
            catch
            {
                return presentAndVisible = false;
            }

            return presentAndVisible;
        }

        /// <summary>
        /// Where we want see the selected element when scroll the window.
        /// </summary>
        public enum LocationInWindow
        {

            TOP,
            MIDDLE,
            BOTTOM
        }

        public static void ScrollWindowToElement(By byElement, LocationInWindow location, IWebDriver driver)
        {
            ScrollWindowToElement(driver.FindElement(byElement), location, driver);
        }

        /// <summary>
        /// Scroll the window to the selected element and setting the location for the element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="location"></param>
        public static void ScrollWindowToElement(IWebElement element, LocationInWindow location, IWebDriver driver)
        {
            int elementX = element.Location.X;
            int elementY = element.Location.Y;
            int windowHeight = driver.Manage().Window.Size.Height;

            switch (location)
            {
                case LocationInWindow.TOP:
                    elementY -= (windowHeight * 20 / 100);
                    break;
                case LocationInWindow.BOTTOM:
                    elementY -= (windowHeight * 80 / 100);
                    break;
                case LocationInWindow.MIDDLE:
                default:
                    elementY -= (windowHeight * 50 / 100);
                    break;
            }

            try
            {
                //RunScript("window.scrollTo(0, " + elementLocation + ");");
                RunScript("window.scrollTo( { top: " + elementY + ", left: " + elementX + ", behavior: 'smooth' });", driver);
            }
            catch
            {
                // Deal System.InvalidOperationException: javascript error: target.offset is not a function                
                RunScript("window.scrollTo(0, " + elementY + ");", driver);
            }

        }

        /// <summary>
        /// Adds the context error.
        /// </summary>
        /// <param name="contextError">The context error.</param>
        /// <param name="driver">The WebDriver instance.</param>
        public static void AddContextError(string contextError, IWebDriver driver)
        {
            // Do some cleansing before sending it back to the server
            string cleanedError = contextError.Replace("\r\n", "<br/>").Replace("\r", "").Replace("\n", "<br/>").Replace("\"", "'");

            // Log or handle the cleaned error as needed
            Console.WriteLine(cleanedError);

            // Sleep if needed
            Thread.Sleep(1000);
        }

        public static void MouseHover(IWebElement element, IWebDriver driver)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(element).Perform();
        }

        /// <summary>Waits and click if element visible.</summary>
        /// <param name="by">The by.</param>
        /// <param name="maxSeconds">The maximum seconds.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static bool WaitAndClickElementVisible(By by, int maxSeconds, IWebDriver driver)
        {
            try
            {
                IWebElement webElement = WaitAndGetElementVisible(by, maxSeconds, driver);
                Click(by, driver);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Waits for element to disappear. This method assumes the element is already present and visible although it waits 1 seconds to help it showing up
        /// </summary>
        /// <param name="by">The by.</param>
        /// <param name="maxSeconds">The maximum seconds.</param>
        /// <returns>True if the element is not longer present and visible</returns>
        public static bool WaitForElementToDisappear(By by, int maxSeconds, IWebDriver driver)
        {
            Thread.Sleep(1000);
            int counter = 0;
            while (IsElementPresentAndVisible(by, driver) && counter < maxSeconds)
            {
                counter++;
                Thread.Sleep(1000);
            }

            return !IsElementPresentAndVisible(by, driver);
        }

        /// Clicks the specified by element.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <summary>Clicks at all costs.</summary>
        /// <param name="byElement">The by element.</param>
        public static void ClickAtAllCosts(By byElement, IWebDriver driver)
        {
            IWebElement element = null;

            try
            {
                element = driver.FindElement(byElement);
            }
            catch (NoSuchElementException)
            {
                AddContextError("Element " + byElement + " not found!", driver);
                return;
            }

            if (element.Displayed && element.Enabled) // Element displayed and ready for click
            {
                try
                {

                    MouseHover(element, driver);
                    ScrollWindowToElement(element, LocationInWindow.MIDDLE, driver);
                    element.Click();
                }
                catch (Exception ex) // Normally when the element is obscured or invisible in some form
                {
                    // Finally, click using JS
                    RunScriptWithParameters("arguments[0].focus(); arguments[0].click();", new object[] { element }, driver);
                }
            }
            else // Hidden element
            {
                // Try JS first                
                RunScriptWithParameters("arguments[0].focus(); arguments[0].click();", new object[] { element }, driver);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="byElement"></param>
        /// <param name="driver"></param>
        public static void Click(By byElement, IWebDriver driver)
        {
            ScrollWindowToElement(byElement, LocationInWindow.MIDDLE, driver);
            ClickAtAllCosts(byElement, driver);
        }

        /// <summary>Clicks the label for.</summary>
        /// <param name="elementId">The element identifier.</param>
        public void ClickLabelFor(string elementId, IWebDriver driver)
        {
            Click(By.CssSelector(string.Format("label[for='{0}']", elementId)), driver);
        }

        /// <summary>
        /// Clicks the using js.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        public static void ClickUsingJS(By byElement, IWebDriver driver)
        {
            object[] args = { driver.FindElement(byElement) };
            RunScriptWithParameters("arguments[0].click();", args, driver);
        }

        /// <summary>
        /// Clicks if present.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        public static void ClickIfPresent(By byElement, IWebDriver driver)
        {
            if (IsElementPresent(byElement, driver))
                Click(byElement, driver);
        }



        /// <summary>
        /// Clicks if present and visible.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        public static void ClickIfPresentAndVisible(By byElement, IWebDriver driver)
        {
            if (IsElementPresentAndVisible(byElement, driver))
                Click(byElement, driver);
        }

        /// <summary>
        /// Clean the options on a list
        /// </summary>
        /// <param name="options"></param>
        /// <param name="textsToReplace"></param>
        /// <returns></returns>
        public static string[] CleanOptions(string[] options, string[] textsToReplace = null)
        {
            IEnumerable<string> replacedOptions = options;

            if (textsToReplace != null)
            {
                // Clean the text options 
                foreach (var text in textsToReplace)
                {
                    replacedOptions = replacedOptions.Select(c => c.Replace(text, "").Trim());
                }
            }

            return replacedOptions.ToArray();

        }

        /// <summary>
        /// Checks if not checked.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        public static void CheckIfNotChecked(By byElement, IWebDriver driver)
        {
            IWebElement checkElement = driver.FindElement(byElement);

            if (!checkElement.Selected)
                checkElement.Click();
        }


        /// <summary>Unchecks if checked.</summary>
        /// <param name="byElement">The by element.</param>
        public static void UncheckIfChecked(By byElement, IWebDriver driver)
        {
            IWebElement checkElement = driver.FindElement(byElement);

            if (checkElement.Selected)
                checkElement.Click();
        }

        /// <summary>
        /// Types the using js.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <param name="text">The text.</param>
        public static void TypeUsingJS(By byElement, string text, IWebDriver driver)
        {
            object[] args = { driver.FindElement(byElement) };
            RunScriptWithParameters("arguments[0].value = '" + text + "';", args, driver);
        }



        /// <summary>
        /// Determines whether the specified by element is checked.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <returns></returns>
        public static bool IsChecked(By byElement, IWebDriver driver)
        {
            return driver.FindElement(byElement).Selected;
        }

        /// <summary>
        /// Waits for all elements of a list and gets an array with the text from all the web elements with same locator
        /// </summary>
        /// <param name="locator">The locator</param>
        /// <param name="textsToReplace">The text we want to remove to clean the options</param>
        /// <returns></returns>
        public static string[] GetTextOptionsFromElements(By locator, IWebDriver driver, string[] textsToReplace = null)
        {
            WaitAndGetElementVisible(locator, 10, driver);

            var webElements = driver.FindElements(locator);

            // Get all the text options from the IWebElements
            var options = webElements.Select(c => c.Text);
            options = CleanOptions(options.ToArray(), textsToReplace);
            return options.ToArray();
        }

        /// <summary>Gets the text if present and not visible.</summary>
        /// <param name="byElement">The by element.</param>
        /// <returns></returns>
        public static String GetTextIfPresentAndNotVisible(By byElement, IWebDriver driver)
        {
            object[] args = { driver.FindElement(byElement) };
            return ((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].innerText;", args).ToString();
        }


        /// <summary>
        /// Finds the select.
        /// </summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <returns></returns>
        public static SelectElement FindSelect(By selectElementBy, IWebDriver driver)
        {
            return new SelectElement(driver.FindElement(selectElementBy));
        }

        /// <summary>
        /// Gets the select values.
        /// </summary>
        /// <param name="selectElement">The select element.</param>
        /// <returns></returns>
        public static string[] GetSelectValues(SelectElement selectElement)
        {
            string[] selectValues = new string[selectElement.Options.Count];

            for (int i = 0; i < selectValues.Length; i++)
                selectValues[i] = selectElement.Options[i].GetAttribute("value");
            return selectValues;
        }

        /// <summary>
        /// Gets the select options.
        /// </summary>
        /// <param name="selectElement">The select element.</param>
        /// <returns></returns>
        public static string[] GetSelectOptions(SelectElement selectElement)
        {
            string[] selectOptions = new string[selectElement.Options.Count];

            for (int i = 0; i < selectOptions.Length; i++)
                selectOptions[i] = selectElement.Options[i].Text;

            return selectOptions;
        }


        /// <summary>
        /// Gets the select options.
        /// </summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <returns></returns>
        public static string[] GetSelectOptions(By selectElementBy, IWebDriver driver)
        {
            return GetSelectOptions(new SelectElement(driver.FindElement(selectElementBy)));
        }


        /// <summary>
        /// Waits the and get select field filled and visible.
        /// </summary>
        /// <param name="selectFieldBy">The select field by.</param>
        /// <param name="maxSeconds">The maximum seconds.</param>
        /// <returns></returns>
        public static SelectElement WaitAndGetSelectFieldFilledAndVisible(By selectFieldBy, int maxSeconds, WebDriver driver)
        {
            IWebElement webElement = WaitAndGetElementVisible(selectFieldBy, maxSeconds, driver);

            if (webElement != null)
            {
                SelectElement select = new SelectElement(webElement);
                if (select.Options.Count > 0)
                    return select;
                else
                    return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Selects by index.
        /// </summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <param name="indexToMatch">The index to match.</param>
        public static void SelectByIndex(By selectElementBy, int indexToMatch, WebDriver driver)
        {
            SelectElement selectElement = WaitAndGetSelectFieldFilledAndVisible(selectElementBy, 10, driver);
            selectElement.SelectByIndex(indexToMatch);
        }


        /// <summary>
        /// Selects by label.
        /// </summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <param name="labelToMatch">The label to match.</param>
        public static void SelectByLabel(By selectElementBy, string labelToMatch, WebDriver driver)
        {
            SelectElement selectElement = WaitAndGetSelectFieldFilledAndVisible(selectElementBy, 10, driver);
            selectElement.SelectByText(labelToMatch);
        }


        /// <summary>
        /// Selects by value.
        /// </summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <param name="valueToMatch">The value to match.</param>
        public static void SelectByValue(By selectElementBy, string valueToMatch, WebDriver driver)
        {
            SelectElement selectElement = WaitAndGetSelectFieldFilledAndVisible(selectElementBy, 10, driver);
            selectElement.SelectByValue(valueToMatch);
        }



        /// <summary>Gets the selected label.</summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <returns></returns>
        public static string GetSelectedLabel(By selectElementBy, WebDriver driver)
        {
            SelectElement selectElement = WaitAndGetSelectFieldFilledAndVisible(selectElementBy, 10, driver);
            return selectElement.SelectedOption.Text;
        }


        /// <summary>Gets the selected value.</summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <returns></returns>
        public string GetSelectedValue(By selectElementBy, WebDriver driver)
        {
            SelectElement selectElement = WaitAndGetSelectFieldFilledAndVisible(selectElementBy, 10, driver);
            return selectElement.SelectedOption.GetAttribute("value");
        }



        /// <summary>
        /// Selects the best option in a a select dropdown by label. The best option is calculated using the "GetBestMatchInSelectFieldOptionsForVehicles" standard search method        
        /// Returns the index
        /// </summary>
        /// <param name="selectElementBy"></param>
        /// <param name="labelToMatch"></param>
        /// <returns></returns>
        public static int SelectBestOptionByLabel(By selectElementBy, string labelToMatch)
        {
            return SelectBestOptionByLabel(selectElementBy, labelToMatch);
        }


        /// <summary>Selects the best numeric option by label (it assumes all labels are numeric values and they are ordered)</summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <param name="labelToMatch">The label to match.</param>
        /// <returns></returns>
        public static int SelectBestNumericOptionByLabel(By selectElementBy, float labelToMatch)
        {
            return SelectBestNumericOptionByLabel(selectElementBy, labelToMatch);
        }



        /// <summary>
        /// Selects the best option in a select dropdown by value. The best option is calculated using the "GetBestMatchInSelectFieldOptionsForVehicles" standard search method.        
        /// Returns the index
        /// </summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <param name="valueToMatch">The value to match.</param>
        public static int SelectBestOptionByValue(By selectElementBy, string valueToMatch)
        {
            return SelectBestOptionByValue(selectElementBy, valueToMatch);
        }




        /// <summary>Selects the best numeric option by value.
        /// It assumes the values are all numeric and ordered</summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <param name="valueToMatch">The value to match.</param>
        /// <returns></returns>
        public static int SelectBestNumericOptionByValue(By selectElementBy, float valueToMatch)
        {
            return SelectBestNumericOptionByValue(selectElementBy, valueToMatch);
        }



        /// <summary>
        /// Gets the select values of a WebDriver Select
        /// </summary>
        /// <param name="selectElementBy">The select element by.</param>
        /// <returns></returns>
        public static string[] GetSelectValues(By selectElementBy, IWebDriver driver)
        {
            return GetSelectValues(new SelectElement(driver.FindElement(selectElementBy)));
        }

        /// <summary>
        /// Wait for IWebElement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="maxSeconds"></param>
        private static void WaitForIframeVisible(IWebElement element, int maxSeconds)
        {
            int count = 0;
            do
            {
                Thread.Sleep(1000);
                count++;
            }
            while (!element.Displayed && (count < maxSeconds));
        }

        /// <summary>
        /// Method to switch to the iframe and execute the action
        /// </summary>
        /// <param name="iframe"></param>
        /// <param name="action"></param>
        private static void SwitchAndDoStuff(IWebElement iframe, Action action, IWebDriver driver)
        {
            // Switch to the iframe
            driver.SwitchTo().Frame(iframe);

            // Do stuff into the iframe
            action();

            Thread.Sleep(2000);
        }

        /// <summary>
        /// Method to handle iframes according used browser
        /// </summary>
        /// <param name="iframe">The frameElement </param>
        /// <param name="action">The method which contains the actions we want to do into the iframe</param>
        public static void ManageIframe(IWebElement iframe, Action action, IWebDriver driver)
        {
            WaitForIframeVisible(iframe, 10);
            SwitchAndDoStuff(iframe, action, driver);
        }

        /// <summary>
        /// Method to handle iframes according used browser
        /// </summary>
        /// <param name="iframe">The Iframe(string) name or ID </param>
        /// <param name="action">The method which contains the actions we want to do into the iframe</param>
        public static void ManageIframe(string iframe, Action action, IWebDriver driver)
        {
            IWebElement element;
            try
            {
                element = driver.FindElement(By.Id(iframe));
            }
            catch
            {
                element = driver.FindElement(By.Name(iframe));
            }

            WaitForIframeVisible(element, 10);
            SwitchAndDoStuff(element, action, driver);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="driver"></param>
        /// <param name="textToFind"></param>
        /// <param name="textsToRemove"></param>
        /// <returns></returns>
        public static bool ClickExactOptionFromElements(By locator, IWebDriver driver, string textToFind, string[] textsToRemove = null)
        {
            // Get the array of options
            var options = GetTextOptionsFromElements(locator, driver, textsToRemove);

            // Search the exact 
            int match = options.ToList().FindIndex(c => c.Equals(textToFind));

            // There is not exact match
            if (match.Equals(-1) || !driver.FindElements(locator)[match].Enabled)
            {
                return false;
            }

            // Click the button
            driver.FindElements(locator)[match].Click();
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        public enum Person
        {
            Principal,
            Couple,
            Father,
            Mother,
            Son,
            Daughter,
            // Add more family members as needed
        }

        /// <summary>
        /// Simulates the blur event on Active element
        /// </summary>
        public static void SimulateBlurEvent(IWebDriver driver)
        {
            driver.SwitchTo().ActiveElement().SendKeys(Keys.Tab);
        }

        /// <summary>
        /// Returns the first visible and enable element. Useful when there are several elements with same locator
        /// </summary>
        /// <param name="byElement">The by element</param>
        /// <returns></returns>
        public static IWebElement GetFirstVisibleAndEnableElement(By byElement, IWebDriver driver)
        {
            var a = driver.FindElements(byElement);

            return a.First(c => c.Displayed && c.Enabled);
        }

        /// <summary>
        /// Click the first visible and enable element. Useful when there are several elements with same locator
        /// </summary>
        /// <param name="byElement"></param>
        public static void ClickFirstVisibleAndEnableElement(By byElement, IWebDriver driver)
        {
            var a = driver.FindElements(byElement);

            a.First(c => c.Displayed && c.Enabled).Click();
        }

        /// <summary>
        /// Is the passed value a value that we consider to be 'yes'?
        /// </summary>
        /// <param name="value">value from the database to check</param>
        /// <returns>Is it considered a 'yes'?</returns>
        public static bool IsYes(string value)
        {
            value = value.Trim();
            return value.Equals("yes", StringComparison.CurrentCultureIgnoreCase) ||
                   value.Equals("y", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Is the passed value a value that we consider to be 'no'?
        /// </summary>
        /// <param name="value">value from the database to check</param>
        /// <returns>Is it considered a 'no'?</returns>
        public static bool IsNo(string value)
        {
            value = value.Trim();
            return value.Equals("no", StringComparison.CurrentCultureIgnoreCase) ||
                   value.Equals("n", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Is the passed value n/a?
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNa(string value)
        {
            return value.Equals("N/A", StringComparison.CurrentCultureIgnoreCase);
        }

    }
}
