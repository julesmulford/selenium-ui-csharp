using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumUI.Tests.Components;

public class SideMenuComponent
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public SideMenuComponent(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
    }

    private void NavigateTo(string item)
    {
        var menuItem = _wait.Until(ExpectedConditions.ElementToBeClickable(
            By.XPath($"//nav[contains(@class,'oxd-sidepanel')]//span[normalize-space()='{item}']")));
        menuItem.Click();
    }

    public void NavigateToPIM()         => NavigateTo("PIM");
    public void NavigateToLeave()       => NavigateTo("Leave");
    public void NavigateToRecruitment() => NavigateTo("Recruitment");
    public void NavigateToMyInfo()      => NavigateTo("My Info");
}
