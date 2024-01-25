namespace UmbracoDeliveryApiClient.Settings
{
    public class UmbracoApiOptions
    {
        public const string SectionName = "UmbracoApi";
        public bool Enabled { get; set; }
        public string ApiKey { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public bool EnableCache { get; set; } = true;
    }
}
