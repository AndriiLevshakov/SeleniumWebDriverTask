﻿using OpenQA.Selenium;
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
    public class InsightsPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        
        private readonly By buttonToSwipeCarousel = By.XPath("//button[@class='slider__left-arrow slider-navigation-arrow']");
        private readonly By activeSlideText = By.XPath("//div[@class='owl-item active']//span[@class='museo-sans-light']");
        private readonly By activeSlideReadMoreButton = By.XPath("//div[@class='owl-item active']//a[@tabindex='0']");
        private readonly By articleText = By.XPath("//span[@class='font-size-80-33']/span[@class='museo-sans-light']");

        private string carouselArticleTitle;
        private string articleHeader;

        public InsightsPage(IWebDriver driver)
        {
            this.driver = driver;

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public void SwipeCarouselTwice()
        { 
            for (int i = 0; i < 4; i++)
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(buttonToSwipeCarousel)).Click();
            }

            Thread.Sleep(2000);

            carouselArticleTitle = driver.FindElement(activeSlideText).Text;            
        }

        public void ClickReadMoreButton()
        {
            driver.FindElement(activeSlideReadMoreButton).Click();            
        }

        public bool CheckIfArticleTextContainsActiveSliderText()
        {
            articleHeader = wait.Until(ExpectedConditions.ElementIsVisible(articleText)).Text;

            if (articleHeader.Contains(carouselArticleTitle))
            {
                return true;
            }

            return false;
        }
    }
}