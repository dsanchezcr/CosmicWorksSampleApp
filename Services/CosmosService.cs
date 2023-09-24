using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using CosmicWorksSampleApp.Models;
using CosmicWorksSampleApp.Services;

namespace CosmicWorksSampleApp.Services;

public class CosmosService : ICosmosService
{
    private readonly CosmosClient _client;

    public CosmosService(IConfiguration configuration)
    {
        _client = new CosmosClient(
            connectionString: configuration.GetConnectionString("CosmosDB")
        ); 
    }

    private Container container
    {
        get => _client.GetDatabase("cosmicworks").GetContainer("products");
    }

    public async Task<IEnumerable<Product>> RetrieveAllProductsAsync()
    {
        var queryable = container.GetItemLinqQueryable<Product>();

        using FeedIterator<Product> feed = queryable
            .ToFeedIterator();

        List<Product> results = new();

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (Product item in response)
            {
                results.Add(item);
            }
        }

        return results;
    }

    public async Task<IEnumerable<Product>> RetrieveDiscountedProductsAsync(decimal _price)
    {
        var queryable = container.GetItemLinqQueryable<Product>();

        using FeedIterator<Product> feed = queryable
            .Where(p => p.price < _price)
            .OrderByDescending(p => p.price)
            .ToFeedIterator();

        List<Product> results = new();

        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            foreach (Product item in response)
            {
                results.Add(item);
            }
        }

        return results;
    }
}