namespace SeleniumTemplate {
    [Binding]
    public class OpenMenuStepDefinitions {
        private protected IWebDriver driver;

        Utils utils;
        HomePO homePO;

        public OpenMenuStepDefinitions(DriverFactory driverFactory) {
            driver = driverFactory.CurrentDriver;
            homePO = new HomePO(driverFactory);
            utils = new Utils(driverFactory);
        }

        [When("I click on a home button")]
        public void WhenIClickOnAHomeButton() {
            homePO.HomeButton.Click();
        }

        [When("I click on a nav bar button")]
        public void WhenIClickOnANavBarButton() {
            homePO.NavBarButton.Click();
        }

        [Then("Main menu is opened")]
        public void ThenMainMenuIsOpened() {
            Assert.That(homePO.Menu.Displayed);
        }

    }
}