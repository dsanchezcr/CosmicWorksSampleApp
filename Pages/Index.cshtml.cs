using Microsoft.AspNetCore.Mvc.RazorPages;
using CosmicWorksSampleApp.Models;
using CosmicWorksSampleApp.Services;

namespace CosmicWorksSampleApp.Pages;

public class IndexPageModel : PageModel
{
    private readonly ICosmosService _cosmosService;

    public IEnumerable<Product>? Products { get; set; }

    public IndexPageModel(ICosmosService cosmosService)
    {
        _cosmosService = cosmosService;
    }

    public async Task OnGetAsync()
    {
        Products ??= await _cosmosService.RetrieveDiscountedProductsAsync(200);
    }
}