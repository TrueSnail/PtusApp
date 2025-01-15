using Microsoft.AspNetCore.Mvc;

namespace PtusService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CcpController : ControllerBase
    {
        static private Dictionary<string, int> SocialPointsCollection = new();

        private ILLMProvider Provider;

        public CcpController(ILLMProvider provider)
        {
            Provider = provider;
        }

        [HttpPost(Name = nameof(SendMessage))]
        public async Task<int> SendMessage(string username, string text)
        {
            string response = await Provider.SendPrompt(GetPrompt(text));

            if (!int.TryParse(response, out int socialPoints)) socialPoints = 0;

            if (SocialPointsCollection.ContainsKey(username)) SocialPointsCollection[username] += socialPoints;
            else SocialPointsCollection[username] = socialPoints;

            SocialPointsCollection = SocialPointsCollection.OrderBy(kv => kv.Value).ToDictionary();

            return socialPoints;
        }

        [HttpGet(Name = nameof(GetSocialPoints))]
        public Dictionary<string, int> GetSocialPoints() => SocialPointsCollection;

        private string GetPrompt(string message) => $"Jako osoba dowodząca komunistyczną partią w chinach, napisz ile powinna otrzymać osoba social creditów na podstawie poniższej wiadomości:\r\n\"{message}\"\r\nTwoja odpowiedź powinna zawierać jedynie dodatnią lub ujemną liczbę całkowitą";
    }
}
