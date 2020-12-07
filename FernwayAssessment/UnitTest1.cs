using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace FernwayAssessment
{
     public class UnitTest1
    {       
        string Test_url = "https://the-internet.herokuapp.com/javascript_alerts";
        IWebDriver driver;
        private IWebElement ResultElement => driver.FindElement(By.XPath("//p[@id='result']"));
        private IWebElement JSAlertButton => driver.FindElement(By.XPath("//button[text()='Click for JS Alert']"));
        private IWebElement JSConfirmButton => driver.FindElement(By.XPath("//button[text()='Click for JS Confirm']"));
        private IWebElement JSPromptButton => driver.FindElement(By.XPath("//button[text()='Click for JS Prompt']"));
        [SetUp]
        public void Start_Browser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }
        [Test, Order(1)]
        public void Test_alert()
        {
            WebDriverWait wait =new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = Test_url;
            var ExpectedAlertText = "I am a JS Alert";
            var ExpectedResultText = "You successfuly clicked an alert";
            JSAlertButton.Click();
            IAlert alert = driver.SwitchTo().Alert();
            //Getting Alert Text
            string ActualAlerttext = alert.Text;
            
            //Validating Alert Text
            Assert.AreEqual(ExpectedAlertText, ActualAlerttext);

            //Clicking Ok button in Alert
            alert.Accept();
            string ActualResultText = ResultElement.Text;
            Assert.AreEqual(ExpectedResultText, ActualResultText);
        }
        [Test, Order(2)]
        public void Test_confirm()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = Test_url;
            var ExpectedAlertText = "I am a JS Confirm";
            var ExpectedResultText = "You clicked: Ok";
            JSConfirmButton.Click();
            IAlert alert = driver.SwitchTo().Alert();
            //Getting Alert Text
            string ActualAlerttext = alert.Text;

            //Validating Alert Text
            Assert.AreEqual(ExpectedAlertText, ActualAlerttext);

            //Clicking Ok button in Alert
            alert.Accept();
            string ActualResultText = ResultElement.Text;
            Assert.AreEqual(ExpectedResultText, ActualResultText);
        }
        [Test, Order(3)]
        public void Test_Dismiss_Confirm()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = Test_url;
            var ExpectedAlertText = "I am a JS Confirm";
            var ExpectedResultText = "You clicked: Cancel";
            JSConfirmButton.Click();
            IAlert alert = driver.SwitchTo().Alert();
            //Getting Alert Text
            string ActualAlerttext = alert.Text;

            //Validating Alert Text
            Assert.AreEqual(ExpectedAlertText, ActualAlerttext);

            //Clicking Ok button in Alert
            alert.Dismiss();
            string ActualResultText = ResultElement.Text;
            Assert.AreEqual(ExpectedResultText, ActualResultText);
        }

        [Test, Order(4)]
        public void Test_Prompt()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = Test_url;
            var ExpectedAlertText = "I am a JS prompt";
            var ExpectedResultText = "You entered: Hello";
            JSPromptButton.Click();
            IAlert alert = driver.SwitchTo().Alert();
            //Getting Alert Text
            string ActualAlerttext = alert.Text;

            //Filling Prompt box
            alert.SendKeys("Hello");

            //Clicking Ok button in Alert
            alert.Accept();

            //Validating Prompt Text
            Assert.AreEqual(ExpectedAlertText, ActualAlerttext);

            string ActualResultText = ResultElement.Text;
            Assert.AreEqual(ExpectedResultText, ActualResultText);
        }

        [Test, Order(5)]
        public void Test_Dismiss_Prompt()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Url = Test_url;
            var ExpectedAlertText = "I am a JS prompt";
            var ExpectedResultText = "You entered: null";
            JSPromptButton.Click();
            IAlert alert = driver.SwitchTo().Alert();
            //Getting Alert Text
            string ActualAlerttext = alert.Text;

            //Validating Alert Text
            Assert.AreEqual(ExpectedAlertText, ActualAlerttext);

            //Clicking Ok button in Alert
            alert.Dismiss();
            string ActualResultText = ResultElement.Text;
            Assert.AreEqual(ExpectedResultText, ActualResultText);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    
}
}
