using Data.Models;
namespace Online_Kuaför_Randevu_ve_Hizmet_Rezervasyon_Uygulaması.Client.Services
{
    public class AppointmentBusinessService
    {
        public List<Employee> GetEligibleEmployees(List<Employee> allEmployees, List<String> selectedServiceIDs)
        {
            if(selectedServiceIDs == null || !selectedServiceIDs.Any())
            {
                return new List<Employee>();
            }

            return allEmployees.Where(e => selectedServiceIDs.All(id => e.Services.Contains(id))).ToList();
        }

        public bool IsSlotAvailable(string employeeId, DateTime newStart, DateTime newEnd, List<BusySlot> busySlots, List<Appointment> localAppointments)
        {
            TimeSpan openTime = new TimeSpan(9, 0, 0);
            TimeSpan closeTime = new TimeSpan(19, 0, 0);

            if(newStart.TimeOfDay < openTime || newEnd.TimeOfDay > closeTime)
            {
                return false;
            }

            bool hasBusySlotConflict = busySlots.Any(b => b.EpId == employeeId && newStart < b.EndAt && newEnd > b.StartAt);

            if (hasBusySlotConflict) return false;

            bool hasAppointmentConflict = localAppointments.Any(a => a.EpId == employeeId && !a.IsCanceled && newStart < a.EndAt && newEnd > a.StartAt);

            return !hasAppointmentConflict;
        }
    }
}
