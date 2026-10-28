using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
    public class BusySlot
    {
        [JsonPropertyName("epId")]
        public string? EpId { get; set; }

        [JsonPropertyName("start-at")]
        public DateTime StartAt { get; set; }

        [JsonPropertyName("end-at")]
        public DateTime EndAt { get; set; }
    }
}
