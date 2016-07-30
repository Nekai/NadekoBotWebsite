using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace NadekoBot.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewData["server_count"] = await GetServerCount();
            return View();
        }

        private async Task<string> GetServerCount()
        {
            using (var http = new HttpClient()) {
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
