using FluentAssertions;
using OpenQA.Selenium;

namespace SeleniumUI.Tests.Pages;

public class EmployeeListPage : BasePage
{
    private static readonly By SearchInput = By.CssSelector("[placeholder=\"Type for hints...\"]");
    private static readonly By SearchButton = By.CssSelector("button[type=\"submit\"]");
    private static readonly By AddButton   = By.XPath("//button[normalize-space()=\"Add Employee\"]");
    private static readonly By TableRows   = By.CssSelector(".oxd-table-body .oxd-table-row");

    public EmployeeListPage(IWebDriver driver) : base(driver) { }

    public void Navigate() => NavigateTo("/web/index.php/pim/viewEmployeeList");

    public void AssertOnPage() => WaitForUrlContains("/pim/viewEmployeeList");

    public void SearchByName(string name)
    {
        Fill(SearchInput, name);
        Click(SearchButton);
        // Allow results to load
        Thread.Sleep(1500);
    }

    public void ClickAddEmployee() => Click(AddButton);

    public int GetRowCount() => Driver.FindElements(TableRows).Count;

    public void AssertHasResults() =>
        GetRowCount().Should().BeGreaterThan(0, "employee list should contain at least one record");
}
