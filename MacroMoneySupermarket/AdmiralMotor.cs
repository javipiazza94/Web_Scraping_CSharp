
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;



namespace Macros
{
    class Admiral:LabyrinthWebDriverScraperBase
    {
        static void Main(string[] args)
        {
            using (var driver = new EdgeDriver())
            {
                try
                {
                    var scraper = new LabyrinthWebDriverScraperBase();
                    Person familyMember = new Person();
                    driver.Url = "https://www.admiral.com/car-insurance";
                    driver.Manage().Window.Maximize();

                    // Set implicit wait
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    //Cookies
                    var cookies = WaitAndGetElementVisible(By.Id("ccc-recommended-settings"), 1000, driver);
                    if (IsElementPresentAndVisible(cookies))
                    {
                        Thread.Sleep(2000);
                        Click(By.Id("ccc-recommended-settings"), driver);                      
                    }

                    //Start your journey
                    var startJourney = WaitAndGetElementVisible(By.CssSelector("a[href='/scQuoteStatus.php']"), 1000, driver);
                    if (IsElementPresentAndVisible(startJourney))
                    {
                        Click(By.CssSelector("a[href='/scQuoteStatus.php']"), driver);
                        Thread.Sleep(2000);
                        IWebElement popup = driver.FindElement(By.XPath("//div[contains(@class, 'modal-overlay')]//a[@href='/scQuoteStatus.php']"));
                        popup.Click();
                        Thread.Sleep(2000);

                    }
                    // Go to the form
                    YourCar(driver, scraper);
                    FillDetailsForDriver(familyMember, driver, scraper);

                    // Other actions...
                }
                finally
                {
                    driver.Quit();
                }
            }
        }

        private static void YourCar(EdgeDriver driver, LabyrinthWebDriverScraperBase scraper)
        {
            // Data
            string manufacturer = "CITROEN";
            string model = " C5 ";
            string year = "2004";
            string derivative = "SX 16V 1749cc 5 Door";
            string transmissionCar = "Manual";
            string fuelType = "petrol";

            // Don't know registration number --> Search Manually
            IWebElement dontRegNumb = driver.FindElement(By.CssSelector("a[data-test='reg-unknown-toggle']"));
            dontRegNumb.Click();
            Thread.Sleep(2000);

            // Car SearchinG
            IWebElement selectCar = driver.FindElement(By.CssSelector("select[data-test='make-dropdown']"));
            selectCar.Click();
            SelectElement select = new SelectElement(selectCar);
            Thread.Sleep(2000);

            // Manufacturer
            select.SelectByText(manufacturer);
            Thread.Sleep(2000);
            // Search button
            driver.FindElement(By.CssSelector("button[btntype='secondary']")).Click();
            Thread.Sleep(2000);

            // Model
            IWebElement modelC5 = driver.FindElement(By.XPath($"//label[@class='adm-control-selectable__item ng-star-inserted']//div[@class='adm-control-selectable__item-title']//adm-control-selectable-item-title[contains(text(), 'C5' )]"));
            modelC5.Click();
            Thread.Sleep(2000);
            IWebElement NextModel = driver.FindElement(By.CssSelector("button[data-test='model-next-button']"));
            NextModel.Click();
            Thread.Sleep(2000);

            // Fuel
            fuelType.ToUpper();
            IWebElement petrol = driver.FindElement(By.XPath($"//label[@data-test='fuel-list']//span[contains(text(), {fuelType})]"));
            petrol.Click();
            Thread.Sleep(2000);

            // Transmission
            IWebElement transmission = driver.FindElement(By.XPath($"//label[@data-test='transmissions-list']//span[contains(text(), {transmissionCar})]"));
            transmission.Click();
            Thread.Sleep(2000);

            // When was registered
            IWebElement registered = driver.FindElement(By.CssSelector("input[formcontrolname='year']"));
            TypeWithSpeedSetting(registered, year, 100, driver);
            Thread.Sleep(2000);
            // Next button
            IWebElement nextCarDetails = driver.FindElement(By.CssSelector("button[data-test='search-fuel-transmission-year-next-button']"));
            nextCarDetails.Click();
            Thread.Sleep(2000);

            // derivative
            IWebElement listDerivative= driver.FindElement(By.XPath($"//label[@class='adm-control-selectable__item ng-star-inserted']//div[@class='adm-control-selectable__item-title']//adm-control-selectable-item-title[contains(text(), '{derivative}')]"));
            listDerivative.Click();
            Thread.Sleep(2000);
            IWebElement NextDerivative = driver.FindElement(By.CssSelector("button[data-test='manual-search-results-next-button']"));
            NextDerivative.Click();
            Thread.Sleep(2000);

            // Continue
            continueButton(driver);

        }

