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

        public static void LogIn(IWebDriver driver, string email, string password) {
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
        public void TestWithChromeDriverApplication()
        {
            using (var driver = new ChromeDriver
                  (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                 (@"https://localhost:44334/");
                LogIn(driver, "test@test", "teste12");
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
        public void TestWithFoxDriver()
        {
            using (var driver = new FirefoxDriver
                  (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                 (@"https://www.google.pt/");
                /*IWebElement tableElement = driver.FindElement(By.TagName("tbody"));
                IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
                IList<IWebElement> rowTD;
                foreach (IWebElement row in tableRow)
                {
                    rowTD = row.FindElements(By.TagName("td"));
                    if (rowTD.Last().FindElement(By.Id("Details")) != null)
                    {
                        rowTD.Last().FindElement(By.Id("Details")).Click();
                        break;
                    }
                }*/
            }
        }

        [Fact]
        public void TestWithEdgeDriver()
        {
            using (var driver = new EdgeDriver
                  (Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl
                 (@"https://localhost:44334/Applications/");
                IWebElement tableElement = driver.FindElement(By.TagName("tbody"));
                IList<IWebElement> tableRow = tableElement.FindElements(By.TagName("tr"));
                IList<IWebElement> rowTD;
                foreach (IWebElement row in tableRow)
                {
                    rowTD = row.FindElements(By.TagName("td"));
                    if (rowTD.Last().FindElement(By.Id("Details")) != null)
                    {
                        rowTD.Last().FindElement(By.Id("Details")).Click();
                        break;
                    }
                }
            }
        }
    }
}
