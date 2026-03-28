using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUI.Tests.Config;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace SeleniumUI.Tests.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;
    private const int DefaultTimeoutSeconds = 15;

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(DefaultTimeoutSeconds));
    }

    protected void NavigateTo(string path)
    {
        var url = TestConfiguration.BaseUrl + path;
        Log.Debug("Navigating to {Url}", url);
        Driver.Navigate().GoToUrl(url);
    }

    protected IWebElement WaitForElement(By locator) =>
        Wait.Until(d => d.FindElement(locator));

    protected IWebElement WaitForClickable(By locator) =>
        Wait.Until(ExpectedConditions.ElementToBeClickable(locator));

    protected void Click(By locator)
    {
        Log.Debug("Clicking {Locator}", locator);
        WaitForClickable(locator).Click();
    }

    protected void Fill(By locator, string text)
    {
        Log.Debug("Filling {Locator} with '{Text}'", locator, text);
        var el = WaitForClickable(locator);
        el.Clear();
        el.SendKeys(text);
    }

    protected string GetText(By locator) => WaitForElement(locator).Text;

    protected bool IsVisible(By locator, int timeoutSeconds = 5)
    {
        try
        {
            new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds))
                .Until(ExpectedConditions.ElementIsVisible(locator));
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void WaitForUrlContains(string fragment) =>
        Wait.Until(d => d.Url.Contains(fragment, StringComparison.OrdinalIgnoreCase));
}
