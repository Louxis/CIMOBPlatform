using System.Collections.Generic;
using System.Linq;
using Xunit;
using CIMOBProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using OpenQA.Selenium.Chrome;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace XUnitTesting {
    public class ApplicationControllerTest
    {
        private IConfigurationRoot _configuration;

        // represents database's configuration
        private DbContextOptions<ApplicationDbContext> _options;
        private ApplicationDbContext _context;

        public ApplicationControllerTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_configuration.GetConnectionString("DefaultConnection"))
                .Options;
        }



#pragma warning disable xUnit1013 // Public method should be marked as test
        public static void LogIn(IWebDriver driver, string email, string password) {
#pragma warning restore xUnit1013 // Public method should be marked as test
            IWebElement logIn = driver.FindElement(By.Id("LogIn"));
            if (logIn == null) {
                IWebElement logOut = driver.FindElement(By.Id("LogOut"));
                logOut.Click();
            }
            logIn.Click();
            IWebElement emailW = driver.FindElement(By.Id("Email"));
            IWebElement passwordW = driver.FindElement(By.Id("Password"));
            IWebElement submit = driver.FindElement(By.Id("Submit"));
            emailW.SendKeys(email);
            passwordW.SendKeys(password);
            submit.Click();
        }

        [Fact]
        public void TestWithChromeDriverCreateApplication()
        {
            using (var driver = new ChromeDriver
                  (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                 (@"https://localhost:44334/");
                LogIn(driver, "test4@test", "teste14");
                IWebElement application = driver.FindElement(By.Id("Application"));
                application.Click();

                IWebElement bilateral1 = driver.FindElement(By.Id("Bilateral1"));
                IWebElement bilateral2 = driver.FindElement(By.Id("Bilateral2"));
                IWebElement bilateral3 = driver.FindElement(By.Id("Bilateral3"));

                SelectElement selectBilateral1 = new SelectElement(bilateral1);
                selectBilateral1.SelectByIndex(0);

                SelectElement selectBilateral2 = new SelectElement(bilateral2);
                selectBilateral1.SelectByIndex(1);

                SelectElement selectBilateral3 = new SelectElement(bilateral3);
                selectBilateral1.SelectByIndex(2);

                IWebElement motivation = driver.FindElement(By.Id("Motivation"));
                motivation.SendKeys("Testing selenium");

                IWebElement submitApplication = driver.FindElement(By.Id("Submit"));
                submitApplication.Click();

                IWebElement details = driver.FindElement(By.Id("Details"));
                details.Click();

                IWebElement currentState = driver.FindElement(By.Id("CurrentState"));

                ExpectedConditions.ElementExists(By.Id("CurrentState"));
            }
        }

        [Fact]
        public void TestWithChromeDriverEvaluateApplication()
        {
            using (var driver = new ChromeDriver
                  (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                 (@"https://localhost:44334/");
                LogIn(driver, "testemployee@cimob.pt", "teste12");
                IWebElement application = driver.FindElement(By.Id("Application"));
                application.Click();

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                

                IWebElement table = driver.FindElement(By.TagName("tbody"));
                IList<IWebElement> tableRows = table.FindElements(By.TagName("tr"));
                IList<IWebElement> rowTD;
                foreach (IWebElement row in tableRows)
                {
                    rowTD = row.FindElements(By.TagName("td"));

                    if (rowTD.First().FindElement(By.Id("UserFullName")).Text.Equals("Teste User 4"))
                    {
                        rowTD.Last().FindElement(By.Id("Evaluate")).Click();
                        break;
                    }
                }

                table = driver.FindElement(By.TagName("tbody"));
                tableRows = table.FindElements(By.TagName("tr"));

                foreach (IWebElement row in tableRows)
                {
                    rowTD = row.FindElements(By.TagName("td"));
                    if (rowTD.First().FindElement(By.Id("UserFullName")).Text.Equals("Teste User 4"))
                    {
                        var clickableElement = wait.Until
                            (ExpectedConditions.ElementToBeClickable(rowTD.Last().FindElement(By.Id("Edit"))));
                        clickableElement.Click();
                        break;
                    }
                }

                driver.FindElement(By.Id("ArithmeticMean")).Clear();
                driver.FindElement(By.Id("ArithmeticMean")).SendKeys("20");
                driver.FindElement(By.Id("MotivationLetter")).Clear();
                driver.FindElement(By.Id("MotivationLetter")).SendKeys("20");
                driver.FindElement(By.Id("Enteriview")).Clear();
                driver.FindElement(By.Id("Enteriview")).SendKeys("20");
                SelectElement applicationStat = new SelectElement(driver.FindElement(By.Id("ApplicationStat")));
                applicationStat.SelectByIndex(1);
                driver.FindElement(By.Id("Submit")).Click();

                table = driver.FindElement(By.TagName("tbody"));
                tableRows = table.FindElements(By.TagName("tr"));

                foreach (IWebElement row in tableRows)
                {
                    rowTD = row.FindElements(By.TagName("td"));

                    if (rowTD.First().FindElement(By.Id("UserFullName")).Text.Equals("Teste User 4"))
                    {
                        ExpectedConditions.TextToBePresentInElement(rowTD[2].FindElement(By.Id("ApplicationStat")), "Seriação Pendente");
                        break;
                    }
                }
            }
        }


        [Fact]
        public void TestWithChromeDriverSeriation()
        {
            using (var driver = new ChromeDriver
                  (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                 (@"https://localhost:44334/");
                LogIn(driver, "testemployee@cimob.pt", "teste12");
                IWebElement application = driver.FindElement(By.Id("Application"));
                application.Click();

                driver.FindElement(By.Id("Seriation")).Click();
                ExpectedConditions.ElementExists(By.TagName("td"));
                
            }
        }

        [Fact]
        public void TestWithChromeDriverCheckApplicationHistory()
        {
            using (var driver = new ChromeDriver
                  (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                 (@"https://localhost:44334/");
                LogIn(driver, "test4@test", "teste14");

                driver.FindElement(By.Id("Details")).Click();

                driver.FindElement(By.Id("ApplicationHistory"));
                ExpectedConditions.ElementExists(By.TagName("td"));
            }
        }


        //[Fact]
        //public void TestWithFoxDriver()
        //{
        //    using (var driver = new FirefoxDriver
        //          (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
        //    {
        //        driver.Navigate().GoToUrl
        //         (@"https://www.google.pt/");
        //        /*IWebElement tableElement = driver.FindElement(By.TagName("tbody"));
        //        IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
        //        IList<IWebElement> rowTD;
        //        foreach (IWebElement row in tableRow)
        //        {
        //            rowTD = row.FindElements(By.TagName("td"));
        //            if (rowTD.Last().FindElement(By.Id("Details")) != null)
        //            {
        //                rowTD.Last().FindElement(By.Id("Details")).Click();
        //                break;
        //            }
        //        }*/
        //    }
        //}

        //[Fact]
        //public void TestWithEdgeDriver()
        //{
        //    using (var driver = new EdgeDriver
        //          (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
        //    {
        //        driver.Navigate().GoToUrl
        //         (@"https://localhost:44334/Applications/");
        //        IWebElement tableElement = driver.FindElement(By.TagName("tbody"));
        //        IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
        //        IList<IWebElement> rowTD;
        //        foreach (IWebElement row in tableRow)
        //        {
        //            rowTD = row.FindElements(By.TagName("td"));
        //            if (rowTD.Last().FindElement(By.Id("Details")) != null)
        //            {
        //                rowTD.Last().FindElement(By.Id("Details")).Click();
        //                break;
        //            }
        //        }
        //    }
        //}

        [Fact]
        public void TestAllChromeDriverTests()
        {
            TestWithChromeDriverCreateApplication();
            TestWithChromeDriverEvaluateApplication();
            TestWithChromeDriverSeriation();
            TestWithChromeDriverCheckApplicationHistory();
        }
    }
}
