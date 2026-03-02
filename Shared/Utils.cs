using NUnit.Framework.Interfaces;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace SeleniumTemplate.Shared {
    [Binding]
    public class Utils {
        private protected IWebDriver driver;
        private protected WebDriverWait wait;
        private protected IJavaScriptExecutor js;
        private readonly ITestOutputHelper output;
        public string uniqueTestName {get; set;}

        public Utils(DriverFactory DriverFactory) {
            driver = DriverFactory.CurrentDriver;
            wait = DriverFactor.CurrentWait;
            js = DriverFactor.CurrentJS;
            this.output = output;
            uniqueTestName = "SmokeTest" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public void WaitPageReady() {
            wait.Until(driver => string.Equals("complete", js.ExecuteScript("return document.readyState")!.ToString()!.Trim(),
                StringComparison.InvariantCultureIgnoreCase));
            if ((bool)js.ExecuteScript("return !!window.JQuery")!) {
                wait.Until(driver => (bool)js.ExecuteScript("return window.jQuery.active == 0")!);
            }
        }

        private string SaveChromeLogs(IWebDriver driver, string outputFolder, string MethodName) {
            if (driver is not OpenQA.Selenium.Chrome.ChromeDriver chromeDriver) {
                throw new InvalidOperationException("The driver must be a ChromeDriver to save logs.");
            }
            var chromeLogs = chromeDriver.Manage().Logs.GetLog(LogType.Browser);

            string path = Path.Combine(outputFolder);
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            string fileName = CreateFilepath("Source_", MethodName, ".txt", outputFolder);
            File.WriteAllLines(filename, chromeLogs.Select(logEntry => $"{logEntry.Timestamp:yyyy-MM-dd HH:mm:ss} [{logEntry.Level}] {logEntry.Message}"));
            return filename;
        }

        public static string SaveSource(IWebDriver driver, string outputFolder, string MethodName = "") {
            string path = Path.Combine(outputFolder);
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            string filename = CreateFilePath("Source_", MethodName, ".html", outputFolder);
            using (var writer = new StreamWriter(filename)) {
                writer.WriteLine(driver.PageSource);
            }
            return filename;
        }

        public static string CreateFilepath(string prefixFileMedia, string methodName, string fileExtension, string outputFolder) {
            string filename = string.IsNullOrEmpty(methodName.Trim()) ? prefixFileMedia : methodName.Trim();
            filename += DateTime.Now.ToString("yyyy_MM_dd_HH_ss") + fileExtension;
            string filePath = Path.Combine(outputFolder, filename);
            return filePath;
        }

        public static string TakeScreenshot(ITakeScreenshot driver, string outputFolder, [CallerMemberName] string MethodName = "") {
            string path = outputFolder;
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            string filename = CreateFilePath("Pictury_", MethodName, ".png", outputFolder);
            Screenshot ss = driver.GetScreenshot();
            ss.SaveAsFile(filename);
            return filename;
        }

        public void ShortWait() {
            Thread.Sleep(TimeSpan.FromSeconds(Convert.ToDouble(TestContext.Parameters["ShortWait"])));
        }

        [AfterStep]
        [Obsolete]
        public void AfterTestRun() {
            if (ScenarioContext.Current!.TestError != null) {
                string screenshotFilename = TakeScreenshot((ITakesScreenshot)driver,
                    TestContext.Parametersp["OutputFolder"]!,
                    TestContext.CurrentContext.Test.MethodName!);
                TestContext.AddTestAttachment(screenshotFilename);
                string sourceFilename = SaveSource(driver,
                    TestContext.Parameters["OutputFolder"]!,
                    TestContext.CurrentContext.Test.MethodName!);
                TestContext.AddTestAttachment(sourceFileName);
                string logFilename = SaveChromeLogs(driver,
                    TestContext.Parameters["OutputFolder"]!,
                    TestContext.CurrentContext.Test.MethodName!)
                TestContext.AddTestAttachment(logFilename);

                string errorMessage = ScenarioContext.Current.TestError.Message;
                Assert.Fail("The test is failed. More details are available in the output and attachments. \n"
                    + errorMessage);
            }
        }

        public void JsClick(IWebElement element) {
            js.ExecuteScript("arguments[0].click();", element);
        }

        public void JsScrollTo(IWebElement element) {
            js.ExecuteScript("argument[0].scrollIntoView(true);", element);
        }

        public void WaitUntilDisplayedAndEnabled(IWebElement element) {
            wait.Until(w => element.Displayed && element.Enabled);
        }
    }
}