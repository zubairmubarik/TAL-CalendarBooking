using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Application.Common.Extensions;


namespace Application.Appointments.Commands.DeleteAppointment
{
    public class DeleteAppointmentCommand : IRequest
    {
        private readonly DateTime _slotDateTime;
       
        public DateTime SlotDateTime { get { return _slotDateTime; } }
        public DeleteAppointmentCommand(DateTime slotDateTime)
        {
            _slotDateTime = slotDateTime;
        }
    }
    internal class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand>
    {
        readonly IAppointmentRepository _repository;
        readonly IMapper _mapper;       
        readonly ILogger<DeleteAppointmentCommandHandler> _logger;
        public DeleteAppointmentCommandHandler(IAppointmentRepository repository, IMapper mapper, ILogger<DeleteAppointmentCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (request.SlotDateTime.IsMinValue())
            {
                _logger.LogError("Error: Appointment Id {0} is invalid DateTime", request.SlotDateTime);
                throw new InvalidInputParameterException(typeof(DeleteAppointmentCommandHandler).Name);
            }

            var slotDateTime = request.SlotDateTime;
            await _repository.DeleteBySlotAsync(slotDateTime, cancellationToken);
            return Unit.Value;
        }
    }
}
