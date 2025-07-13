# ChannelEngine .NET Technical Assessment

This solution contains a multi-project .NET 9.0 application to interact with the ChannelEngine REST API.

---

## Projects

| Project                     | Description                                                                 |
|-----------------------------|-----------------------------------------------------------------------------|
| `ChannelEngine.Shared`      | Class library with API client and business logic (fetch orders, update stock) |
| `ChannelEngine.ConsoleApp`  | Console entry point: outputs Top 5 sold products and updates stock         |
| `ChannelEngine.WebApp`      | ASP.NET MVC app: shows Top 5 products in an HTML table                     |
| `ChannelEngine.Tests`       | Unit tests for Top 5 logic using xUnit                                     |

---

## How to Run

### Console App

```bash
dotnet run --project ChannelEngine.ConsoleApp
```
### Web App and vist the page with the given port
dotnet run --project ChannelEngine.WebApp


### for running the tests
dotnet test ChannelEngine.Tests