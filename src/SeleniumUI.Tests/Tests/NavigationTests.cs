using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using FluentAssertions;
using NUnit.Framework;
using SeleniumUI.Tests.Components;
using SeleniumUI.Tests.Fixtures;

namespace SeleniumUI.Tests.Tests;

[AllureSuite("Navigation")]
[Category("regression")]
public class NavigationTests : BrowserFixture
{
    private SideMenuComponent _sideMenu = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        LoginAsAdmin();
        _sideMenu = new SideMenuComponent(Driver);
    }

    [Test, Category("smoke"), AllureTag("smoke")]
    [AllureTitle("Navigate to PIM module")]
    public void NavigateToPIM()
    {
        _sideMenu.NavigateToPIM();
        Driver.Url.Should().Contain("/pim/", "clicking PIM in side menu should navigate to PIM section");
    }

    [Test, AllureTag("regression")]
    [AllureTitle("Navigate to Leave module")]
    public void NavigateToLeave()
    {
        _sideMenu.NavigateToLeave();
        Driver.Url.Should().Contain("/leave/", "clicking Leave in side menu should navigate to Leave section");
    }

    [Test, AllureTag("regression")]
    [AllureTitle("Navigate to Recruitment module")]
    public void NavigateToRecruitment()
    {
        _sideMenu.NavigateToRecruitment();
        Driver.Url.Should().Contain("/recruitment/", "clicking Recruitment should navigate to Recruitment section");
    }

    [Test, AllureTag("regression")]
    [AllureTitle("Navigate to My Info module")]
    public void NavigateToMyInfo()
    {
        _sideMenu.NavigateToMyInfo();
        Driver.Url.Should().Contain("/pim/viewMyDetails", "clicking My Info should navigate to personal details page");
    }
}
