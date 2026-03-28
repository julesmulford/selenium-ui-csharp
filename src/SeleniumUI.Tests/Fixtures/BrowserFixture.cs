using Allure.Net.Commons;
using Allure.NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumUI.Tests.Data;
using SeleniumUI.Tests.Driver;
using SeleniumUI.Tests.Pages;
using Serilog;

namespace SeleniumUI.Tests.Fixtures;

[AllureNUnit]
[Parallelizable(ParallelScope.Fixtures)]
public abstract class BrowserFixture
{
    protected IWebDriver Driver = null!;

    [SetUp]
    public virtual void SetUp()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        Driver = WebDriverFactory.CreateChromeDriver();
        Log.Information("Test starting: {TestName}", TestContext.CurrentContext.Test.Name);
    }

    [TearDown]
    public virtual void TearDown()
    {
        if (Driver != null)
        {
            try
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                AllureApi.AddAttachment("Final Screenshot", "image/png", screenshot.AsByteArray);
            }
            catch (Exception ex)
            {
                Log.Warning(ex, "Screenshot capture failed");
            }

            Driver.Quit();
            Driver = null!;
        }

        Log.CloseAndFlush();
    }

    protected void LoginAsAdmin()
    {
        var loginPage = new LoginPage(Driver);
        loginPage.Navigate();
        loginPage.Login(TestDataFactory.AdminUsername, TestDataFactory.AdminPassword);
        loginPage.AssertLoggedIn();
    }
}
