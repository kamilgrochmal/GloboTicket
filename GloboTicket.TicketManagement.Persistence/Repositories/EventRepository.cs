using System;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.TicketManagement.Infrastructure.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository 
    {
        public EventRepository(GloboTicketDbContext db) : base(db)
        {
        }

        public Task<bool> IsEventNameAndDateUnique(string eventName, DateTime eventDate)
        {
            var matches = _db.Events.Any(a => a.Name == eventName && a.Date == eventDate);
            return Task.FromResult(matches);
        }
    }
}