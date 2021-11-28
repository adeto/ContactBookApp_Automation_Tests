using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace ContactBookApp_Android_Automation_Tests
{
    public class ContactBook_Android_Automation_Tests
    {
        private AndroidDriver<AndroidElement> driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Android" };
            appiumOptions.AddAdditionalCapability("app", @"C:\Adi\Automation QA\C#\Exam\contactbook-androidclient.apk");
            driver = new AndroidDriver<AndroidElement>(new Uri("http://[::1]:4723/wd/hub"), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
        }

        [Test]
        public void Test_Android_ContactBookApp_OpenApp_And_Search()
        {
            var inputSearchBookField = driver.FindElementById("contactbook.androidclient:id/editTextApiUrl");
            inputSearchBookField.Click();
            inputSearchBookField.Clear();
            inputSearchBookField.SendKeys("https://contactbook.adelinapetrova.repl.co/api");

            var buttonConnect = driver.FindElementById("contactbook.androidclient:id/buttonConnect");
            buttonConnect.Click();

            var inputSerachContact = driver.FindElementById("contactbook.androidclient:id/editTextKeyword");
            inputSerachContact.Click();
            inputSerachContact.SendKeys("steve");

            var buttonSearch = driver.FindElementById("contactbook.androidclient:id/buttonSearch");
            buttonSearch.Click();

            //wait
            var firstName = driver.FindElementById("contactbook.androidclient:id/textViewFirstName");
            var lastName = driver.FindElementById("contactbook.androidclient:id/textViewLastName");

            Assert.AreEqual("Steve Jobs", firstName.Text + " " + lastName.Text);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}