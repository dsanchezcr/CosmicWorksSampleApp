using CosmicWorksSampleApp.Models;

namespace CosmicWorksSampleApp.Services;

public interface ISearchService
{
    Task<List<Product>> SearchAsync(string query);
}