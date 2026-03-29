namespace Allure.NUnit.Attributes;

/// <summary>
/// Sets a custom title for the Allure test report entry.
/// Allure .NET does not ship a built-in AllureTitle attribute;
/// the test method name is used as the title by default.
/// This stub keeps the attribute in source for documentation purposes.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class AllureTitleAttribute : Attribute
{
    public string Title { get; }
    public AllureTitleAttribute(string title) => Title = title;
}
