using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.TicketManagement.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(GloboTicketDbContext db) : base(db)
        {
        }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includePastEvents)
        {
            var categoriesWithEvents =
                _db.Categories.Include(a => a.Events.Where(a => a.Date >= DateTime.Now));

            if (includePastEvents)
                categoriesWithEvents.Include(a => a.Events);

            return await categoriesWithEvents.ToListAsync();
        }
    }
}