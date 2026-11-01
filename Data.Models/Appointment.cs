using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Ad-Soyad alanı doldurulması zorunludur!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Ad-Soyad 3 ile 50 karakter arasında olmalıdır!")]
        public string CustFullName { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Email alanı doldurulması zorunludur!")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz!")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("phone")]
        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Telefon numarası 10 ile 15 karakter arasında olmalıdır.")]
        public string Phone { get; set; } = string.Empty;

        [JsonPropertyName("isCanceled")]
        public bool IsCanceled { get; set; }
    }
}
