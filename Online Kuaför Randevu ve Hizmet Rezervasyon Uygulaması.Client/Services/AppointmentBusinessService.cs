using Data.Models;
using Online_Kuaför_Randevu_ve_Hizmet_Rezervasyon_Uygulaması.Client.Services;

namespace Online_Kuafor_Randevu_ve_Hizmet_Rezervasyon_Uygulamasi.Client.Services
{
    public class AppointmentBusinessService
    {
        public List<Employee> GetEligibleEmployees(List<Employee> allEmployees, List<string> selectedServiceIDs)
        {
            if (selectedServiceIDs == null || !selectedServiceIDs.Any()) return new List<Employee>();
            return allEmployees.Where(e => selectedServiceIDs.All(id => e.Services.Contains(id))).ToList();
        }

        public bool IsSlotAvailable(string employeeId, DateTime newStart, DateTime newEnd, List<BusySlot> busySlots, List<Appointment> localAppointments)
        {
            if (newStart <= DateTime.Now) return false;

            TimeSpan openTime = new TimeSpan(9, 0, 0);
            TimeSpan closeTime = new TimeSpan(19, 0, 0);

            if (newStart.TimeOfDay < openTime || newEnd.TimeOfDay > closeTime) return false;
            if (newStart.Date != newEnd.Date) return false; 

            bool hasBusySlotConflict = busySlots.Any(b =>
                b.EpId == employeeId && newStart < b.EndAt && newEnd > b.StartAt);
            if (hasBusySlotConflict) return false;

            bool hasAppointmentConflict = localAppointments.Any(a =>
                a.EpId == employeeId && !a.IsCanceled && newStart < a.EndAt && newEnd > a.StartAt);

            return !hasAppointmentConflict;
        }

        public async Task<(bool IsSuccess, string Message, Appointment? Data)> CreateAppointmentAsync(
            Appointment draftAppointment,
            List<Service> selectedServices,
            List<BusySlot> currentBusySlots,
            LocalStorageService localStorage)
        {
            draftAppointment.Price = selectedServices.Sum(s => s.Price);
            int totalDuration = selectedServices.Sum(s => s.Duration);
            draftAppointment.EndAt = draftAppointment.StartAt.AddMinutes(totalDuration);

            var localAppointments = await localStorage.GetItemAsync<List<Appointment>>("appointments") ?? new List<Appointment>();

            bool isAvailable = IsSlotAvailable(draftAppointment.EpId, draftAppointment.StartAt, draftAppointment.EndAt, currentBusySlots, localAppointments);

            if (!isAvailable)
                return (false, "Seçilen saat dilimi artık uygun değil veya çalışma saatleri dışında.", null);

            string uniqueCode = $"KFR-{DateTime.Now.Year}-{new Random().Next(1000, 9999)}";
            draftAppointment.Id = uniqueCode;
            draftAppointment.IsCanceled = false;

            draftAppointment.Services = selectedServices.Select(s => s.Id).ToList();

            localAppointments.Add(draftAppointment);
            await localStorage.SetItemAsync("appointments", localAppointments);

            return (true, "Randevunuz başarıyla oluşturuldu.", draftAppointment);
        }

        public async Task<(bool IsSuccess, string Message)> CancelAppointmentAsync(string appointmentId, LocalStorageService localStorage)
        {
            var localAppointments = await localStorage.GetItemAsync<List<Appointment>>("appointments") ?? new List<Appointment>();

            var appt = localAppointments.FirstOrDefault(a => a.Id == appointmentId);
            if (appt == null) return (false, "Randevu bulunamadı.");
            if (appt.IsCanceled) return (false, "Randevu zaten iptal edilmiş.");

            if (appt.StartAt <= DateTime.Now.AddHours(2))
                return (false, "Randevuya 2 saatten az kaldığı veya saati geçtiği için iptal edilemez.");

            appt.IsCanceled = true;
            await localStorage.SetItemAsync("appointments", localAppointments);

            return (true, "Randevunuz iptal edildi. Bu saat dilimi artık uygun saat hesaplamasında dolu kabul edilmeyecektir.");
        }
    }
}