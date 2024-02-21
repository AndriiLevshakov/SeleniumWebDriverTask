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
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public string BaseUrl { get; } = "https://www.epam.com/";

        private readonly By acceptButtonLocator = By.Id("onetrust-accept-btn-handler");
        private readonly By careersLink = By.XPath("//span[@class='top-navigation__item-text']/a[contains(@href, 'careers')]");
        private readonly By magnifierIcon = By.XPath("//span[contains(@class, 'search-icon')]");
        private readonly By searchInput = By.XPath("//input[@type='search']");
        private readonly By findButtonForGlobalSearch = By.XPath("//span[contains(text(), 'Find')]");
        private readonly By searchResult = By.XPath("//section[contains(@data-config-path, 'content-container')]");
        private readonly By aboutLink = By.XPath("//a[contains(@class, 'top-navigation__item')][contains(text(), 'About')]");
        private readonly By insightsLink = By.XPath("//a[contains(@class, 'top-navigation__item')][@href='/insights']");

        public HomePage(IWebDriver driver) 
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Navigate().GoToUrl(BaseUrl); 
        }

        public void ClickAcceptButton()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(acceptButtonLocator)).Click();
        }

        public void ClickCareersLink()
        {
            driver.FindElement(careersLink).Click();
        }

        public void ClickMagnifierIcon()
        {
            driver.FindElement(magnifierIcon).Click();
        }

        public void SendSearchInputToGlobalSearch(string searchWords)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(searchInput)).SendKeys(searchWords);
        }

        public void ClickFindButtonForGlobalSearch()
        {
            driver.FindElement(findButtonForGlobalSearch).Click();
        }

        public bool ConfirmPresenceOfSearchKeywordsOnThePage(string word)
        {
            var searchResults = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(searchResult));

            return searchResults.All(result => result.Text.ToLower().Contains(word.ToLower()));
        }

        public void ClickAboutLink()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(aboutLink)).Click();
        }

        public void ClickInsightsLink()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(insightsLink)).Click();
        }
    }
}
