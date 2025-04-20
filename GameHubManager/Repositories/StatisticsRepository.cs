using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;


namespace GameHubManager.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly DSContext _context;

        public StatisticsRepository(DSContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationModel>> GetReservationsByaCertainPeriod(DateTime FirstDate, DateTime SecondDate)
        {
            return await _context.Reservations
           .Where(r => r.StartTime >= FirstDate && r.StartTime <= SecondDate)
           .Include(r => r.Device)
           .ToListAsync();
        }

        public async Task<List<SaleModel>> GetSalesByaCertainPeriod(DateTime FirstDate, DateTime SecondDate)
        {
            return await _context.MenuSales
           .Where(r => r.SaleDate >= FirstDate && r.SaleDate <= SecondDate)
           .ToListAsync();
        }

        public async Task<List<ReservationModel>> GetReservationsByToday()
        {
            var TodayDate = DateTime.Now;
            return await _context.Reservations
           .Where(r => r.StartTime.Date == TodayDate.Date)
           .Include(x => x.Device)
           .ThenInclude(y => y.DeviceType)
            .ToListAsync();
        }

        public async Task<List<ReservationModel>> GetReservationsByCurrentMonth()
        {
            var TodayDate = DateTime.Now;

            return await _context.Reservations
           .Where(r => r.StartTime.Month == TodayDate.Month)
           .Include(x => x.Device)
           .ThenInclude(y => y.DeviceType)
           .ToListAsync();
        }

        public async Task<List<SaleModel>> GetSalesByToday()
        {
            var TodayDate = DateTime.Now;
            return await _context.MenuSales
           .Where(r => r.SaleDate.Date == TodayDate.Date)
           .ToListAsync();
        }

        public async Task<List<SaleModel>> GetSalesByCurrentMonth()
        {
            var TodayDate = DateTime.Now;

            return await _context.MenuSales
           .Where(r => r.SaleDate.Month == TodayDate.Month)
           .ToListAsync();
        }

    }
}
