using CosmicWorksSampleApp.Models;
using CosmicWorksSampleApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CosmicWorksSampleApp.Pages;
public class SearchPageModel : PageModel
{
    private readonly ISearchService _searchService;
    public IEnumerable<Product>? Products { get; set; }
    public SearchPageModel(ISearchService searchService)
    {
        _searchService = searchService;
    }
    [BindProperty]
    public string Query { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (!string.IsNullOrEmpty(Query))
        {
            Products ??= await _searchService.SearchAsync(Query);
        }
        return Page();
    }
}