using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Dtos
{
    public class AppointmentDto : IMapFrom<Appointment>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }        
        public DateTime SlotStartTime { get; set; }
        public DateTime SlotEndTime { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Appointment, AppointmentDto>();
        }
    }
}
