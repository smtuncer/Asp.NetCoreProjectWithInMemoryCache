using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace Asp.NetCoreWebApp.Controllers;
public class HomeController : Controller
{
    private readonly IMemoryCache _memoryCache;

    public HomeController(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public IActionResult Index()
    {
        if (!_memoryCache.TryGetValue("cachedData", out string cachedData))
        {
            // Cache'te veri yoksa, veri kaynaðýndan al
            cachedData = "Bu cache'de saklanan veri";

            // Cache ayarlarý
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                // Cache süresini belirleyin (örneðin 60 saniye)
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                // Cache'in süre dolmadan önce kaybolmamasý için bir öncelik belirleyin
                Priority = CacheItemPriority.Normal
            };

            // Veriyi cache'e ekle
            _memoryCache.Set("cachedData", cachedData, cacheEntryOptions);
        }

        // Cache'teki veriyi döndür
        return Ok(cachedData);
    }
}
