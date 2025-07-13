// See https://aka.ms/new-console-template for more information
using ChannelEngine.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Register HttpClient and our service
services.AddHttpClient<IChannelEngineService, ChannelEngineService>();

var provider = services.BuildServiceProvider();
var channelService = provider.GetRequiredService<IChannelEngineService>();

Console.WriteLine("Fetching top 5 sold products (IN_PROGRESS orders)...");

var topProducts = await channelService.GetTop5ProductsAsync();

Console.WriteLine("\nTop 5 Products:");
Console.WriteLine($"{"Name",-30} {"GTIN",-15} {"Qty",5}");

foreach (var p in topProducts)
{
    Console.WriteLine($"{p.Description,-30} {p.Gtin,-15} {p.Quantity,5}");
}

if (topProducts.Count > 0)
{
    var productToUpdate = topProducts[0];
    Console.WriteLine($"\nUpdating stock of: {productToUpdate.Description} to 25...");

    bool success = await channelService.SetProductStockAsync(productToUpdate.MerchantProductNo, 25);

    Console.WriteLine(success
        ? "Stock updated successfully."
        : "Failed to update stock.");
}
