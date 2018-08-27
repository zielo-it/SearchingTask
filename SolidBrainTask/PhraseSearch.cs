using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SolidBrainTask
{
    public class PhraseSearch
    {
        private readonly IWebDriver _driver = new ChromeDriver();

        public bool Search(string phrase)
        {
            const string url = "https://new.abb.com";
            _driver.Navigate().GoToUrl(url);

            var webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            webDriverWait.Until(ExpectedConditions.ElementExists(By.Id("PublicWrapper")));

            ClickSearchIcon();

            webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.Id("search")));

            PhraseSending(phrase);

            webDriverWait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("OneABBSearchList-item")));

            var result = IsPhraseProperlySearched(phrase, _driver.FindElements(By.ClassName("OneABBSearchList-item")));

            _driver.Quit();

            return result;
        }

        private bool IsPhraseProperlySearched(string phrase, IReadOnlyList<IWebElement> items)
        {
            for (var i = 0; i < 3; i++)
                if (!FindPhraseInItem(items[i], phrase))
                    return false;

            return true;
        }

        private void ClickSearchIcon()
        {
            _driver.FindElement(By.ClassName("abb-icon__search")).Click();
        }

        private void PhraseSending(string phrase)
        {
            var searchBar = _driver.FindElement(By.Id("search"));
            searchBar.SendKeys(phrase);
            searchBar.SendKeys(Keys.Enter);
        }

        private bool FindPhraseInItem(ISearchContext item, string phrase)
        {
            var headingElement = item.FindElement(By.ClassName("OneABBSearchList-item-heading"));

            if (headingElement.Text.ToLower().Contains(phrase.ToLower())) return true;

            var paragraphElements = item.FindElements(By.ClassName("OneABBSearchList-item-paragraph"));

            foreach (var paragraphElement in paragraphElements)
                if (paragraphElement.Text.ToLower().Contains(phrase.ToLower()))
                    return true;

            return false;
        }
    }
}