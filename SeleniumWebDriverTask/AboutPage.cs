﻿using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebDriverTask
{
    public class AboutPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        private readonly By downloadButton = By.XPath("//a[contains(@href, 'EPAM_Corporate_Overview')]");        
        private readonly By SectionWhichHelpToSeeDownloadButton = By.XPath("//span[contains(text(), 'MEET')]");

        public AboutPage(IWebDriver driver)
        { 
            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void ClickDownloadButton()
        {
            var actions = new Actions(driver);

            actions.MoveToElement(driver.FindElement(SectionWhichHelpToSeeDownloadButton)).Perform();
            
            wait.Until(ExpectedConditions.ElementToBeClickable(downloadButton)).Click();
        }
        
        public bool CheckIfDownloaded(string fileName)
        {
            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadPath = Path.Combine(userPath, "Downloads");

            DirectoryInfo dirInfo = new DirectoryInfo(downloadPath);

            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string filePath = Path.Combine(downloadPath, fileName);

            return File.Exists(filePath);
        }
    }
}