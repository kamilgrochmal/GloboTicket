using System;
using MediatR;

namespace GloboTicket.TicketManagement.Application.Features.Events
{
    public class GetEventDetailQuery : IRequest<EventDetailVM>
    {
        public Guid Id { get; set; }
    }
}