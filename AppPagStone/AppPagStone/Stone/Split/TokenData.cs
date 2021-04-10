using Newtonsoft.Json;

namespace AppPagStone.Stone
{
    [JsonObject("data")]
    public class TokenData
    {
        public string token { get; set; }
        public string token_type { get; set; }
    }
}
