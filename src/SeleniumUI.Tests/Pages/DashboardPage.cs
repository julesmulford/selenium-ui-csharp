using FluentAssertions;
using OpenQA.Selenium;

namespace SeleniumUI.Tests.Pages;

public class DashboardPage : BasePage
{
    private static readonly By Breadcrumb       = By.CssSelector(".oxd-topbar-header-breadcrumb h6");
    private static readonly By UserDropdown     = By.CssSelector(".oxd-userdropdown-tab");
    private static readonly By UserDropdownMenu = By.CssSelector(".oxd-userdropdown-dropdown");
    private static readonly By Widgets          = By.CssSelector(".oxd-grid-item");

    public DashboardPage(IWebDriver driver) : base(driver) { }

    public void AssertOnDashboard()
    {
        WaitForUrlContains("/dashboard/index");
        GetText(Breadcrumb).Should().Contain("Dashboard");
    }

    public void OpenUserDropdown() => Click(UserDropdown);

    public void AssertUserDropdownVisible()
    {
        OpenUserDropdown();
        IsVisible(UserDropdownMenu).Should().BeTrue("dropdown menu should be visible after clicking user tab");
    }

    public int GetWidgetCount() => Driver.FindElements(Widgets).Count;
}
