using Microsoft.AspNetCore.Mvc;

namespace PtusService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CcpController : ControllerBase
    {
        static public Dictionary<string, float> SocialPoints = new();

        [HttpGet(Name = nameof(GetSocialPoints))]
        public Dictionary<string, float> GetSocialPoints()
        {
            return SocialPoints;
        }

        [HttpPost(Name = nameof(SendMessage))]
        public float SendMessage(string username, string text)
        {
            SocialPoints[username] = 5;
            return 5;
        }
    }
}
