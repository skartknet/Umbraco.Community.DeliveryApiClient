namespace UmbracoDeliveryApiClient.Models
{

    public class Image
    {
        public Guid Id { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public long Bytes { get; set; }

        public string? Extension { get; set; }
        public string? Url { get; set; }
    }
}
