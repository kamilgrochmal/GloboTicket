using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GloboTicket.TicketManagement.Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(GloboTicketDbContext db) : base(db)
        {
        }

        public async Task<List<Order>> GetPagedOrdersForMonth(DateTime date, int page, int size)
        {
            return await _db.Orders.Where(a => a.OrderPlaced.Month == date.Month && a.OrderPlaced.Year == date.Year)
                .Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
            //TODO Check how it works
        }

        public async Task<int> GetTotalCountOfOrdersForMonth(DateTime date)
        {
            return await _db.Orders.CountAsync(a => a.OrderPlaced.Month == date.Month && a.OrderPlaced.Year == date.Year);
        }
    }
}