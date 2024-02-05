using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject2
{

    [TestFixture]
    public class UnitTest
    {

        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void LoginTest()
        {
            // Uygulama sayfasýný aç
            driver.Navigate().GoToUrl("https://localhost:7042/Account/Login/");

            // Giriþ bilgilerini doldur
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys("smhglby17@gmail.com");

            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("12345678");

            // Giriþ butonuna týkla
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            // Baþarýlý giriþin kontrolü
            Assert.IsTrue(driver.Url.Contains("https://localhost:7042/Home/Index2"));
        }

        [Test]
        public void RegisterTest()
        {
            // Uygulama sayfasýný aç
            driver.Navigate().GoToUrl("https://localhost:7042/Account/Register/");

            // Gerekli elementleri bulma
            IWebElement firstNameInput = driver.FindElement(By.Id("firstname"));
            IWebElement lastNameInput = driver.FindElement(By.Id("lastname"));
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            SelectElement countryDropdown = new SelectElement(driver.FindElement(By.Id("country")));
            IWebElement registerButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            // Giriþ bilgilerini doldur
            firstNameInput.SendKeys("John");
            lastNameInput.SendKeys("Doe");
            emailInput.SendKeys("john.doe@example.com");
            passwordInput.SendKeys("password123");
            countryDropdown.SelectByText("Türkiye"); // veya countryDropdown.SelectByValue("country1");

            // Kayýt butonuna týkla
            registerButton.Click();

            // Baþarýlý kaydýn kontrolü (örneðin, bir sonraki sayfaya yönlendirildiðini kontrol edebilirsiniz)
            Assert.IsTrue(driver.Url.Contains("https://localhost:7042/Account/Login"));
        }

        [Test]
        public void ProjeTest()
        {
            // Önce giriþ yapmasý gerekli
            driver.Navigate().GoToUrl("https://localhost:7042/Account/Login/");

            // Giriþ bilgilerini doldur
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys("smhglby17@gmail.com");

            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("12345678");
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            // Giriþ butonuna týkla
            loginButton.Click();

            // Uygulama sayfasýný aç
            driver.Navigate().GoToUrl("https://localhost:7042/Project/ProjectAdd");

            // Gerekli elementleri bulma
            IWebElement projectTitleInput = driver.FindElement(By.Id("projectTitle"));
            IWebElement projectDescriptionTextarea = driver.FindElement(By.Id("projectDescription"));
            IWebElement projectBudgetInput = driver.FindElement(By.Id("projectBudget"));
            IWebElement projectTimeInput = driver.FindElement(By.Id("projectTime"));
            IWebElement projectLanguageInput = driver.FindElement(By.Id("projectLanguage"));
            IWebElement projectButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            // Giriþ bilgilerini doldur
            projectTitleInput.SendKeys("Test Project");
            projectDescriptionTextarea.SendKeys("Bu bir test projesidir.");
            projectBudgetInput.SendKeys("100");
            projectTimeInput.SendKeys("3 ay");
            projectLanguageInput.SendKeys("C#");

            // Kayýt butonuna týkla
            projectButton.Click();

            // Baþarýlý kaydýn kontrolü (örneðin, bir sonraki sayfaya yönlendirildiðini kontrol edebilirsiniz)
            Assert.IsTrue(driver.Url.Contains("https://localhost:7042/Project/ProjectList"));
        }

        [TearDown]
        public void Cleanup()
        {
            // WebDriver'ý kapat
            driver.Quit();
        }
    }
}