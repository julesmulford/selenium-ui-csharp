using Allure.Net.Commons;
using FluentAssertions;
using OpenQA.Selenium;

namespace SeleniumUI.Tests.Pages;

public class LoginPage : BasePage
{
    private static readonly By UsernameInput = By.CssSelector("input[name=\"username\"]");
    private static readonly By PasswordInput = By.CssSelector("input[name=\"password\"]");
    private static readonly By SubmitButton  = By.CssSelector("button[type=\"submit\"]");
    private static readonly By ErrorMessage  = By.CssSelector(".oxd-alert-content-text");

    public LoginPage(IWebDriver driver) : base(driver) { }

    public void Navigate() => NavigateTo("/web/index.php/auth/login");

    public void Login(string username, string password)
    {
        AllureApi.Step($"Login as {username}", () =>
        {
            Fill(UsernameInput, username);
            Fill(PasswordInput, password);
            Click(SubmitButton);
        });
    }

    public void ClickSubmit() => Click(SubmitButton);

    public void AssertLoggedIn() => WaitForUrlContains("/dashboard/index");

    public void AssertErrorVisible() =>
        IsVisible(ErrorMessage).Should().BeTrue("error message should be visible after failed login");

    public void AssertOnLoginPage() =>
        IsVisible(SubmitButton).Should().BeTrue("login submit button should be visible");
}
