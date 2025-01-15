using OpenAI.Chat;

namespace PtusService;

public class ChatGPTService : ILLMProvider
{
    private ChatClient Client;

    public ChatGPTService(string apiKey)
    {
        Client = new("gpt-4o-mini", apiKey);
    }

    public async Task<string> SendPrompt(string prompt)
    {
        var completion = await Client.CompleteChatAsync(prompt);
        return completion.Value.Content[0].Text;
    }
}
