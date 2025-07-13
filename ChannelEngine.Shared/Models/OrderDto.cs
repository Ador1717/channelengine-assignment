namespace ChannelEngine.Shared.Models;

public class OrderDto
{
    public string MerchantProductNo { get; set; } = string.Empty;
    public string Gtin { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
}