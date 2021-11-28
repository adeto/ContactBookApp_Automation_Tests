using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;

namespace ContactBook_Web_App_Selenium_Automation_Tests
{
    public class ContactBook_Web_App_Selenium_Tests
    {
        ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://contactbook.adelinapetrova.repl.co/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        }

        [Test]
        public void Test_ContactBook_WebApp_ListContacts()
        {
            var linkContacts = driver.FindElementByXPath("//a[contains(text(),'Contacts')]");
            linkContacts.Click();

            var firstContact = driver.FindElementByXPath("//body/main[1]/div[1]/a[1]");
            firstContact.Click();

            System.Threading.Thread.Sleep(3000);

            var firstName = driver.FindElementByCssSelector("div :first-child table .fname td");
            var lastName = driver.FindElementByCssSelector("div :first-child table .lname td");
            var name = (firstName.Text + " " + lastName.Text);

            Assert.AreEqual("Steve Jobs", name);
        }

        [Test]
        public void Test_ContactBook_WebApp_FindContact_By_Keyword()
        {
            var linkSearchContact = driver.FindElementByXPath("//a[contains(text(),'Search')]");
            linkSearchContact.Click();

            var inputKeywordField = driver.FindElementByCssSelector("#keyword");

            inputKeywordField.Click();

            System.Threading.Thread.Sleep(3000);
            inputKeywordField.SendKeys("albert");

            var buttonSearch = driver.FindElementByCssSelector("#search");
            buttonSearch.Click();

            System.Threading.Thread.Sleep(3000);
            var firstName = driver.FindElementByXPath("//td[contains(text(),'Albert')]");
            var lastName = driver.FindElementByXPath("//td[contains(text(),'Einstein')]");
            var name = (firstName.Text + " " + lastName.Text);

            Assert.AreEqual("Albert Einstein", name);
        }

        [Test]
        public void Test_ContactBook_WebApp_FindContact_By_InvalidKeyword()
        {
            var linkSearchContact = driver.FindElementByXPath("//a[contains(text(),'Search')]");
            linkSearchContact.Click();

            var inputKeywordField = driver.FindElementByCssSelector("#keyword");
            inputKeywordField.Clear();
            inputKeywordField.Click();

            System.Threading.Thread.Sleep(3000);
            inputKeywordField.SendKeys("invalid2635");

            var buttonSearch = driver.FindElementByCssSelector("#search");
            buttonSearch.Click();

            System.Threading.Thread.Sleep(3000);
            var searchResult = driver.FindElementByCssSelector("#searchResult");

            Assert.AreEqual("No contacts found.", searchResult.Text);
        }

        [Test]
        public void Test_ContactBook_WebApp_Create_New_Contact()
        {
            var createContact = driver.FindElementByXPath("//a[contains(text(),'Create')]");
            createContact.Click();

            System.Threading.Thread.Sleep(3000);
            var firstName = driver.FindElementByCssSelector("#firstName");
            firstName.Click();
            firstName.SendKeys("Monika");

            var lastName = driver.FindElementByCssSelector("#lastName");
            lastName.Click();
            lastName.SendKeys("Stoyanova");

            var email = driver.FindElementByCssSelector("#email");
            email.Click();
            email.SendKeys("moni@gmail.com");

            var phone = driver.FindElementByCssSelector("#phone");
            phone.Click();
            phone.SendKeys("+359 87 98 78");

            var comments = driver.FindElementByCssSelector("#comments");
            comments.Click();
            comments.SendKeys("My best friend");

            var btnCreate = driver.FindElementByCssSelector("#create");
            btnCreate.Click();

            var btnContact = driver.FindElementByXPath("//a[contains(text(),'Contacts')]");
            btnContact.Click();

            var lastFirstNameResult = driver.FindElementByCssSelector("div :last-child table .fname td");
            var lastLastNameResult = driver.FindElementByCssSelector("div :last-child table .lname td");
            var lastNameResult = ((lastFirstNameResult.Text) + " " + (lastLastNameResult.Text));
            Assert.AreEqual("Monika Stoyanova", lastNameResult);
        }

        [Test]
        public void Test_ContactBook_WebApp_Create_New_Contact_With_InvalidEmail()
        {
            var createContact = driver.FindElementByXPath("//a[contains(text(),'Create')]");
            createContact.Click();

            System.Threading.Thread.Sleep(3000);
            var firstName = driver.FindElementByCssSelector("#firstName");
            firstName.Clear();
            firstName.Click();
            firstName.SendKeys("Andrey");

            var lastName = driver.FindElementByCssSelector("#lastName");
            lastName.Clear();
            lastName.Click();
            lastName.SendKeys("Lqpchev");

            var email = driver.FindElementByCssSelector("#email");
            email.Clear();
            email.Click();
            email.SendKeys("andrey@");

            var phone = driver.FindElementByCssSelector("#phone");
            phone.Clear();
            phone.Click();
            phone.SendKeys("+359 87 98 78");

            var comments = driver.FindElementByCssSelector("#comments");
            comments.Clear();
            comments.Click();
            comments.SendKeys("");

            var btnCreate = driver.FindElementByCssSelector("#create");
            btnCreate.Click();

            var errorMessage = driver.FindElementByCssSelector("body:nth-child(2) main:nth-child(3) > div.err:nth-child(1)");

            Assert.AreEqual("Error: Invalid email!", errorMessage.Text);
        }

        [Test]
        public void Test_ContactBook_WebApp_Create_New_Contact_With_EmptyName()
        {
            var createContact = driver.FindElementByXPath("//a[contains(text(),'Create')]");
            createContact.Click();

            System.Threading.Thread.Sleep(3000);
            var firstName = driver.FindElementByCssSelector("#firstName");
            firstName.Clear();
            firstName.Click();
            firstName.SendKeys("");

            var lastName = driver.FindElementByCssSelector("#lastName");
            lastName.Clear();
            lastName.Click();
            lastName.SendKeys("Lqpchev");

            var email = driver.FindElementByCssSelector("#email");
            email.Clear();
            email.Click();
            email.SendKeys("andrey@gmail.com");

            var phone = driver.FindElementByCssSelector("#phone");
            phone.Clear();
            phone.Click();
            phone.SendKeys("+359 87 98 78");

            var comments = driver.FindElementByCssSelector("#comments");
            comments.Clear();
            comments.Click();
            comments.SendKeys("");

            var btnCreate = driver.FindElementByCssSelector("#create");
            btnCreate.Click();

            var errorMessage = driver.FindElementByCssSelector("body:nth-child(2) main:nth-child(3) > div.err:nth-child(1)");

            Assert.AreEqual("Error: First name cannot be empty!", errorMessage.Text);
        }
        [TearDown]
        public void ShutDown()
        {
            driver.Quit();
        }
    }
}