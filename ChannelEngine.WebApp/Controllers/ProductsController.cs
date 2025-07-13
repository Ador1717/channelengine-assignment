using ChannelEngine.Shared.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChannelEngine.WebApp.Controllers;

public class ProductsController : Controller
{
    private readonly IChannelEngineService _channelService;

    public ProductsController(IChannelEngineService channelService)
    {
        _channelService = channelService;
    }

    public async Task<IActionResult> Index()
    {
        var topProducts = await _channelService.GetTop5ProductsAsync();
        return View(topProducts);
    }
}
