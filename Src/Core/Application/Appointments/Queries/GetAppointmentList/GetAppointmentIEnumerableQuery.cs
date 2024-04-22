using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Text.Json;

namespace Application.Appointments.Queries.GetAppointmentList
{
    public class GetAppointmentIEnumerableQuery : IRequest<ResponseIEnumerableDto<AppointmentDto>>
    {
        // TODO
        // Pagging
        // Searching
        // Filter
        // Sort

        // From Slot Date and To Slot Date

        private readonly DateTime _slot;

        public DateTime SlotDateTime { get { return _slot; } }

        public GetAppointmentIEnumerableQuery(DateTime slot)
        {
            _slot = slot;
        }
    }

    public class GetAppointmentsEnumerableQueryHandler : IRequestHandler<GetAppointmentIEnumerableQuery, ResponseIEnumerableDto<AppointmentDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentsEnumerableQueryHandler(IAppointmentRepository repository, IMapper mapper)
        {
            _appointmentRepository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseIEnumerableDto<AppointmentDto>> Handle(GetAppointmentIEnumerableQuery request, CancellationToken cancellationToken)
        {
            // TODO : request pagging, searching, adv searching, filter & ssort

            var occupiedSlots = _appointmentRepository.GetAvailableItemsAsEnumerable(x => x.IsActive == true
                                && x.SlotStartTime.Date == request.SlotDateTime.Date
                                , cancellationToken).ToList();



            // Find available time slots
            var availableSlots = FindAvailableSlots(request.SlotDateTime.Add(new TimeSpan(9, 0, 0)),
                request.SlotDateTime.Add(new TimeSpan(17, 0, 0)), occupiedSlots);

            var results = _mapper.Map<IEnumerable<AppointmentDto>>(availableSlots);

            return new ResponseIEnumerableDto<AppointmentDto>()
            {
                Value = results,
                Count = results.Count(),
                IsError = false,
                ResponseCode = ResponseCode.Ok,
                JsonResponded = JsonSerializer.Serialize(results)
            };
        }

        // TODO: Make common function
        public List<Appointment> FindAvailableSlots(DateTime startTime, DateTime endTime, List<Appointment> occupiedSlots)
        {
            // Generate all time slots within the range
            var allSlots = new List<DateTime>();
            DateTime currentSlot = startTime;
            while (currentSlot < endTime)
            {
                allSlots.Add(currentSlot);
                currentSlot = currentSlot.AddMinutes(30); // Assuming slots are 30 mins long
            }

            // Filter out occupied slots
            var availableSlots = allSlots.Where(slot => !occupiedSlots.Any(occupied => occupied.SlotStartTime == slot)).ToList();

            // TODO: Improve below code
            List<Appointment> availableAppointments = new List<Appointment>();
            foreach (var slot in availableSlots)
                availableAppointments.Add(new Appointment() { SlotStartTime = slot });

            return availableAppointments;
        }
    }
}
