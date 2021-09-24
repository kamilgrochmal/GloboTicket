using System;

namespace GloboTicket.TicketManagement.Application.Features.Events
{
    public class EventListVM
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
    }
}