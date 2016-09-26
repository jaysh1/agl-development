using Newtonsoft.Json;

namespace agl.developer.test.parser
{
    [JsonObject]
    public class Pet
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
