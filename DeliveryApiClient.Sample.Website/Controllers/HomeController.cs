using DeliveryApiClient.Sample.Website.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Umbraco.Community.DeliveryApiClient.Net;

namespace DeliveryApiClient.Sample.Website.Controllers
{
    public class HomeController : Controller
    {        
        private readonly IContentProvider contentProvider;

        public HomeController(IContentProvider contentProvider)
        {
            this.contentProvider = contentProvider;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Page()
        {
            var model = await contentProvider.GetContentItemAsync<PageViewModel>("resource-material");

            return View(model);
        }
    }
}
