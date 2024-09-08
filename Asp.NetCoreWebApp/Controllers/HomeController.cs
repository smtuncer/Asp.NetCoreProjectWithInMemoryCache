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
            // Cache'te veri yoksa, veri kayna��ndan al
            cachedDataKey = "Bu cache'de saklanan veri";

            // Cache ayarlar�
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                // Cache s�resini belirleyin (�rne�in 5 dakika)
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                // Cache'in s�re dolmadan �nce kaybolmamas� i�in bir �ncelik belirleyin
                Priority = CacheItemPriority.Normal,

            };

            // Veriyi cache'e ekle
            _memoryCache.Set("cachedData", cachedDataKey, cacheEntryOptions);
        }

        // Cache'teki veriyi d�nd�r
        return Ok(cachedDataKey);
    }
}
