using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace SeleniumWebDriverTask
{    
    public class Tests
    {
        IWebDriver driver;
        string baseUrl = "https://www.epam.com/";

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized");
            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }

        [TestCase("C#", "All Locations")]
        public void Test1(string programmingLanguage, string location)
        {
            driver.Navigate().GoToUrl(baseUrl);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            IWebElement acceptButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("onetrust-accept-btn-handler")));
            acceptButton.Click();

            IWebElement careersLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@class=\"top-navigation__item-text\"]/a[contains(@href, 'careers')]")));
            careersLink.Click();

            IWebElement keywordsField = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@placeholder=\"Keyword\"]")));
            keywordsField.SendKeys(programmingLanguage);


            IWebElement locationField = driver.FindElement(By.XPath("//span[@class=\"select2-selection__arrow\"]"));
            locationField.Click();

            IWebElement allLocationSelector = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//li[contains(@id, 'all_locations')]")));
            allLocationSelector.Click();

            IWebElement remoteOption = driver.FindElement(By.XPath("//label[contains(text(), 'Remote')]"));
            remoteOption.Click();

            IWebElement findButton = driver.FindElement(By.XPath("//button[@type=\"submit\"]"));
            findButton.Click();

            IWebElement sortingLabelDate = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//label[contains(text(), 'Date')]")));
            sortingLabelDate.Click();

            IWebElement latestResult = driver.FindElement(By.XPath("//ul[@class=\"search-result__list\"]/li/div[3]/div/div/div[2]/a"));
            latestResult.Click();

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

            bool isLanguagePresent = driver.PageSource.Contains(programmingLanguage);

            Assert.That(isLanguagePresent, $"Programming language '{programmingLanguage}' not found on the page");
        }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Task2_GlobalSearch(string searchKeyword)
        {
            driver.Navigate().GoToUrl(baseUrl);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement magnifierIcon = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//span[contains(@class, 'search-icon')]")));
            magnifierIcon.Click();

            IWebElement searchInput = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@type=\"search\"]")));
            searchInput.SendKeys(searchKeyword);

            IWebElement findButton = driver.FindElement(By.XPath("//span[contains(text(), 'Find')]"));
            findButton.Click();

            // Validate using LINQ
            var searchResults = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//section[contains(@data-config-path, 'content-container')]")));
            Assert.That(searchResults.All(result => result.Text.ToLower().Contains(searchKeyword.ToLower())));
        }
    }
}