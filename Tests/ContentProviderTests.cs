using DeliveryApiClient.Sample.Website.Models;
using Microsoft.Extensions.Options;
using Umbraco.Community.DeliveryApiClient.Net;
using Umbraco.Community.DeliveryApiClient.Net.Settings;


namespace Tests
{
    public class ContentProviderTests
    {
        private UmbracoDeliveryApiContext _context;

        [SetUp]
        public void Setup()
        {
            var options = Options.Create(new UmbracoApiOptions()
            {
                Domain = "https://mario-lumi.aueast01.umbraco.io/",
                ApiKey = "123"
            });

            _context = new UmbracoDeliveryApiContext(options);
        }

        [Test]
        public async Task ItShouldBeAbletoGrabAPageFromcloud()
        {
            using (var httpClient = new HttpClient())
            {
                var contentProvider = new UmbracoContentProvider(_context, httpClient);
                var model = await contentProvider.GetContentItemAsync<PageViewModel>("779fc54b-cb59-4701-917e-49b0d5b18e90");

                var f = model;

                Assert.IsTrue(model != null);
            }

        }

    }
}