namespace SeleniumTemplate {
    internal class HomePO {
        private protected IWebDriver driver;

        public HomePO(DriverFactory driverFactory) {
            driver = driverFactory.CurrentDriver;
        }

        public IWebElement HomeButton => driver.FindElement(Locators.HomeButtonLocator);
        public IWebElement NavBarButton => driver.FindElement(Locators.NavBarButtonLocator);
        public IWebElement Menu => driver.FindElement(Locators.MenuLocator);

    }
}