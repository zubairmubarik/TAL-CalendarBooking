using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Appointments.Commands.CreateAppointments
{
    public class CreateAppointmentCommand : IRequest<AppointmentDto>
    {
        readonly Appointment _appointment;        
        public Appointment Appointment { get { return _appointment; } }

        public CreateAppointmentCommand(Appointment Appointment)
        {
            _appointment = Appointment;
        }
    }
    internal class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
    {
        readonly IAppointmentRepository _repository;
        readonly IMapper _mapper;
        readonly ILogger<CreateAppointmentCommandHandler> _logger;
       
        public CreateAppointmentCommandHandler(IAppointmentRepository repository, ILogger<CreateAppointmentCommandHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (request?.Appointment == null)
            {
                _logger.LogError("Error: Request object is not available");
                throw new InvalidInputParameterException(typeof(CreateAppointmentCommand).Name);
            }

            var newAppointment = request.Appointment;

            //TODO: We need seprate Validator and apply Unit Tests
            if (!(newAppointment.SlotStartTime.TimeOfDay >= TimeSpan.FromHours(9) 
                    && newAppointment.SlotStartTime.TimeOfDay <= TimeSpan.FromHours(17)))  //9AM and 5PM
            {
                _logger.LogError($"Appointment  {newAppointment.SlotStartTime} is not allowed ");
                // TODO: Response Message to return validation
                return new AppointmentDto();
            }

            if (_repository.GetItemsAsQueryable(p => p.SlotStartTime >= newAppointment.SlotStartTime 
                                                && p.SlotEndTime <= newAppointment.SlotEndTime, cancellationToken).Any())
            {
                   _logger.LogInformation($"Appointment exist from {newAppointment.SlotStartTime} - {newAppointment.SlotEndTime} ");
                // TODO: Response Message to return validation
                return new AppointmentDto();
            }

            var newEntity = await _repository.CreateAsync(newAppointment, cancellationToken);

            return _mapper.Map<AppointmentDto>(newEntity);
        }
    }
}
