using ChannelEngine.Shared.Models;
using Xunit;

namespace ChannelEngine.Tests;

public class TopProductsTests
{
    [Fact]
    public void Calculates_Top_5_Products_By_Quantity()
    {
        // Arrange
        var orders = new List<OrderDto>
        {
            new() { MerchantProductNo = "A", Gtin = "111", Description = "Product A", Quantity = 5 },
            new() { MerchantProductNo = "B", Gtin = "222", Description = "Product B", Quantity = 2 },
            new() { MerchantProductNo = "A", Gtin = "111", Description = "Product A", Quantity = 3 },
            new() { MerchantProductNo = "C", Gtin = "333", Description = "Product C", Quantity = 8 },
            new() { MerchantProductNo = "D", Gtin = "444", Description = "Product D", Quantity = 1 },
            new() { MerchantProductNo = "E", Gtin = "555", Description = "Product E", Quantity = 4 },
            new() { MerchantProductNo = "F", Gtin = "666", Description = "Product F", Quantity = 10 }
        };

        // Act
        var top = orders
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

        // Assert
        Assert.Equal(5, top.Count);
        Assert.Contains(top, p => p.MerchantProductNo == "F" && p.Quantity == 10);
        Assert.Contains(top, p => p.MerchantProductNo == "C" && p.Quantity == 8);
        Assert.Contains(top, p => p.MerchantProductNo == "A" && p.Quantity == 8);
        Assert.Contains(top, p => p.MerchantProductNo == "E" && p.Quantity == 4);
        Assert.Contains(top, p => p.MerchantProductNo == "B" && p.Quantity == 2);
    }

}
