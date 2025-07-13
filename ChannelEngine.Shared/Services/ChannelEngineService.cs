using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using ChannelEngine.Shared.Models;

namespace ChannelEngine.Shared.Services;

public class ChannelEngineService : IChannelEngineService
{
    private const string ApiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";
    private const string BaseUrl = "https://api-dev.channelengine.net/api/v2";

    private readonly HttpClient _httpClient;

    public ChannelEngineService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<OrderDto>> GetInProgressOrdersAsync()
    {
        var url = $"{BaseUrl}/orders?statuses=IN_PROGRESS&apikey={ApiKey}";
        var response = await _httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var orders = new List<OrderDto>();

        using var doc = JsonDocument.Parse(content);
        var items = doc.RootElement.GetProperty("Content");

        foreach (var order in items.EnumerateArray())
        {
            foreach (var line in order.GetProperty("Lines").EnumerateArray())
            {
                orders.Add(new OrderDto
                {
                    MerchantProductNo = line.GetProperty("MerchantProductNo").GetString() ?? "",
                    Gtin = line.GetProperty("Gtin").GetString() ?? "",
                    Description = line.GetProperty("Description").GetString() ?? "",
                    Quantity = line.GetProperty("Quantity").GetInt32()
                });
            }
        }

        return orders;
    }

    public async Task<List<OrderDto>> GetTop5ProductsAsync()
    {
        var allOrders = await GetInProgressOrdersAsync();

        return allOrders
            .GroupBy(o => o.MerchantProductNo)
            .Select(g => new OrderDto
            {
                MerchantProductNo = g.Key,
                Gtin = g.First().Gtin,
                Description = g.First().Description,
                Quantity = g.Sum(x => x.Quantity)
            })
            .OrderByDescending(p => p.Quantity)
            .Take(5)
            .ToList();
    }

    public async Task<bool> SetProductStockAsync(string merchantProductNo, int stock)
    {
        var url = $"{BaseUrl}/offer/stock?apikey={ApiKey}";

        var payload = new[]
        {
            new ProductStockUpdateDto
            {
                MerchantProductNo = merchantProductNo,
                StockLocations = new List<StockLocationDto>
                {
                    new StockLocationDto
                    {
                        StockLocationId = "1", // assuming "1" is valid for dev env
                        Stock = stock
                    }
                }
            }
        };

        var response = await _httpClient.PutAsJsonAsync(url, payload);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API error: " + errorBody);
        }

        return response.IsSuccessStatusCode;
    }

}