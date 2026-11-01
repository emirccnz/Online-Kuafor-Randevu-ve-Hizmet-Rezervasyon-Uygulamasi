using Data.Models;

namespace Online_Kuafor_Randevu_ve_Hizmet_Rezervasyon_Uygulamasi.Client.Services
{
    public class AppointmentSessionService
    {
        public List<Service> SelectedServices { get; set; } = new();
        public Employee? SelectedEmployee { get; set; }

        public DateTime? SelectedDate { get; set; }
        public TimeSpan? SelectedTime { get; set; } 

        public decimal TotalPrice => SelectedServices.Sum(s => s.Price);
        public int TotalDuration => SelectedServices.Sum(s => s.Duration);

        public DateTime? GetExactStartDateTime()
        {
            if (SelectedDate.HasValue && SelectedTime.HasValue)
                return SelectedDate.Value.Date + SelectedTime.Value;
            return null;
        }

        public void ClearSession()
        {
            SelectedServices.Clear();
            SelectedEmployee = null;
            SelectedDate = null;
            SelectedTime = null;
        }
    }
}