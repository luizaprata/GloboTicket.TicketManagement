﻿
using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Infrastructure;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Models.Mail;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>

    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger;

        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService, ILogger<CreateEventCommandHandler> logger)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _emailService = emailService;
            _logger = logger;

        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidatonException(validationResult);

            var @event = _mapper.Map<Event>(request);
            @event = await _eventRepository.AddAsync(@event);

            var email = new Email() { To = "luiza.psp@gmail.com", Body = $"A new Event was created: {request}", Subject = "A bew Event" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
            }

            return @event.EventId;
        }
    }
}
