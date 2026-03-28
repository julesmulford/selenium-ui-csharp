namespace SeleniumUI.Tests.Data;

public static class TestDataFactory
{
    public static Employee CreateEmployee() => EmployeeBuilder.Create().Build();

    public static string AdminUsername   => "Admin";
    public static string AdminPassword   => "admin123";
    public static string InvalidPassword => "wrongpassword";
    public static string InvalidUsername => "invalid_user";
}
