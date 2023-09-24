using CosmicWorksSampleApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace CosmicWorksSampleApp.Services;
public class SearchService : ISearchService
{
    private readonly IConfiguration configuration;

    public SearchService(IConfiguration _configuration)
    {
        configuration = _configuration;
    }
    public async Task<List<Product>> SearchAsync(string query)
    {
        string url = configuration.GetValue<string>("AzureSearchUri") + query;
        using HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("api-key", configuration.GetValue<string>("AzureSearchKey"));

        var response = await httpClient.GetStringAsync(url);
        var jsonObject = JObject.Parse(response);
        var valueArray = jsonObject["value"].ToString();
        var productResults = JsonConvert.DeserializeObject<List<Product>>(valueArray);
        var results = new List<Product>();

        foreach (var result in productResults)
        {
            var product = new Product 
                { id = result.id, sku = result.sku, name = result.name, description = result.description, price = result.price };
            results.Add(product);
        }

        return results;
    }
}