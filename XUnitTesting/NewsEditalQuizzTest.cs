using CIMOBProject.Data;
using Microsoft.EntityFrameworkCore;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace XUnitTesting {
    public class NewsEditalQuizzTest {

        [Fact]
        public void TestNewsFirefox()
        {
            using (var driver = new FirefoxDriver((Path.GetDirectoryName
                                    (Assembly.GetExecutingAssembly().Location))))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateNews")).Click();
                string testTitle = "I'm a testing new!";
                string testContent = "I'm a News Test Content!";
                string testUrl = "www.testing.test";
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("NewsSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
                TestPublish(driver);
            }
        }

        [Fact]
        public void TestNewsChrome()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName
                                        (Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateNews")).Click();
                string testTitle = "I'm a testing new!";
                string testContent = "I'm a News Test Content!";
                string testUrl = "www.testing.test";
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("NewsSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
                TestPublish(driver);
            }
        }

        [Fact]
        public void TestNewsEdge()
        {
            using (var driver = new EdgeDriver(Path.GetDirectoryName
                               (Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateNews")).Click();
                string testTitle = "I'm a testing new!";
                string testContent = "I'm a News Test Content!";
                string testUrl = "www.testing.test";
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("NewsSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
                TestPublish(driver);
            }
        }

        private void TestPublish(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            IWebElement tableElement = driver.FindElement(By.TagName("tbody"));
            IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
            IWebElement lastRow = tableRow.Last();
            IWebElement clickCell = lastRow.FindElement(By.Id("Publish"));
            wait.Until(ExpectedConditions.TextToBePresentInElement(lastRow, "Não"));
            wait.Until(ExpectedConditions.ElementToBeClickable(clickCell));
            clickCell.Click();
            //refresh their values
            tableElement = driver.FindElement(By.TagName("tbody"));
            tableRow = tableElement.FindElements(By.TagName("tr"));
            lastRow = tableRow.Last();
            wait.Until(ExpectedConditions.TextToBePresentInElement(lastRow, "Sim"));
        }

        [Fact]
        public void TestEditalFirefox()
        {
            using (var driver = new FirefoxDriver((Path.GetDirectoryName
                                    (Assembly.GetExecutingAssembly().Location))))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateEdital")).Click();
                string testOpenDate = "01/01/2018";
                string testCloseDate = "10/10/2018";
                string testTitle = "I'm a testing edital!";
                string testContent = "I'm a edital Test Content!";
                string testUrl = "www.testing.testedital";
                driver.FindElement(By.Id("OpenDate")).SendKeys(testOpenDate);
                driver.FindElement(By.Id("CloseDate")).SendKeys(testCloseDate);
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("EditalSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
            }
        }

        [Fact]
        public void TestEditalChrome()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName
                                        (Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateEdital")).Click();
                string testOpenDate = "01/01/2018";
                string testCloseDate = "10/10/2018";
                string testTitle = "I'm a testing edital!";
                string testContent = "I'm a edital Test Content!";
                string testUrl = "www.testing.testedital";
                driver.FindElement(By.Id("OpenDate")).SendKeys(testOpenDate);
                driver.FindElement(By.Id("CloseDate")).SendKeys(testCloseDate);
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("EditalSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
            }
        }

        [Fact]
        public void TestEditalEdge()
        {
            using (var driver = new EdgeDriver(Path.GetDirectoryName
                               (Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateEdital")).Click();
                string testOpenDate = "01/01/2018";
                string testCloseDate = "10/10/2018";
                string testTitle = "I'm a testing edital!";
                string testContent = "I'm a edital Test Content!";
                string testUrl = "www.testing.testedital";
                driver.FindElement(By.Id("OpenDate")).SendKeys(testOpenDate);
                driver.FindElement(By.Id("CloseDate")).SendKeys(testCloseDate);
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("EditalSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
            }
        }

        [Fact]
        public void TestQuizzFirefox()
        {
            using (var driver = new FirefoxDriver((Path.GetDirectoryName
                                    (Assembly.GetExecutingAssembly().Location))))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateEdital")).Click();
                string testOpenDate = "01/01/2018";
                string testCloseDate = "10/10/2018";
                string testTitle = "I'm a testing edital!";
                string testContent = "I'm a edital Test Content!";
                string testUrl = "www.testing.testedital";
                driver.FindElement(By.Id("OpenDate")).SendKeys(testOpenDate);
                driver.FindElement(By.Id("CloseDate")).SendKeys(testCloseDate);
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("EditalSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
            }
        }

        [Fact]
        public void TestQuizzChrome()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName
                                        (Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateEdital")).Click();
                string testOpenDate = "01/01/2018";
                string testCloseDate = "10/10/2018";
                string testTitle = "I'm a testing edital!";
                string testContent = "I'm a edital Test Content!";
                string testUrl = "www.testing.testedital";
                driver.FindElement(By.Id("OpenDate")).SendKeys(testOpenDate);
                driver.FindElement(By.Id("CloseDate")).SendKeys(testCloseDate);
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("EditalSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
            }
        }

        [Fact]
        public void TestQuizzEdge()
        {
            using (var driver = new EdgeDriver(Path.GetDirectoryName
                               (Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateEdital")).Click();
                string testOpenDate = "01/01/2018";
                string testCloseDate = "10/10/2018";
                string testTitle = "I'm a testing edital!";
                string testContent = "I'm a edital Test Content!";
                string testUrl = "www.testing.testedital";
                driver.FindElement(By.Id("OpenDate")).SendKeys(testOpenDate);
                driver.FindElement(By.Id("CloseDate")).SendKeys(testCloseDate);
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("EditalSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));
            }
        }
    }
}
