using Newtonsoft.Json;

namespace AppPagStone.Stone.Split
{
    [JsonObject("splits")]
    public class SplitData
    {
        public string recipient_key { get; set; }
        public string recipient_name { get; set; }
        public float amount { get; set; }
    }
}
