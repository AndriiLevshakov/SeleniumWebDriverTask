using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriverTask
{
    public class CareersPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By keywordsField = By.XPath("//input[@placeholder='Keyword']");
        private readonly By allLocationSelector = By.XPath("//li[contains(@id, 'all_locations')]");
        private readonly By remoteOption = By.XPath("//label[contains(text(), 'Remote')]");
        private readonly By findButtonForTest1 = By.XPath("//button[@type='submit']");
        private readonly By sortingLabelDate = By.XPath("//label[contains(text(), 'Date')]");
        private readonly By latestResult = By.XPath("//div[@class='search-result__item-name-section']//a[contains(@class, 'search-result')]");
        private readonly By locationField = By.XPath("//span[@class='select2-selection__arrow']");

        public CareersPage(IWebDriver driver)
        {
            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void EnterKeywords(string keywords)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(keywordsField)).SendKeys(keywords);
        }

        public void OpenLocationDropDownMenu()
        {
            driver.FindElement(locationField).Click();
        }

        public void SelectAllLocations()
        {
            driver.FindElement(allLocationSelector).Click();
        }

        public void SelectRemoteOption()
        {
            driver.FindElement(remoteOption).Click();
        }

        public void ClickFindButton()
        {
            driver.FindElement(findButtonForTest1).Click();
        }

        public void ClickSortingLabelByDate()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(sortingLabelDate)).Click();
        }

        public void GetLatestResul()
        {
            var elements = driver.FindElements(latestResult);

            if(elements.Any())
            {
                elements.First().Click();
            }

            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public bool ConfirmThatProgrammingLanguageIsPresentOnThePage(string programmingLanguage)
        {
            return driver.PageSource.Contains(programmingLanguage);
        }
    }
}
