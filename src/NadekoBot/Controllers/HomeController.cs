using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace NadekoBot.Controllers
{
    public class HomeController : Controller
    {
        IMemoryCache _cache;
        ILogger<HomeController> _logger;

        public HomeController(
            IMemoryCache cache,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            var sw = new Stopwatch();
            sw.Start();

            object serverCount;
            if (!_cache.TryGetValue("server_count", out serverCount))
            {
                serverCount = await GetServerCount();
                _cache.Set("server_count", serverCount,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));
            }

            sw.Stop();

            _logger.LogInformation($"Home response time {sw.Elapsed.TotalSeconds}");
            return View("Home", serverCount);
        }
        //TODO: Change this to get the data directly from the bot
        private async Task<string> GetServerCount()
        {
            using (var http = new HttpClient())
            {
                var res = await http.GetAsync("https://www.carbonitex.net/discord/bots");
                var str = await res.Content.ReadAsStringAsync();
                var m = Regex.Match(str, @"test=\\""(?<count>\d*)\\"" name=\\""Nadeko\\""");
                return m.Groups["count"].Value;
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
