using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

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
            
            Thread.Sleep(1000);

            ClickSearchIcon();

            searchBar = driver.FindElement(By.Id("search"));

            searchBar.SendKeys(Phrase);
            searchBar.SendKeys(Keys.Enter);

            Thread.Sleep(3000);

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
