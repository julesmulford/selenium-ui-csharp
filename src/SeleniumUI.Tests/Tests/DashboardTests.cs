using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using FluentAssertions;
using NUnit.Framework;
using SeleniumUI.Tests.Fixtures;
using SeleniumUI.Tests.Pages;

namespace SeleniumUI.Tests.Tests;

[AllureSuite("Dashboard")]
[Category("regression")]
public class DashboardTests : BrowserFixture
{
    private DashboardPage _dashboardPage = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        LoginAsAdmin();
        _dashboardPage = new DashboardPage(Driver);
    }

    [Test, Category("smoke"), AllureTag("smoke")]
    [AllureTitle("Dashboard visible after login")]
    public void DashboardVisibleAfterLogin() =>
        _dashboardPage.AssertOnDashboard();

    [Test, AllureTag("regression")]
    [AllureTitle("User dropdown is accessible")]
    public void UserDropdownIsAccessible() =>
        _dashboardPage.AssertUserDropdownVisible();

    [Test, AllureTag("regression")]
    [AllureTitle("Dashboard has widgets")]
    public void DashboardHasWidgets() =>
        _dashboardPage.GetWidgetCount().Should().BeGreaterThan(0, "dashboard should have at least one widget");
}
