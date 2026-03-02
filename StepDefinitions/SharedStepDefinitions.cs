namespace SeleniumTemplate {
    [Binding]
    public class SharedStepDefinitions {
        private protected IWebDriver driver;

        Utils utils;

        public SharedStepDefinitions(DriverFactory driverFactory) {
            driver = driverFactory.CurrentDriver;
            utils = new Utils(driverFactory);
        }

        [Given("I have accessed the page")]
        public void GivenIHaveAccessedThePage() {
            driver.Navigate().GoToUrl(TestContext.Parameters["URL"]);
            Assert.That(driver.Url.Contains("https://www.page.com"));
        }
        
    }
}