using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroMoneySupermarket
{
    internal class ScraperBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void ClearInput(IWebElement element)
        {
            element.Click();
            element.Clear();
            element.SendKeys(Keys.Control + "A");
            element.SendKeys(Keys.Backspace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="text"></param>
        /// <param name="speed"></param>
        /// <param name="driver"></param>
        public void TypeWithSpeedSetting(IWebElement input, string text, int speed, IWebDriver driver)
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
        /// </summary>
        /// <param name="by"></param>
        /// <param name="maxSeconds"></param>
        /// <param name="driver"></param>
        /// <returns></returns>
        public IWebElement WaitAndGetElementVisible(By by, int maxSeconds, IWebDriver driver)
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
        public bool WaitForElementVisible(By elementBy, int maxmiliSeconds, IWebDriver driver)
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
        public void WaitForAllListElements(By locator, int maxTime, IWebDriver driver)
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
        public bool IsElementPresentAndVisible(IWebElement element)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="by"></param>
        /// <returns></returns>
        public bool IsElementPresent(By by, IWebDriver driver)
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
        public bool IsElementPresentAndVisible(By by, IWebDriver driver)
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
        public IWebElement WaitAndGetElementPresent(By by, int maxSeconds, IWebDriver driver)
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
        public void RunScript(String script, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }

        /// <summary>
        /// Runs the script with parameters.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="args">The arguments.</param>
        public void RunScriptWithParameters(String script, object[] args, IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(script, args);
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <returns></returns>
        public String GetText(By byElement, IWebDriver driver)
        {
            return driver.FindElement(byElement).Text;
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        public String GetAttribute(By byElement, String attribute, IWebDriver driver)
        {
            return driver.FindElement(byElement).GetAttribute(attribute);
        }

        /// <summary>
        /// Removes the attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="attr">The attribute.</param>
        public void RemoveAttribute(IWebElement element, String attr, IWebDriver driver)
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
        protected void SetAttribute(IWebElement element, String attr, String value, IWebDriver driver)
        {
            RunScriptWithParameters($"arguments[0].setAttribute('{attr}', '{value}');", new object[] { element }, driver);
        }

        /// <summary>Gets the text if present.</summary>
        /// <param name="byElement">The by element.</param>
        /// <returns></returns>
        public String GetTextIfPresent(By byElement, IWebDriver driver)
        {
            IWebElement element = WaitAndGetElementPresent(byElement, 1, driver);
            return element?.Text ?? "";
        }

        /// <summary>Gets the attribute if present.</summary>
        /// <param name="byElement">The by element.</param>
        /// <param name="attribute">The attribute.</param>
        /// <returns></returns>
        protected String GetAttributeIfPresent(By byElement, String attribute, IWebDriver driver)
        {
            IWebElement element = WaitAndGetElementPresent(byElement, 1, driver);
            return element?.GetAttribute(attribute) ?? "";
        }
        /// <summary>
        /// Determines whether [is text present and the text node visible] [the specified text].
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        protected bool IsTextPresentAndVisible(String text, IWebElement driver)
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

        protected void ScrollWindowToElement(By byElement, LocationInWindow location, IWebDriver driver)
        {
            ScrollWindowToElement(driver.FindElement(byElement), location, driver);
        }

        /// <summary>
        /// Scroll the window to the selected element and setting the location for the element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="location"></param>
        protected void ScrollWindowToElement(IWebElement element, LocationInWindow location, IWebDriver driver)
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
        public void AddContextError(string contextError, IWebDriver driver)
        {
            // Do some cleansing before sending it back to the server
            string cleanedError = contextError.Replace("\r\n", "<br/>").Replace("\r", "").Replace("\n", "<br/>").Replace("\"", "'");

            // Log or handle the cleaned error as needed
            Console.WriteLine(cleanedError);

            // Sleep if needed
            Thread.Sleep(1000);
        }

        public void MouseHover(IWebElement element, IWebDriver driver)
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
        public bool WaitAndClickElementVisible(By by, int maxSeconds, IWebDriver driver)
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
        /// Clicks the specified by element.
        /// </summary>
        /// <param name="byElement">The by element.</param>
        /// <summary>Clicks at all costs.</summary>
        /// <param name="byElement">The by element.</param>
        public void ClickAtAllCosts(By byElement, IWebDriver driver)
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

        public void Click(By byElement, IWebDriver driver)
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
        /// Clean the options on a list
        /// </summary>
        /// <param name="options"></param>
        /// <param name="textsToReplace"></param>
        /// <returns></returns>
        public string[] CleanOptions(string[] options, string[] textsToReplace = null)
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
        /// Waits for all elements of a list and gets an array with the text from all the web elements with same locator
        /// </summary>
        /// <param name="locator">The locator</param>
        /// <param name="textsToReplace">The text we want to remove to clean the options</param>
        /// <returns></returns>
        public string[] GetTextOptionsFromElements(By locator, IWebDriver driver, string[] textsToReplace = null)
        {
            WaitAndGetElementVisible(locator, 10, driver);

            var webElements = driver.FindElements(locator);

            // Get all the text options from the IWebElements
            var options = webElements.Select(c => c.Text);
            options = CleanOptions(options.ToArray(), textsToReplace);
            return options.ToArray();
        }



        public bool ClickExactOptionFromElements(By locator, IWebDriver driver, string textToFind, string[] textsToRemove = null)
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

    }
}
