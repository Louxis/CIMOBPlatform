using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace XUnitTesting
{
    public class InterviewTest
    {
        [Fact]
        public void TestNewsFirefox() {
            using (var driver = new FirefoxDriver((Path.GetDirectoryName
                                    (Assembly.GetExecutingAssembly().Location)))) {
                driver.Navigate().GoToUrl
                       (@"https://localhost:44334/");
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                wait.Until
                         (ExpectedConditions.ElementToBeClickable(By.Id("News")));
                ApplicationControllerTest.LogIn(driver, "testemployee@cimob.pt", "teste12");
                driver.FindElement(By.Id("Application")).Click();
                IWebElement tableElement = driver.FindElement(By.TagName("tbody"));
                IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
                IWebElement lastRow = tableRow.Last();
                /*IWebElement evalButton = lastRow.FindElement(By.ClassName("eval"));
                if(evalButton != null) {
                    evalButton.Click();
                }*/
                //refresh
                tableElement = driver.FindElement(By.TagName("tbody"));
                tableRow = tableElement.FindElements(By.TagName("tr"));
                lastRow = tableRow.Last();
                IWebElement interviewButton = lastRow.FindElement(By.ClassName("interview"));                 
                wait.Until(ExpectedConditions.ElementToBeClickable(interviewButton));
                interviewButton.Click();
                /*DateTime testDate = DateTime.Today;
                driver.FindElement(By.Id("date")).Click();
                driver.FindElement(By.Id("date")).SendKeys(testDate.ToShortDateString());           */
                //CHECK THIS
                wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("submit"))));


                /*driver.FindElement(By.Id("News")).Click();
                driver.FindElement(By.Id("CreateNews")).Click();
                string testTitle = "I'm a testing new!";
                string testContent = "I'm a News Test Content!";
                string testUrl = "www.testing.test";
                driver.FindElement(By.Id("Title")).SendKeys(testTitle);
                driver.FindElement(By.Id("TextContent")).SendKeys(testContent);
                driver.FindElement(By.Id("link-text")).SendKeys(testUrl);
                driver.FindElement(By.Id("NewsSubmit")).Click();
                wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.ClassName("table")), testTitle));*/
            }
        }
    }
}
