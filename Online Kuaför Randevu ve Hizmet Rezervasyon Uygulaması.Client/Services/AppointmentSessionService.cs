using Data.Models;
namespace Online_Kuaför_Randevu_ve_Hizmet_Rezervasyon_Uygulaması.Client.Services
{
    public class AppointmentSessionService
    {
        public List<Service> SelectedServices { get; set; } = new();
        public Employee? SelectedEmployee { get; set; }
        public DateTime? SelectedDate { get; set; }

        public decimal TotalPrice => SelectedServices.Sum(s => s.Price);    
        public int TotalDuration => SelectedServices.Sum(s => s.Duration);

        public void ClearSession()
        {
            SelectedServices.Clear();
            SelectedEmployee = null;
            SelectedDate = null;
        }
    }
}
