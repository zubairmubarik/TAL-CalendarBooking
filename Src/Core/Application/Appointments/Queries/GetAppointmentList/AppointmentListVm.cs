using Application.Common.Dtos;

namespace Application.Appointments.Queries.GetAppointmentList
{
    public class AppointmentListVm
    {
        public IList<AppointmentDto> Appointments { get; set; }
        public string JsonResponded { get; set; }
        public int Count { get; set; }
    }
}
