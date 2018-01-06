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

        [Fact]
        public void TestWithChromeDriver()
        {
            using (var driver = new ChromeDriver
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
