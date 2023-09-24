namespace CosmicWorksSampleApp.Services;

public interface IChatService
{
    Task<string> GetResponseAsync(string prompt);
}