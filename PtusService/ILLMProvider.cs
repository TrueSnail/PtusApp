namespace PtusService;

public interface ILLMProvider
{
    public Task<string> SendPrompt(string prompt);
}
