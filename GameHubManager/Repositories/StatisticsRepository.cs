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
            .Select(d => new ReservationModel
            {
                Device = new DeviceModel
                {
                    Name = d.Device.Name
                },
                AmountPaid = d.AmountPaid,
                AmountDue = d.AmountDue,
                StartTime = d.StartTime


            })
           .ToListAsync();
        }

        public async Task<List<SaleModel>> GetSalesByaCertainPeriod(DateTime FirstDate, DateTime SecondDate)
        {
            return await _context.MenuSales
           .Where(r => r.SaleDate >= FirstDate && r.SaleDate <= SecondDate)
           .Select(d => new SaleModel
           {
               SoldPrice = d.SoldPrice,
               SaleDate = d.SaleDate,
               ItemName = d.ItemName

           })
           .ToListAsync();
        }

        public async Task<List<ReservationModel>> GetReservationsByToday()
        {
            var TodayDate = DateTime.Now;
            return await _context.Reservations
           .Where(r => r.StartTime.Date == TodayDate.Date)
           .Select(d => new ReservationModel
           {
               Device = new DeviceModel
               {
                   Name = d.Device.Name
               },
               AmountPaid = d.AmountPaid,
               AmountDue = d.AmountDue

           })
            .ToListAsync();
        }

        public async Task<List<ReservationModel>> GetReservationsByCurrentMonth()
        {
            var TodayDate = DateTime.Now;

            return await _context.Reservations
           .Where(r => r.StartTime.Month == TodayDate.Month)
           .Select(d => new ReservationModel
           {
               Device = new DeviceModel
               {
                   Name = d.Device.Name
               },
               AmountPaid = d.AmountPaid,
               AmountDue = d.AmountDue
           })
           .ToListAsync();
        }

        public async Task<List<SaleModel>> GetSalesByToday()
        {
            var TodayDate = DateTime.Now;
            return await _context.MenuSales
           .Where(r => r.SaleDate.Date == TodayDate.Date)
           .Select(s => new SaleModel
           {
               ItemName = s.ItemName,
               SaleDate = s.SaleDate,
               SoldPrice = s.SoldPrice

           })
           .ToListAsync();
        }

        public async Task<List<SaleModel>> GetSalesByCurrentMonth()
        {
            var TodayDate = DateTime.Now;

            return await _context.MenuSales
           .Where(r => r.SaleDate.Month == TodayDate.Month)
           .Select(s => new SaleModel
           {
               ItemName = s.ItemName,
               SaleDate = s.SaleDate,
               SoldPrice = s.SoldPrice


           })
           .ToListAsync();
        }

    }
}
