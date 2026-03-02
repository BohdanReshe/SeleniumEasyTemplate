using OpenQA.Selenium.Chrome;

namespace SeleniumTemplate.Shared {
    public class DriverFactory : IDisposable {

        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected IJavaScriptExecutor js;

        private readonly Lazy<IWebDriver> currentWenDriverLazy;
        private boll isDisposed;

        public DriverFactory() {
            currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver());
            wait = SetWait();
            js = SetJS();
        }

        public IWebDriver CurrentDriver => currentWebDriverLazy.Value;
        public WebDriverWait CurrentWait => wait;
        public IJavaScriptExecutor CurrentJS => js;

        public string DownloadDir => Path.GetTempPath();

        public WebDriverWait SetWait() {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(Convert.ToDouble(TestContext.Parameters["Wait"])));
        }

        public IJavaScriptExecutor SetJS() {
            return (IJavaScriptExecutor)driver;
        }
        
        private IWebDriver CreateWebDriver() {
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
            chromeOptions.AddArguments("start-maximized");
            if (TestContext.Parameters["Headless"] == "true") {
                chromeOptions.AddArguments("headless");
                chromeOptions.AddArguments("--window-size=1920,1080");
            }
            chromeOptions.AddUserProfilePreference("download.default_directory", DownloadDir);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);
            chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Convert.ToDouble(TestContext.Parameters["PageLoad"]));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Convert.ToDouble(TestContext.Parameters["ImplicitWait"]));
            return driver;
        }

        public void Dispose() {
            if (isDisposed) {
                return
            }
            if (currentWebDriverLazy.IsValueCreated) {
                CurrentDriver.Quit();
            }
            isDisposed = true;
        }
    }
}