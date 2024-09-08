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
        if (!_memoryCache.TryGetValue("cachedDataKey", out string cachedDataKey))
        {
            // Cache'te veri yoksa, veri kaynaðýndan al
            cachedDataKey = "Bu cache'de saklanan veri";

            // Cache ayarlarý
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                // Cache süresini belirleyin (örneðin 5 dakika)
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                // Cache'in süre dolmadan önce kaybolmamasý için bir öncelik belirleyin
                Priority = CacheItemPriority.Normal,

            };

            // Veriyi cache'e ekle
            _memoryCache.Set("cachedData", cachedDataKey, cacheEntryOptions);
        }

        // Cache'teki veriyi döndür
        return Ok(cachedDataKey);
    }
}
