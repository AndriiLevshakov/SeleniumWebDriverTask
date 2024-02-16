using Microsoft.Extensions.Options;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumWebDriverTask
{    
    public class Tests
    {
        private IWebDriver driver;
        private WebDriverWait wait; 
        
        readonly string baseUrl = "https://www.epam.com/";

        private readonly By acceptButtonLocator = By.Id("onetrust-accept-btn-handler");

        private readonly By careersLink = By.XPath("//span[@class='top-navigation__item-text']/a[contains(@href, 'careers')]");

        private readonly By keywordsField = By.XPath("//input[@placeholder='Keyword']");

        private readonly By allLocationSelector = By.XPath("//li[contains(@id, 'all_locations')]");

        private readonly By remoteOption = By.XPath("//label[contains(text(), 'Remote')]");

        private readonly By findButtonForTest1 = By.XPath("//button[@type='submit']");

        private readonly By sortingLabelDate = By.XPath("//label[contains(text(), 'Date')]");

        private readonly By latestResult = By.XPath("//div[@class='search-result__item-name-section']//a[contains(@class, 'search-result')]");

        private readonly By magnifierIcon = By.XPath("//span[contains(@class, 'search-icon')]");

        private readonly By locationField = By.XPath("//span[@class='select2-selection__arrow']");

        private readonly By searchInput = By.XPath("//input[@type='search']");

        private readonly By findButtonForGlobalSearch = By.XPath("//span[contains(text(), 'Find')]");

        private readonly By searchResult = By.XPath("//section[contains(@data-config-path, 'content-container')]");


        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("--start-maximized");

            driver = new ChromeDriver(options);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
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

            wait.Until(ExpectedConditions.ElementToBeClickable(acceptButtonLocator)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(careersLink)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(keywordsField)).SendKeys(programmingLanguage);

            driver.FindElement(locationField).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(allLocationSelector)).Click();

            driver.FindElement(remoteOption).Click();

            driver.FindElement(findButtonForTest1).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(sortingLabelDate)).Click();

            var elements = driver.FindElements(latestResult);

            if (elements.Any())
            {
                elements.First().Click();
            }

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

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementToBeClickable(magnifierIcon)).Click();

            wait.Until(ExpectedConditions.ElementToBeClickable(searchInput)).SendKeys(searchKeyword);

            driver.FindElement(findButtonForGlobalSearch).Click();
          
            var searchResults = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(searchResult));

            Assert.That(searchResults.All(result => result.Text.ToLower().Contains(searchKeyword.ToLower())));
        }
    }
}