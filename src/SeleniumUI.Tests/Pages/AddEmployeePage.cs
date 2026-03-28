using OpenQA.Selenium;
using SeleniumUI.Tests.Data;

namespace SeleniumUI.Tests.Pages;

public class AddEmployeePage : BasePage
{
    private static readonly By FirstName  = By.CssSelector("input[name=\"firstName\"]");
    private static readonly By MiddleName = By.CssSelector("input[name=\"middleName\"]");
    private static readonly By LastName   = By.CssSelector("input[name=\"lastName\"]");
    private static readonly By SaveButton = By.CssSelector("button[type=\"submit\"]");

    public AddEmployeePage(IWebDriver driver) : base(driver) { }

    public void AssertOnPage() => WaitForUrlContains("/pim/addEmployee");

    public void FillEmployeeDetails(Employee employee)
    {
        Fill(FirstName, employee.FirstName);
        Fill(MiddleName, employee.MiddleName);
        Fill(LastName, employee.LastName);
    }

    public void Save() => Click(SaveButton);
}
