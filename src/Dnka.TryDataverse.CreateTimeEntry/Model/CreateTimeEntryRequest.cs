using Newtonsoft.Json;
using System;

namespace Dnka.TryDataverse.CreateTimeEntry.Model
{
    public class CreateTimeEntryRequest
    {
        [JsonProperty("StartOn")]
        public DateTime StartOn { get; set; }

        [JsonProperty("EndOn")]
        public DateTime EndOn { get; set; }
    }
}