        private static void FillDetailsForDriver(Person familyMember, EdgeDriver driver, LabyrinthWebDriverScraperBase scraper)
        {
            switch (familyMember)
            {
                case Person.Principal:
                    YourDetails(driver, scraper, familyMember);
                    DetailsCorrect(driver, scraper);
                    Drivers(driver, scraper, familyMember);                  
                    break;
                case Person.Mother:
                    AddPassenger(familyMember, scraper, driver);
                    // Fill details for the mother
                    break;
            }
        }


        private static void YourDetails(EdgeDriver driver, LabyrinthWebDriverScraperBase scraper, Person person)
        {
            string postcode = "";
            string lastName = "";
            string firstName = "";

            string year = "";
            string month = "";
            string day = "";

            switch (person)
            {
                case Person.Principal:
                    // Data for Principal
                    postcode = "SW1A 1AA";
                    lastName = "Puente Crespo";
                    firstName = "Manuel";
                    DateTime date = DateTime.Now.AddYears(-72);
                    year = date.Year.ToString();
                    month = date.Month.ToString();
                    day = date.Day.ToString();
                    break;

                case Person.Mother:
                    // Data for Mother
                    postcode = "SW1A 1AB";
                    lastName = "Piazza de la Maza";
                    firstName = "Reyes";
                    DateTime date2 = DateTime.Now.AddYears(-66);
                    year = date2.Year.ToString();
                    month = date2.Month.ToString();
                    day = date2.Day.ToString();
                    break;

                default:
                    // Handle other cases or provide default values
                    postcode = "DefaultPostcode";
                    lastName = "DefaultLastName";
                    firstName = "DefaultFirstName";
                    break;
            }

            // Title
            IWebElement title = driver.FindElement(By.XPath("//label[@class='adm-control-button-grid__item ng-star-inserted']//span[text()=' Mr ' ]"));
            title.Click();
            Thread.Sleep(2000);

            // First name
            IWebElement name = driver.FindElement(By.Id("firstName"));
            TypeWithSpeedSetting(name, firstName, 100, driver);
            Thread.Sleep(2000);

            // Last name
            IWebElement surname = driver.FindElement(By.Id("lastName"));
            TypeWithSpeedSetting(surname, lastName, 100, driver);
            Thread.Sleep(2000);

            //DOB
            IWebElement DOBDay = driver.FindElement(By.Id("dob-day"));
            TypeWithSpeedSetting(DOBDay, day, 100, driver);
            Thread.Sleep(1000);
            IWebElement DOBMonth = driver.FindElement(By.Id("dob-month"));
            TypeWithSpeedSetting(DOBMonth, month, 100, driver);
            Thread.Sleep(1000);
            IWebElement DOBYear = driver.FindElement(By.Id("dob-year"));
            TypeWithSpeedSetting(DOBYear, year, 100, driver);
            Thread.Sleep(2000);

            // Postcode
            IWebElement enterPostcode = driver.FindElement(By.Name("postcode"));
            TypeWithSpeedSetting(enterPostcode, postcode, 100, driver);
            Thread.Sleep(1000);
            driver.FindElement(By.Id("postcode-search")).Click();
            Thread.Sleep(1000);
            IWebElement royal = driver.FindElement(By.Name("newAddressSelect"));
            royal.Click();
            Thread.Sleep(1000);
            SelectElement select = new SelectElement(royal);
            select.SelectByIndex(1);
            Thread.Sleep(2000);

            // Continue
            IWebElement continueButton = driver.FindElement(By.CssSelector("button[btntype='primary']"));
            int tries = 0;
            while (tries < 2)
            {
                if (IsElementPresentAndVisible(continueButton))
                {
                    continueButton.Click();
                    Thread.Sleep(2000);
                    tries++;
                }
                else
                {
                    break;
                }
            }
        }

