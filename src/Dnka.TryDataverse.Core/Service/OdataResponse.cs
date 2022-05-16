using System.Text.Json.Serialization;

namespace Dnka.TryDataverse.Core.Service
{
    public class OdataResponse<T>
    {
        [JsonPropertyName("@odata.context")]
        public string OdataContext { get; set; }

        [JsonPropertyName("value")]
        public T Value { get; set; }
    }
}
