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
            // Cache'te veri yoksa, veri kayna��ndan al
            cachedData = "Bu cache'de saklanan veri";

            // Cache ayarlar�
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                // Cache s�resini belirleyin (�rne�in 60 saniye)
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                // Cache'in s�re dolmadan �nce kaybolmamas� i�in bir �ncelik belirleyin
                Priority = CacheItemPriority.Normal
            };

            // Veriyi cache'e ekle
            _memoryCache.Set("cachedData", cachedData, cacheEntryOptions);
        }

        // Cache'teki veriyi d�nd�r
        return Ok(cachedData);
    }
}
