using Allure.NUnit.Attributes;
using NUnit.Framework;
using SeleniumUI.Tests.Data;
using SeleniumUI.Tests.Fixtures;
using SeleniumUI.Tests.Pages;

namespace SeleniumUI.Tests.Tests;

[AllureSuite("Authentication")]
[Category("regression")]
public class LoginTests : BrowserFixture
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _loginPage = new LoginPage(Driver);
        _loginPage.Navigate();
    }

    [Test, Category("smoke"), AllureTag("smoke")]
    [AllureTitle("Login form is visible")]
    public void LoginFormIsVisible() =>
        _loginPage.AssertOnLoginPage();

    [Test, Category("smoke"), AllureTag("smoke")]
    [AllureTitle("Successful login with valid credentials")]
    public void SuccessfulLoginWithValidCredentials()
    {
        _loginPage.Login(TestDataFactory.AdminUsername, TestDataFactory.AdminPassword);
        _loginPage.AssertLoggedIn();
    }

    [Test, AllureTag("regression")]
    [AllureTitle("Error shown for invalid password")]
    public void ErrorShownForInvalidPassword()
    {
        _loginPage.Login(TestDataFactory.AdminUsername, TestDataFactory.InvalidPassword);
        _loginPage.AssertErrorVisible();
    }

    [Test, AllureTag("regression")]
    [AllureTitle("Error shown for invalid credentials")]
    public void ErrorShownForInvalidCredentials()
    {
        _loginPage.Login(TestDataFactory.InvalidUsername, TestDataFactory.InvalidPassword);
        _loginPage.AssertErrorVisible();
    }

    [Test, AllureTag("regression")]
    [AllureTitle("Submit with empty fields stays on login")]
    public void SubmitWithEmptyFieldsStaysOnLogin()
    {
        _loginPage.ClickSubmit();
        _loginPage.AssertOnLoginPage();
    }
}
