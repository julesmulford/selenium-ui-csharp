namespace SeleniumUI.Tests.Config;

public static class TestConfiguration
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("APP_BASE_URL")
        ?? "https://opensource-demo.orangehrmlive.com";

    public static bool Headless =>
        bool.Parse(Environment.GetEnvironmentVariable("BROWSER_HEADLESS") ?? "true");

    public static int ImplicitWaitSeconds =>
        int.Parse(Environment.GetEnvironmentVariable("IMPLICIT_WAIT") ?? "10");

    public static int PageLoadTimeoutSeconds =>
        int.Parse(Environment.GetEnvironmentVariable("PAGE_LOAD_TIMEOUT") ?? "30");

    public static string AdminUsername =>
        Environment.GetEnvironmentVariable("ADMIN_USERNAME") ?? "Admin";

    public static string AdminPassword =>
        Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "admin123";
}
