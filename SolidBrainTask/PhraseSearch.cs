using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SolidBrainTask
{
    public class PhraseSearch
    {
        static IWebDriver driver = new ChromeDriver();
        static IWebElement searchBar;

        public string Phrase { get; set; }
        
        public bool Search()
        {
            string url = "https://new.abb.com";
            driver.Navigate().GoToUrl(url);

            var webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            webDriverWait.Until(ExpectedConditions.ElementExists(By.Id("PublicWrapper")));

            ClickSearchIcon();

            webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("search")));

            PhraseSending();

            webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("OneABBSearchList-item")));

            var items = driver.FindElements(By.ClassName("OneABBSearchList-item"));
            bool result = false;

            for(var i = 0; i < 3; i++)
            {
                result = FindPhraseInItem(items[i]);

                if(!result)
                {
                    break;
                }
            }
            
            driver.Quit();
            return result;
        }

        private void ClickSearchIcon()
        {
            driver.FindElement(By.ClassName("abb-icon__search")).Click();
        }

        private void PhraseSending()
        {
            searchBar = driver.FindElement(By.Id("search"));
            searchBar.SendKeys(Phrase);
            searchBar.SendKeys(Keys.Enter);
        }

        private bool FindPhraseInItem(IWebElement item)
        {
            var headingElement = item.FindElement(By.ClassName("OneABBSearchList-item-heading"));

            if (headingElement.Text.ToLower().Contains(Phrase.ToLower()))
            {
                return true;
            }

            var paragraphElements = item.FindElements(By.ClassName("OneABBSearchList-item-paragraph"));

            foreach(var paragraphElement in paragraphElements)
            {
                if (paragraphElement.Text.ToLower().Contains(Phrase.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
