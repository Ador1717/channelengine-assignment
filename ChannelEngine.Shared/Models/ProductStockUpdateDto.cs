namespace ChannelEngine.Shared.Models;

public class ProductStockUpdateDto
{
    public string MerchantProductNo { get; set; } = string.Empty;
    
    public List<StockLocationDto> StockLocations { get; set; } = new();
}

public class StockLocationDto
{
    public string StockLocationId { get; set; } = "1"; // Default test ID
    public int Stock { get; set; }
}