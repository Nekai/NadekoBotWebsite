using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace NadekoBot.Controllers
{
    public class CommandslistController : Controller
    {
        //TODO add caching
        public async Task<IActionResult> Index()
        {
            var cmdlistHtml = await GetCommandListHtml();
            return View((object)cmdlistHtml);
        }
        //TODO get this from the actual bot instead of github dev branch
        private async Task<string> GetCommandListHtml()
        {
            using (var http = new HttpClient())
            {
                var str = await GetCommandListRaw();
                http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "ghmd-renderer");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "text/x-markdown");
                var res = await http.PostAsync("https://api.github.com/markdown/raw", new StringContent(str));
                var content = res.Content;

                return await content.ReadAsStringAsync();
            }
        }

        private async Task<string> GetCommandListRaw()
        {
            using (var http = new HttpClient())
            {
                return await http.GetStringAsync("https://raw.githubusercontent.com/Kwoth/NadekoBot/dev/commandlist.md");
            }
        }
    }
}