using CosmicWorksSampleApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CosmicWorksSampleApp.Pages;
public class ChatPageModel : PageModel
{
    private readonly IChatService _chatService;

    public ChatPageModel(IChatService chatService)
    {
        _chatService = chatService;
    }

    public string ResponseText { get; set; }
    [BindProperty]
    public string Message { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (!string.IsNullOrEmpty(Message))
        {
            ResponseText = await _chatService.GetResponseAsync(Message);
            return Page();
        }
        
        return Page();
    }   
}