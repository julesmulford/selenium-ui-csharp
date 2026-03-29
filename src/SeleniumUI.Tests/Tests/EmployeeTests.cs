using Allure.Net.Commons;
using Allure.NUnit.Attributes;
using FluentAssertions;
using NUnit.Framework;
using SeleniumUI.Tests.Data;
using SeleniumUI.Tests.Fixtures;
using SeleniumUI.Tests.Pages;

namespace SeleniumUI.Tests.Tests;

[AllureSuite("Employee Management")]
[Category("regression")]
public class EmployeeTests : BrowserFixture
{
    private EmployeeListPage _employeeListPage = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        LoginAsAdmin();
        _employeeListPage = new EmployeeListPage(Driver);
        _employeeListPage.Navigate();
    }

    [Test, Category("smoke"), AllureTag("smoke")]
    [AllureTitle("Employee list page loads")]
    public void EmployeeListPageLoads() =>
        _employeeListPage.AssertOnPage();

    [Test, AllureTag("regression")]
    [AllureTitle("Employee list has records")]
    public void EmployeeListHasRecords() =>
        _employeeListPage.AssertHasResults();

    [Test, AllureTag("regression")]
    [AllureTitle("Search returns results")]
    public void SearchReturnsResults()
    {
        _employeeListPage.SearchByName("Admin");
        _employeeListPage.AssertHasResults();
    }

    [Test, AllureTag("regression")]
    [AllureTitle("Navigate to add employee page")]
    public void NavigateToAddEmployeePage()
    {
        _employeeListPage.ClickAddEmployee();
        new AddEmployeePage(Driver).AssertOnPage();
    }

    [Test, AllureTag("regression")]
    [AllureTitle("Add new employee successfully")]
    public void AddNewEmployeeSuccessfully()
    {
        var employee = TestDataFactory.CreateEmployee();
        _employeeListPage.ClickAddEmployee();

        var addPage = new AddEmployeePage(Driver);
        addPage.AssertOnPage();
        addPage.FillEmployeeDetails(employee);
        addPage.Save();

        Driver.Url.Should().Contain("/pim/viewPersonalDetails",
            "saving a new employee should redirect to the personal details page");
    }
}
