namespace Umbraco.Community.DeliveryApiClient.Net.Models;

/// <summary>
/// Default model for the Umbraco Multi Url Picker
/// </summary>
public class MultiUrlPickerLink
{
    public string Title { get; set; }
    public string Target { get; set; }
    public MultiUrlPickerLinkType LinkType { get; set; }
    public string DestinationId { get; set; }
    public string Url { get; set; }
}
