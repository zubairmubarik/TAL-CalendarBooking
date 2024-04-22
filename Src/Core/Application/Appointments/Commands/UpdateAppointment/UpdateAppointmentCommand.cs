using Application.Appointments.Commands.CreateAppointments;
using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Appointments.Commands.UpdateAppointment
{
    public class UpdateAppointmentCommand : IRequest<AppointmentDto>
    {
        readonly Appointment _appointment;
        public Appointment Appointment { get { return _appointment; } }

        public UpdateAppointmentCommand(Appointment Appointment)
        {
            _appointment = Appointment;
        }
    }

    internal class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentDto>
    {
        readonly IAppointmentRepository _repository;
        readonly IMapper _mapper;
        readonly ILogger<CreateAppointmentCommandHandler> _logger;

        public UpdateAppointmentCommandHandler(IAppointmentRepository repository, ILogger<CreateAppointmentCommandHandler> logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AppointmentDto> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (request?.Appointment == null)
            {
                _logger.LogError("Error: Request object is not available");
                throw new InvalidInputParameterException(typeof(UpdateAppointmentCommand).Name);
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

                //TODO: Find any available slot 
            }

            var newEntity = await _repository.CreateAsync(newAppointment, cancellationToken);

            return _mapper.Map<AppointmentDto>(newEntity);
        }
    }
}
