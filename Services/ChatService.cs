using Newtonsoft.Json.Linq;
using Azure;
using Azure.AI.OpenAI;

namespace CosmicWorksSampleApp.Services;

public class ChatService : IChatService
{
    private readonly OpenAIClient _client;
    private readonly string _deploymentId;
    private readonly IConfiguration configuration;
    public ChatService(IConfiguration _configuration)
    {
        configuration = _configuration;
        _client = new OpenAIClient(new Uri(configuration.GetValue<string>("AzureOpenAIEndpoint")), new AzureKeyCredential(configuration.GetValue<string>("AzureOpenAIKey")!));
        _deploymentId = configuration.GetValue<string>("AzureOpenAIDeployment");
    }
    public async Task<string> GetResponseAsync(string prompt)
    {
        // Azure Cognitive Search setup
        var searchEndpoint = new Uri(configuration.GetValue<string>("AzureSearchEndpoint"));
        var searchKey = configuration.GetValue<string>("AzureSearchResourceKey"); 
        var searchIndexName = configuration.GetValue<string>("AzureSearchIndex");

        var chatCompletionsOptions = new ChatCompletionsOptions()
        {
            Messages = { new ChatMessage(ChatRole.User, prompt) },
            AzureExtensionsOptions = new AzureChatExtensionsOptions()
            {
                Extensions =
                {
                    new AzureCognitiveSearchChatExtensionConfiguration()
                    {
                        SearchEndpoint = searchEndpoint,
                        IndexName = searchIndexName,
                        SearchKey = new AzureKeyCredential(searchKey!),
                    }
                }
            }
        };

        try
        {
            var response = await _client.GetChatCompletionsAsync(_deploymentId, chatCompletionsOptions);

            var message = response.Value.Choices[0].Message;
            return $"{message.Content}";
        }
        catch (Exception e)
        {
            // Handle exception
            return $"An error occurred: {e.Message}";
        }
    }
}