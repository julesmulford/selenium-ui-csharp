using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumUI.Tests.Config;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Serilog;

namespace SeleniumUI.Tests.Driver;

public static class WebDriverFactory
{
    public static IWebDriver CreateChromeDriver()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());

        var options = new ChromeOptions();
        if (TestConfiguration.Headless)
        {
            options.AddArgument("--headless=new");
        }
        options.AddArguments(
            "--no-sandbox",
            "--disable-dev-shm-usage",
            "--window-size=1280,720",
            "--disable-gpu");

        var driver = new ChromeDriver(options);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestConfiguration.ImplicitWaitSeconds);
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeoutSeconds);

        Log.Information("ChromeDriver created (headless={Headless})", TestConfiguration.Headless);
        return driver;
    }
}
