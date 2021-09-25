using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;

namespace GloboTicket.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventCommandValidator(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;

            RuleFor(a => a.Name)
                .NotEmpty().WithMessage("Field is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("Maximum length is 50.");

            RuleFor(a => a)
                .MustAsync(EventNameAndDateUnique)
                .WithMessage("An event with the same name and date already exists.");

            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("Field is required.")
                .NotNull()
                .GreaterThan(DateTime.Now);

            RuleFor(a => a.Price)
                .NotEmpty().WithMessage("Field is required.")
                .NotNull()
                .GreaterThan(0);
        }

        private async Task<bool> EventNameAndDateUnique(CreateEventCommand eventCommand, CancellationToken token)
        {
            return !await _eventRepository.IsEventNameAndDateUnique(eventCommand.Name, eventCommand.Date);
        }
    }
}