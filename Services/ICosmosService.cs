using CosmicWorksSampleApp.Models;

namespace CosmicWorksSampleApp.Services;
public interface ICosmosService
{
    Task<IEnumerable<Product>> RetrieveDiscountedProductsAsync(decimal _price);
    Task<IEnumerable<Product>> RetrieveAllProductsAsync();
}