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
        
        [SetUp]
        public void Setup()
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadPath = Path.Combine(userPath, "Downloads");

            var options = new ChromeOptions();

            options.AddArguments("--headless");            
            options.AddArguments("--window-size=1920,1080");
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            

            driver = new ChromeDriver(options);
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }

        [TestCase("C#", "All Locations")]
        public void Test1_Careers(string programmingLanguage, string location)
        {
            var homePage = new HomePage(driver);

            

            homePage.ClickAcceptButton();

            homePage.ClickCareersLink();

            var careersPage = new CareersPage(driver);

            careersPage.EnterKeywords(programmingLanguage);

            careersPage.OpenLocationDropDownMenu();

            careersPage.SelectAllLocations();

            careersPage.SelectRemoteOption();

            careersPage.ClickFindButton();

            careersPage.ClickSortingLabelByDate();

            careersPage.GetLatestResul();

            Assert.That(careersPage.ConfirmThatProgrammingLanguageIsPresentOnThePage(programmingLanguage),
                $"Programming language '{programmingLanguage}' not found on the page");
        }

        [TestCase("BLOCKCHAIN")]
        [TestCase("Cloud")]
        [TestCase("Automation")]
        public void Test2_GlobalSearch(string searchKeyword)
        {
            var homePage = new HomePage(driver);

            homePage.ClickMagnifierIcon();

            homePage.SendSearchInputToGlobalSearch(searchKeyword);

            homePage.ClickFindButtonForGlobalSearch();
          
            Assert.That(homePage.ConfirmPresenceOfSearchKeywordsOnThePage(searchKeyword));
        }

        [Test]
        public void Test3_ValidateFileDownload()
        {
            var homePage = new HomePage(driver);            

            var aboutPage = new AboutPage(driver);

            homePage.ClickAcceptButton();

            homePage.ClickAboutLink();

            aboutPage.ClickDownloadButton();

            Assert.That(aboutPage.CheckIfDownloaded("EPAM_Corporate_Overview_Q4_EOY.pdf"));
        }

        [Test]
        public void Test4_ValidateArticleTitleInCarousel()
        {
            var homePage = new HomePage(driver);            

            var insightsPage = new InsightsPage(driver);

            homePage.ClickAcceptButton();

            homePage.ClickInsightsLink();

            insightsPage.SwipeCarouselTwice();

            insightsPage.ClickReadMoreButton();

            Assert.That(insightsPage.CheckIfArticleTextContainsActiveSliderText());
        }
    }
}