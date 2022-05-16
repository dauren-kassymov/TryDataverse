using System;
using System.Text.Json.Serialization;

namespace Dnka.TryDataverse.Core.Model
{
    public class TimeEntryEntity
    {
        [JsonPropertyName("msdyn_start")]
        public DateTime Start { get; set; }

        [JsonPropertyName("msdyn_end")]
        public DateTime End { get; set; }
    }
}
