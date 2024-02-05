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
            // Uygulama sayfas�n� a�
            driver.Navigate().GoToUrl("https://localhost:7042/Account/Login/");

            // Giri� bilgilerini doldur
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys("smhglby17@gmail.com");

            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("12345678");

            // Giri� butonuna t�kla
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));
            loginButton.Click();

            // Ba�ar�l� giri�in kontrol�
            Assert.IsTrue(driver.Url.Contains("https://localhost:7042/Home/Index2"));
        }

        [Test]
        public void RegisterTest()
        {
            // Uygulama sayfas�n� a�
            driver.Navigate().GoToUrl("https://localhost:7042/Account/Register/");

            // Gerekli elementleri bulma
            IWebElement firstNameInput = driver.FindElement(By.Id("firstname"));
            IWebElement lastNameInput = driver.FindElement(By.Id("lastname"));
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            SelectElement countryDropdown = new SelectElement(driver.FindElement(By.Id("country")));
            IWebElement registerButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            // Giri� bilgilerini doldur
            firstNameInput.SendKeys("John");
            lastNameInput.SendKeys("Doe");
            emailInput.SendKeys("john.doe@example.com");
            passwordInput.SendKeys("password123");
            countryDropdown.SelectByText("T�rkiye"); // veya countryDropdown.SelectByValue("country1");

            // Kay�t butonuna t�kla
            registerButton.Click();

            // Ba�ar�l� kayd�n kontrol� (�rne�in, bir sonraki sayfaya y�nlendirildi�ini kontrol edebilirsiniz)
            Assert.IsTrue(driver.Url.Contains("https://localhost:7042/Account/Login"));
        }

        [Test]
        public void ProjeTest()
        {
            // �nce giri� yapmas� gerekli
            driver.Navigate().GoToUrl("https://localhost:7042/Account/Login/");

            // Giri� bilgilerini doldur
            IWebElement emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys("smhglby17@gmail.com");

            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("12345678");
            IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            // Giri� butonuna t�kla
            loginButton.Click();

            // Uygulama sayfas�n� a�
            driver.Navigate().GoToUrl("https://localhost:7042/Project/ProjectAdd");

            // Gerekli elementleri bulma
            IWebElement projectTitleInput = driver.FindElement(By.Id("projectTitle"));
            IWebElement projectDescriptionTextarea = driver.FindElement(By.Id("projectDescription"));
            IWebElement projectBudgetInput = driver.FindElement(By.Id("projectBudget"));
            IWebElement projectTimeInput = driver.FindElement(By.Id("projectTime"));
            IWebElement projectLanguageInput = driver.FindElement(By.Id("projectLanguage"));
            IWebElement projectButton = driver.FindElement(By.CssSelector("button[type='submit']"));

            // Giri� bilgilerini doldur
            projectTitleInput.SendKeys("Test Project");
            projectDescriptionTextarea.SendKeys("Bu bir test projesidir.");
            projectBudgetInput.SendKeys("100");
            projectTimeInput.SendKeys("3 ay");
            projectLanguageInput.SendKeys("C#");

            // Kay�t butonuna t�kla
            projectButton.Click();

            // Ba�ar�l� kayd�n kontrol� (�rne�in, bir sonraki sayfaya y�nlendirildi�ini kontrol edebilirsiniz)
            Assert.IsTrue(driver.Url.Contains("https://localhost:7042/Project/ProjectList"));
        }

        [TearDown]
        public void Cleanup()
        {
            // WebDriver'� kapat
            driver.Quit();
        }
    }
}