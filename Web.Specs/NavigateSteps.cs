using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using NUnit.Framework;

namespace Web.Specs
{
    [Binding]
    public class NavigateSteps
    {
        [Given("I navigate to '(.*)'")]
        public void GivenINavigateTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        [Then("the page title is [\"'](.*)[\"']")]
        public void ThePageTitleIs(string title)
        {
            Assert.That(driver.Title, Is.EqualTo(title));
        }

        [When("I search for '(.*)'")]
        public void WhenISearchFor(string p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then("I see search results")]
        public void ThenISeeSearchResults()
        {
            ScenarioContext.Current.Pending();
        }

        [Then("I wait for (\\d*) seconds")]
        public void ThenIWait(int seconds)
        {
            System.Threading.Thread.Sleep(seconds * 1000);
        }

        [Then("the environment is '(.*)'")]
        public void TheEnvironmentIs(string expectedEnvironment)
        {
            Assert.That(Environment.GetEnvironmentVariable("ENVIRONMENT"), Is.EqualTo(expectedEnvironment));
        }

        [BeforeFeature("web")]
        public static void BeforeFeature()
        {
            var options = new ChromeOptions();
            if(Environment.GetEnvironmentVariable("IS_TESTERY") == "true") {
                options.AddArguments(Environment.GetEnvironmentVariable("TESTERY_CHROME_ARGS").Split(';'));
            }

            FeatureContext.Current.Add("driver", new ChromeDriver(options));
        }

        [BeforeFeature]
        public static void LoadEnvironment()
        {
            string environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
            FeatureContext.Current.Add("ENVIRONMENT", environment);
        }

        [AfterScenario("screenshot")]
        public static void takeScreenshot()
        {
            Screenshot s = ((ITakesScreenshot)((IWebDriver)FeatureContext.Current["driver"])).GetScreenshot();
            string fileName = ScenarioContext.Current.ScenarioInfo.Title.Replace(" ","") + DateTime.Now.ToString("_MMddyyyyHHmmss") + ".png";

            try
            {
                var separator = Path.DirectorySeparatorChar;
                string screenshotDir = ScenarioContext.Current.ScenarioContainer.Resolve<TestContext>().WorkDirectory.ToString() + separator + "screenshots";
                if (!Directory.Exists(screenshotDir))
                {
                    Directory.CreateDirectory(screenshotDir);
                }
                s.SaveAsFile(screenshotDir + separator + fileName, ScreenshotImageFormat.Png);
                Console.WriteLine($"-> info: added screenshot ({fileName}) to dir ({screenshotDir})");;

            } catch(Exception e)
            {
                Console.WriteLine($"-> error: ({e})");
            }

        }

        [AfterFeature("web")]
        public static void AfterFeature()
        {
            ((IWebDriver)FeatureContext.Current["driver"]).Quit();
        }

        public IWebDriver driver
        {
            get { return ((IWebDriver)FeatureContext.Current["driver"]); }
        }

        
    }
}
