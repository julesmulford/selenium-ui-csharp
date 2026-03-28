namespace SeleniumUI.Tests.Data;

public class EmployeeBuilder
{
    private string _firstName  = "AutoFirst";
    private string _middleName = "AutoMid";
    private string _lastName   = $"AutoLast{Guid.NewGuid().ToString("N")[..6].ToUpper()}";

    public EmployeeBuilder WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public EmployeeBuilder WithMiddleName(string middleName)
    {
        _middleName = middleName;
        return this;
    }

    public EmployeeBuilder WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public Employee Build() => new(_firstName, _middleName, _lastName);

    public static EmployeeBuilder Create() => new();
}
