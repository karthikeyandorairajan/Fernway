using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System.Collections.Generic;

namespace FernwayAssessment2
{
    public class UnitTest1
    {
        string Test_url = "http://computer-database.gatling.io/computers";
        IWebDriver driver;
        private IWebElement AddNewComputerButton => driver.FindElement(By.Id("add"));
        private IWebElement ComputerNameTextBox => driver.FindElement(By.XPath("//input[@id='name']"));
        private IWebElement IntroducedTextBox => driver.FindElement(By.XPath("//input[@id='introduced']"));
        private IWebElement DiscontinuedTextBox => driver.FindElement(By.XPath("//input[@id='discontinued']"));
        private SelectElement CompanyTextBox => new SelectElement(driver.FindElement(By.XPath("//select[@id='company']")));
        private IWebElement AddComputerHeader => driver.FindElement(By.XPath("//h1[text()='Add a computer']"));
        private IWebElement CreateThisComputer => driver.FindElement(By.XPath("//input[@type='submit']"));
        private IWebElement SuccessMessage => driver.FindElement(By.XPath("//div[contains(@class,'alert-message')]"));
        private IWebElement CancelButton => driver.FindElement(By.XPath("//a[text()='Cancel']"));
        private IWebElement DateErrorMessage => driver.FindElement(By.XPath("//div[@class='clearfix error']"));
        private IWebElement SearchBox => driver.FindElement(By.XPath("//input[@id='searchbox']"));
        private IWebElement FilterByNameButton => driver.FindElement(By.Id("searchsubmit"));
        private IList<IWebElement> SearchResults => driver.FindElements(By.XPath("//table[@class='computers zebra-striped']/tbody/tr/td[1]"));
        private IWebElement FirstSearchResult => driver.FindElement(By.XPath("//table[@class='computers zebra-striped']/tbody/tr/td[1]/a"));
        private IWebElement NoSearchResults => driver.FindElement(By.ClassName("well"));
        private IWebElement EditComputerHeader => driver.FindElement(By.XPath("//h1[text()='Edit computer']"));
        private IWebElement DeleteThisComputer => driver.FindElement(By.XPath("//input[@type='submit']"));
        [SetUp]
        public void Start_Browser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [Test, Order(1)]
        public void Add_Computer_FillAllDetails()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            AddNewComputerButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Assert.IsTrue(AddComputerHeader.Displayed);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='name']")));
            Thread.Sleep(2000);
            string ComputerName = "hp120";
            
            //Postive Scenario
            ComputerNameTextBox.SendKeys(ComputerName);
            IntroducedTextBox.SendKeys("2020-12-05");
            DiscontinuedTextBox.SendKeys("2020-12-06");
            CompanyTextBox.SelectByValue("1");
            CreateThisComputer.Click();
            SuccessMessage.Text.Contains("Computer "+ComputerName+ "has been created");
        }
        [Test, Order(2)]
        public void Add_Computer_DateFormatVerification()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            AddNewComputerButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Assert.IsTrue(AddComputerHeader.Displayed);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='name']")));
            Thread.Sleep(2000);
            string ComputerName = "";

            //Negative Scenario
            ComputerNameTextBox.SendKeys(ComputerName);
            IntroducedTextBox.SendKeys("123456");
            DiscontinuedTextBox.SendKeys("12-12-2020");
            CompanyTextBox.SelectByValue("1");
            CreateThisComputer.Click();
            Assert.IsTrue(DateErrorMessage.Displayed);
                     
        }
        [Test, Order(3)]
        public void Add_Computer_FillSpecialCharacter()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            AddNewComputerButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Assert.IsTrue(AddComputerHeader.Displayed);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='name']")));
            Thread.Sleep(2000);
            string ComputerName = "hp&%$#@";
             //Postive Scenario
            ComputerNameTextBox.SendKeys(ComputerName);
            CreateThisComputer.Click();
            SuccessMessage.Text.Contains("Computer " + ComputerName + "has been created");
        }

        [Test, Order(4)]
        public void Add_Computer_CancelButtonVerification()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            AddNewComputerButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Assert.IsTrue(AddComputerHeader.Displayed);
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='name']")));
            Thread.Sleep(2000);
            string ComputerName = "hp120";
            ComputerNameTextBox.SendKeys(ComputerName);
            IntroducedTextBox.SendKeys("2020-12-05");
            DiscontinuedTextBox.SendKeys("2020-12-06");
            CompanyTextBox.SelectByValue("1");
            CancelButton.Click();
            try
            {
                driver.FindElement(By.XPath("//div[contains(@class,'alert-message')]"));
            }
            catch(NoSuchElementException e)
            {
                TestContext.WriteLine("No Computer Created" + e);
            }
        }

        [Test, Order(5)]
        public void Filter_Computer_Verification1()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            string FilterComputerName = "Amiga";
            SearchBox.SendKeys(FilterComputerName);
            FilterByNameButton.Click();
            foreach (var webElement in SearchResults)
            {
                Assert.IsTrue(webElement.Text.Contains(FilterComputerName));                    
            }
           
        }
        [Test, Order(6)]
        public void Filter_Computer_Verification2()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            string FilterComputerName = "xxx";
            SearchBox.SendKeys(FilterComputerName);
            FilterByNameButton.Click();
            Assert.IsTrue(NoSearchResults.Displayed);
        }
        [Test, Order(7)]
        public void Filter_Computer_Verification3()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            string FilterComputerName = "AN/FSQ-32";
            SearchBox.SendKeys(FilterComputerName);
            FilterByNameButton.Click();
            foreach (var webElement in SearchResults)
            {
                Assert.IsTrue(webElement.Text.Contains(FilterComputerName));
            }
        }
        [Test, Order(8)]
        public void Edit_Computer_Verification()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            string FilterComputerName = "Amiga";
            SearchBox.SendKeys(FilterComputerName);
            FilterByNameButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            FirstSearchResult.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Assert.IsTrue(EditComputerHeader.Displayed);
            string ComputerName = "amiga_120";

            //Postive Scenario
            ComputerNameTextBox.SendKeys(ComputerName);
            CreateThisComputer.Click();
            SuccessMessage.Text.Contains("Computer " + ComputerName + "has been updated");
        }
        [Test, Order(9)]
        public void Edit_Computer_CancelButtonVerification()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            string FilterComputerName = "Amiga";
            SearchBox.SendKeys(FilterComputerName);
            FilterByNameButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            FirstSearchResult.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Assert.IsTrue(EditComputerHeader.Displayed);
            string ComputerName = "amiga_120";
            ComputerNameTextBox.SendKeys(ComputerName);
            CancelButton.Click();
            try
            {
                driver.FindElement(By.XPath("//div[contains(@class,'alert-message')]"));
            }
            catch (NoSuchElementException e)
            {
                TestContext.WriteLine("No Computer Created" + e);
            }
        }
        [Test, Order(10)]
        public void Delete_Computer_Verification()
        {
            driver.Url = Test_url;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            string FilterComputerName = "Amiga";
            SearchBox.SendKeys(FilterComputerName);
            FilterByNameButton.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            FirstSearchResult.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
            Assert.IsTrue(EditComputerHeader.Displayed);
            string ComputerName = "amiga_120";
            DeleteThisComputer.Click();
            SuccessMessage.Text.Contains("Computer " + ComputerName + "has been deleted");
        }

        [TearDown]
        public void Close_Browser()
        {
            driver.Quit();
        }
    }
    
}
