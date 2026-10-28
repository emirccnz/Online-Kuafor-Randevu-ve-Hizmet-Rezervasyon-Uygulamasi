using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Appointment
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("epId")]
        public string? EpId { get; set; }

        [JsonPropertyName("services")]
        public List<string> Services { get; set; } = new();

        [JsonPropertyName("start-at")]
        public DateTime StartAt { get; set; }

        [JsonPropertyName("end-at")]
        public DateTime EndAt { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("custFullName")]
        public string CustFullName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        public string Phone { get; set; } = string.Empty;

        [JsonPropertyName("isCanceled")]
        public bool IsCanceled { get; set; }
    }
}
