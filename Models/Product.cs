using Newtonsoft.Json;

namespace CosmicWorksSampleApp.Models;

public class Product
{
    [JsonProperty("id")]
    public required string id { get; set; }

    [JsonProperty("sku")]
    public required string sku { get; set; }

    [JsonProperty("name")]
    public required string name { get; set; }

    [JsonProperty("description")]
    public required string description { get; set; }

    [JsonProperty("price")]
    public required decimal price { get; set; }
}