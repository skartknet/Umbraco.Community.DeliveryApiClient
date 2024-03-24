using Umbraco.Community.DeliveryApiClient.Net.Models;

namespace DeliveryApiClient.Sample.Website.Models
{
    public class PageViewModel : ContentBase<PageProperties>
    {
    }

    public class PageProperties
    {
        public string Email { get; set; } = "";
        public int Number { get; set; }
        public object? RichText { get; set; } = null;
    }
}
