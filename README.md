# selenium-ui-csharp

Selenium 4 + .NET 8 + NUnit UI test framework for [OrangeHRM Demo](https://opensource-demo.orangehrmlive.com).

## Tech Stack

| Tool | Version |
|---|---|
| .NET | 8.0 |
| Selenium.WebDriver | 4.21.0 |
| Selenium.Support | 4.21.0 |
| WebDriverManager | 2.17.4 |
| NUnit | 4.1.0 |
| NUnit3TestAdapter | 4.5.0 |
| FluentAssertions | 6.12.0 |
| Serilog | 3.1.1 |
| Allure.NUnit | 2.12.1 |

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- Google Chrome (latest stable)
- (Optional) [Allure CLI](https://docs.qameta.io/allure/#_installing_a_commandline) for HTML reports

## Project Structure

```
selenium-ui-csharp/
├── .github/workflows/ci.yml      # GitHub Actions CI pipeline
├── selenium-ui-csharp.sln
└── src/
    └── SeleniumUI.Tests/
        ├── Config/               # Environment-driven configuration
        ├── Driver/               # WebDriverFactory (ChromeDriver via WebDriverManager)
        ├── Pages/                # Page Object Model
        │   ├── BasePage.cs       # Fluent wait helpers shared by all pages
        │   ├── LoginPage.cs
        │   ├── DashboardPage.cs
        │   ├── EmployeeListPage.cs
        │   └── AddEmployeePage.cs
        ├── Components/           # Reusable UI components
        │   └── SideMenuComponent.cs
        ├── Data/                 # Test data models and builder
        │   ├── Employee.cs       # C# record
        │   ├── EmployeeBuilder.cs
        │   └── TestDataFactory.cs
        ├── Fixtures/             # NUnit base fixture with driver lifecycle
        │   └── BrowserFixture.cs
        └── Tests/                # Test classes
            ├── LoginTests.cs
            ├── DashboardTests.cs
            ├── NavigationTests.cs
            └── EmployeeTests.cs
```

## Running Tests

### Restore & build

```bash
dotnet restore
dotnet build --configuration Release
```

### Smoke tests only

```bash
dotnet test --filter "Category=smoke"
```

### Full regression suite

```bash
dotnet test --filter "Category=regression"
```

### All tests

```bash
dotnet test
```

### Override base URL or headless mode

```bash
APP_BASE_URL=https://opensource-demo.orangehrmlive.com \
BROWSER_HEADLESS=true \
dotnet test --filter "Category=smoke"
```

## Configuration

All settings are read from environment variables (CI-friendly). Defaults target the public OrangeHRM demo.

| Variable | Default |
|---|---|
| `APP_BASE_URL` | `https://opensource-demo.orangehrmlive.com` |
| `BROWSER_HEADLESS` | `true` |
| `IMPLICIT_WAIT` | `10` (seconds) |
| `PAGE_LOAD_TIMEOUT` | `30` (seconds) |
| `ADMIN_USERNAME` | `Admin` |
| `ADMIN_PASSWORD` | `admin123` |

## Allure Reports

```bash
# Install Allure CLI (once)
npm install -g allure-commandline

# Serve the report after a test run
allure serve src/SeleniumUI.Tests/allure-results
```

## Architecture

### WebDriverFactory

`WebDriverFactory.CreateChromeDriver()` uses [WebDriverManager](https://github.com/rosolko/WebDriverManager.Net) to automatically download the matching ChromeDriver binary — no manual driver management required. Chrome runs headless by default.

### Page Object Model

`BasePage` centralises `WebDriverWait`-backed helpers (`WaitForElement`, `WaitForClickable`, `Fill`, `Click`, `IsVisible`, `WaitForUrlContains`). Every page class extends `BasePage` and exposes high-level actions rather than raw selectors.

### EmployeeBuilder

`EmployeeBuilder` uses a fluent API and generates a unique last name on every instantiation via `Guid.NewGuid()` suffix, ensuring test isolation without shared state.

### BrowserFixture

`BrowserFixture` is the abstract NUnit base class. `[SetUp]` creates a fresh `ChromeDriver`; `[TearDown]` captures a screenshot (attached to Allure), quits the driver, and flushes Serilog. Tests are run with `[Parallelizable(ParallelScope.Fixtures)]`.

### Allure Integration

`[AllureNUnit]` on `BrowserFixture` wires the Allure lifecycle into every test. Tests are decorated with `[AllureSuite]`, `[AllureTitle]`, and `[AllureTag]` attributes. Steps inside page actions use `AllureApi.Step(...)`.

## Test Coverage

| Suite | Test | Category |
|---|---|---|
| Authentication | Login form is visible | smoke |
| Authentication | Successful login with valid credentials | smoke, regression |
| Authentication | Error shown for invalid password | regression |
| Authentication | Error shown for invalid credentials | regression |
| Authentication | Submit with empty fields stays on login | regression |
| Dashboard | Dashboard visible after login | smoke, regression |
| Dashboard | User dropdown is accessible | regression |
| Dashboard | Dashboard has widgets | regression |
| Navigation | Navigate to PIM module | smoke, regression |
| Navigation | Navigate to Leave module | regression |
| Navigation | Navigate to Recruitment module | regression |
| Navigation | Navigate to My Info module | regression |
| Employee Management | Employee list page loads | smoke, regression |
| Employee Management | Employee list has records | regression |
| Employee Management | Search returns results | regression |
| Employee Management | Navigate to add employee page | regression |
| Employee Management | Add new employee successfully | regression |

## CI/CD

The GitHub Actions pipeline (`.github/workflows/ci.yml`) runs in three sequential jobs:

1. **Build** — `dotnet build` on every push/PR
2. **Smoke Tests** — fast gate using `--filter "Category=smoke"`
3. **Regression Tests** — full suite after smoke tests pass

Allure results and TRX reports are uploaded as artifacts after each job.
