﻿namespace UmbracoDeliveryApiClient.Models;

public class ChildrenCollection<T>
{
    public int Total { get; set; }
    public List<T> Items { get; set; } = new List<T>();
}