        private static void DetailsCorrect(EdgeDriver driver, LabyrinthWebDriverScraperBase scraper)
        {
            string carValue = "4000";
            string miles = "10000";
            string vxs = "100";

            // Details correct
            driver.FindElement(By.CssSelector("label[class='adm-control-checkbox__item adm-control-checkbox__item--block']")).Click();
            Thread.Sleep(2000);

            // Where did you purchase the car
            driver.FindElement(By.CssSelector("div[class='adm-control-datepicker__input-suffix']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button[aria-label='2004']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button[aria-label='April 2004']")).Click();
            Thread.Sleep(2000);

            // Car value
            IWebElement value = driver.FindElement(By.Name("purchaseValue"));
            TypeWithSpeedSetting(value, carValue, 100, driver);
            Thread.Sleep(2000);

            continueButton(driver);

            // Level of cover
            driver.FindElement(By.XPath("//adm-card-section[@class='adm-card__section ng-star-inserted']//input[@id ='F']/ancestor::label")).Click();
            Thread.Sleep(2000);

            // Car using
            driver.FindElement(By.XPath("//adm-control-radio[@class='adm-control-radio']//input[@data-test='class-of-use-option-S']/ancestor::label")).Click();
            Thread.Sleep(2000);

            // Cover start date
            Dictionary<int, string> months = new Dictionary<int, string>
        {
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };

            DateTime coverstartDate = DateTime.Now.AddDays(10);
            string year = coverstartDate.Year.ToString();
            int monthKey = coverstartDate.Month;
            string month = months[monthKey];
            string day = coverstartDate.Day.ToString();

            driver.FindElement(By.CssSelector("div.adm-control-datepicker__input-suffix")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector($"button[aria-label='{year}']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector($"button[aria-label='{month} {year}']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector($"button[aria-label='{day} {month} {year}']")).Click();
            Thread.Sleep(1000);

            // Payment frequency
            driver.FindElement(By.CssSelector("label[data-test='radio-monthly']")).Click();
            Thread.Sleep(2000);
            continueButton(driver);

            // Annual mileage
            IWebElement mileage = driver.FindElement(By.CssSelector("select[data-test='annual-mileage-select']"));
            mileage.Click();
            SelectElement select = new SelectElement(mileage);
            Thread.Sleep(1000);
            select.SelectByValue(miles);
            Thread.Sleep(2000);

            // Car kept address
            IWebElement royal = driver.FindElement(By.Name("addressSelect"));
            royal.Click();
            Thread.Sleep(1000);
            SelectElement address = new SelectElement(royal);
            address.SelectByIndex(1);
            Thread.Sleep(2000);

            // Car kept overnight
            IWebElement overnight = driver.FindElement(By.CssSelector("select[data-test='parked-overnight-select']"));
            overnight.Click();
            Thread.Sleep(1000);
            SelectElement addressNight = new SelectElement(overnight);
            addressNight.SelectByText("Street");
            Thread.Sleep(2000);

            // Voluntary Excess
            if (IsElementPresentAndVisible(By.CssSelector("select[data-test='voluntary-excess-select']"), driver))
            {
                Click(By.CssSelector("select[data-test='voluntary-excess-select']"), driver);
                IWebElement VEx = driver.FindElement(By.CssSelector("select[data-test='voluntary-excess-select']"));
                Thread.Sleep(1000);
                SelectElement VxS = new SelectElement(VEx);
                VxS.SelectByText($"£{vxs}");
                Thread.Sleep(2000);
            }

            continueButton(driver);
        }

        private static void Drivers(EdgeDriver driver, LabyrinthWebDriverScraperBase scraper, Person person)
        {
            string email = "";
            string phoneNumber = "";
            string occupation = "";
            string industryJob = "";
            string drivingLincenceNumber = "";

            switch (person)
            {
                case Person.Principal:
                    //Data
                    email = "manuelpuentecrepo@gmail.com";
                    phoneNumber = "34618529725";
                    occupation = "Teacher";
                    industryJob = "Education";
                    drivingLincenceNumber = "MANUE753116SM9IJ";
                    break;
                case Person.Mother:
                    //Data
                    email = "mother@gmail.com";
                    phoneNumber = "34618805687";
                    occupation = "Mother Occupation";
                    industryJob = "Mother Industry";
                    drivingLincenceNumber = "REYES653116SM9IJ";
                    break;
            }
            

            // Has the driver lived in the UK
            driver.FindElement(By.XPath("//span[contains(text(), 'Yes')]/ancestor::label")).Click();
            driver.FindElement(By.CssSelector("button[data-test='done-button']")).Click();


            // Contact details
            // Email
            IWebElement mail = driver.FindElement(By.Name("email"));
            TypeWithSpeedSetting(mail, email, 100, driver);
            Thread.Sleep(2000);
            // Phone number

            IWebElement phone = driver.FindElement(By.CssSelector("input[formcontrolname='phone']"));
            TypeWithSpeedSetting(phone, phoneNumber, 100, driver);
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[data-test='done-button']")).Click();


            // Personal information
            // Marital Status
            IWebElement marital = driver.FindElement(By.CssSelector("select[data-test='marital-select']"));
            marital.Click();
            Thread.Sleep(1000);
            SelectElement addressNight = new SelectElement(marital);
            addressNight.SelectByText("Single");
            Thread.Sleep(2000);

            // Children under 16 age
            driver.FindElement(By.XPath(" //label[@for='hasChildren']/ancestor::div[@class='adm-form-group__content']//span[contains(text(), 'No')]")).Click();

            // Homeowner
            driver.FindElement(By.XPath(" //label[@data-test='homeowner-label']/ancestor::div[@class='adm-form-group__content']//span[contains(text(), 'No')]")).Click();

            // Number of vehicles
            IWebElement vehicles = driver.FindElement(By.CssSelector("select[data-test='vehicles-select']"));
            vehicles.Click();
            Thread.Sleep(1000);
            SelectElement numberVechicles = new SelectElement(vehicles);
            numberVechicles.SelectByText("2");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[data-test='done-button']")).Click();


            // Employment details
            // Main occupation
            IWebElement mainJob = driver.FindElement(By.CssSelector("input[placeholder='Enter occupation...']"));
            TypeWithSpeedSetting(mainJob, occupation, 100, driver);
            driver.FindElement(By.CssSelector("mat-option[class='mat-option mat-focus-indicator ng-star-inserted']")).Click();
            Thread.Sleep(2000);

            // Industry
            IWebElement industry = driver.FindElement(By.CssSelector("input[placeholder='Enter industry...']"));
            TypeWithSpeedSetting(industry, industryJob, 100, driver);
            driver.FindElement(By.CssSelector("mat-option[class='mat-option mat-focus-indicator ng-star-inserted']")).Click();
            Thread.Sleep(1000);

            // Self-employed driver
            driver.FindElement(By.XPath("//label[contains( text(), ' Is the driver self-employed? ')]/ancestor::div[@class='adm-form-group__content']//span[contains(text(), 'No')]")).Click();
            Thread.Sleep(1000);

            // Secondary occupation
            driver.FindElement(By.XPath(" //label[contains( text(), ' Do they have a secondary occupation? ')]/ancestor::div[@class='adm-form-group__content']//span[contains(text(), 'No')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button[data-test='done-button']")).Click();


            // Driving Licence
            // Full UK manual driving licence
            driver.FindElement(By.XPath("//label[@for = 'fullManual']/ancestor::div[@class='adm-form-group__content']//span[contains(text(), 'Yes')]")).Click();
            Thread.Sleep(1000);

            // Number
            IWebElement numberLicence = driver.FindElement(By.CssSelector("input[formcontrolname='licenceNumber']"));
            Thread.Sleep(1000);
            ClearInput(numberLicence);
            TypeWithSpeedSetting(numberLicence, drivingLincenceNumber, 100, driver);
            Thread.Sleep(2000);

            // Full UK manual driving licence
            IWebElement longLicence = driver.FindElement(By.CssSelector("select[data-test='heldForYears-select']"));
            longLicence.Click();
            Thread.Sleep(1000);
            SelectElement drivingTime = new SelectElement(longLicence);
            drivingTime.SelectByText("8");
            Thread.Sleep(2000);

            // Does the driver use another vehicle?
            IWebElement anotherVehicle = driver.FindElement(By.CssSelector("select[data-test='anotherVehicle-select']"));
            anotherVehicle.Click();
            Thread.Sleep(1000);
            SelectElement driverVehicle = new SelectElement(anotherVehicle);
            driverVehicle.SelectByText("No");
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[data-test='done-button']")).Click();


            //Driving History
            // Has the driver ever had insurance declined, cancelled or special terms imposed?
            driver.FindElement(By.XPath("//label[contains( text(), ' Has the driver ever had insurance declined, cancelled or special terms imposed? ')]/ancestor::div[@class='adm-form-group__content']//span[contains(text(), 'No')]")).Click();
            Thread.Sleep(1000);

            // Has the driver got any unspent non-motoring convictions?
            driver.FindElement(By.XPath("//label[@data-test='driver-convictions-question']/ancestor::div[@class='adm-form-group__content']//span[contains(text(), 'No')]")).Click();
            Thread.Sleep(1000);

            // Does the driver have any medical conditions or disabilities the DVLA must be made aware of?
            driver.FindElement(By.XPath("//label[@data-test='driver-dvla-question']/ancestor::div[@class='adm-form-group__content']//span[contains(text(), 'No')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.CssSelector("button[data-test='done-button']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@formcontrolname='noPreviousClaims']/ancestor::label")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@formcontrolname='noMotoringOffences']/ancestor::label")).Click();
            Thread.Sleep(1000);

            // Add second driver
            driver.FindElement(By.XPath("//input[@formcontrolname='addAnother']/ancestor::label//span[contains( text(), 'Yes')]")).Click();
            Thread.Sleep(1000);
            continueButton(driver);

        }
        private static void AddPassenger(Person familyMember, LabyrinthWebDriverScraperBase scraper, EdgeDriver driver)
        {
            string postcode = "";
            string lastName = "";
            string firstName = "";
            string year = "";
            string month = "";
            string day = "";
            string email = "";
            string phoneNumber = "";
            string occupation = "";
            string industryJob = "";
            string drivingLincenceNumber = "";

            switch (familyMember)
            {
                case Person.Principal:
                    // Data for Principal
                    postcode = "SW1A 1AA";
                    lastName = "Puente Crespo";
                    firstName = "Manuel";
                    DateTime date = DateTime.Now.AddYears(-72);
                    year = date.Year.ToString();
                    month = date.Month.ToString();
                    day = date.Day.ToString();
                    email = "javipiazza94@gmail.com";
                    phoneNumber = "34618809725";
                    occupation = "Software Engineer";
                    industryJob = "software";
                    drivingLincenceNumber = "JAVIE753116SM9IJ";
                    break;

                case Person.Mother:
                    // Data for Mother
                    postcode = "SW1A 1AB";
                    lastName = "Piazza de la Maza";
                    firstName = "Reyes";
                    DateTime date2 = DateTime.Now.AddYears(-66);
                    year = date2.Year.ToString();
                    month = date2.Month.ToString();
                    day = date2.Day.ToString();
                    email = "mother@gmail.com";
                    phoneNumber = "34618805687";
                    occupation = "Mother Occupation";
                    industryJob = "Mother Industry";
                    drivingLincenceNumber = "REYES653116SM9IJ";
                    break;

                default:
                    // Handle other cases or provide default values
                    postcode = "DefaultPostcode";
                    lastName = "DefaultLastName";
                    firstName = "DefaultFirstName";
                    break;
            }

            // Title
            IWebElement title = driver.FindElement(By.XPath("//label[@class='adm-control-button-grid__item ng-star-inserted']//span[text()=' Ms ' ]"));
            title.Click();
            Thread.Sleep(2000);

            // First name
            IWebElement name = driver.FindElement(By.CssSelector("input[formcontrolname='firstName']"));
            TypeWithSpeedSetting(name, firstName, 100, driver);
            Thread.Sleep(2000);

            // Last name
            IWebElement surname = driver.FindElement(By.Id("input[formcontrolname='lastName']"));
            TypeWithSpeedSetting(surname, lastName, 100, driver);
            Thread.Sleep(2000);

            //DOB
            IWebElement DOBDay = driver.FindElement(By.Id("dob-day"));
            TypeWithSpeedSetting(DOBDay, day, 100, driver);
            Thread.Sleep(1000);
            IWebElement DOBMonth = driver.FindElement(By.Id("dob-month"));
            TypeWithSpeedSetting(DOBMonth, month, 100, driver);
            Thread.Sleep(1000);
            IWebElement DOBYear = driver.FindElement(By.Id("dob-year"));
            TypeWithSpeedSetting(DOBYear, year, 100, driver);
            Thread.Sleep(2000);

            // Has the driver lived in the UK
            driver.FindElement(By.XPath("//span[contains(text(), 'Yes')]/ancestor::label")).Click();
            driver.FindElement(By.CssSelector("button[data-test='done-button']")).Click();

            // Contact details
            // Email
            IWebElement mail = driver.FindElement(By.Name("email"));
            TypeWithSpeedSetting(mail, email, 100, driver);
            Thread.Sleep(2000);


            // Phone number
            IWebElement phone = driver.FindElement(By.CssSelector("input[formcontrolname='phone']"));
            TypeWithSpeedSetting(phone, phoneNumber, 100, driver);
            Thread.Sleep(2000);
            driver.FindElement(By.CssSelector("button[data-test='done-button']")).Click();

            // Postcode
            IWebElement enterPostcode = driver.FindElement(By.Name("postcode"));
            TypeWithSpeedSetting(enterPostcode, postcode, 100, driver);
            Thread.Sleep(1000);
            driver.FindElement(By.Id("postcode-search")).Click();
            Thread.Sleep(1000);
            IWebElement royal = driver.FindElement(By.Name("newAddressSelect"));
            royal.Click();
            Thread.Sleep(1000);
            SelectElement select = new SelectElement(royal);
            select.SelectByIndex(1);
            Thread.Sleep(2000);
        }
        #region Helpers
        private static void continueButton(WebDriver driver)
        {
            driver.FindElement(By.CssSelector("button[btntype='primary']")).Click();
            Thread.Sleep(2000);
        }
        #endregion
    }



}

