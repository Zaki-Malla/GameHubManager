using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using GameHubManager.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Controllers

{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public IActionResult StatisticSelection()
        {

            return View("StatisticSelection");
        }

        public async Task<IActionResult> GeneralStatistics()
        {
            var SalesByToday = await _statisticsRepository.GetSalesByToday();
            var ReservationsByToday = await _statisticsRepository.GetReservationsByToday();
            var SalesByCurrentMonth = await _statisticsRepository.GetSalesByCurrentMonth();
            var ReservationsByCurrentMonth = await _statisticsRepository.GetReservationsByCurrentMonth();

            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(((int)today.DayOfWeek + 1) % 7));

            GeneralStatisticsViewModel model = new GeneralStatisticsViewModel
            {
                SalesByDay = Enumerable.Range(0, 7)
                .Select(i =>
                {
                    var date = startOfWeek.AddDays(i);
                    var total = SalesByCurrentMonth
                    .Where(s => s.SaleDate.Date == date)
                    .Sum(s => s.SoldPrice);

                    return (Date: date, DayName: date.ToString("dddd", new System.Globalization.CultureInfo("ar-EG")), Total: total);
                }).ToList(),

                ReservationsByDay = Enumerable.Range(0, 7)
                .Select(i =>
                {
                    var date = startOfWeek.AddDays(i);
                    var total = ReservationsByCurrentMonth
                        .Where(s => s.EndTime.Value.Date == date.Date)
                        .Sum(s => s.AmountPaid) ?? 0;

                    return (Date: date, DayName: date.ToString("dddd", new System.Globalization.CultureInfo("ar-EG")), Total: total);

                }).ToList(),

                TotalFundByDay = Enumerable.Range(0, 7)
                    .Select(i =>
                    {
                        var date = startOfWeek.AddDays(i);

                        var salesTotal = SalesByCurrentMonth
                            .Where(s => s.SaleDate.Date == date)
                            .Sum(s => s.SoldPrice);

                        var reservationsTotal = ReservationsByCurrentMonth
                            .Where(r => r.EndTime.Value.Date == date)
                            .Sum(r => r.AmountPaid) ?? 0;

                        return (Date: date, DayName: date.ToString("dddd", new System.Globalization.CultureInfo("ar-EG")), Total: salesTotal + reservationsTotal);

                    }).ToList(),

                TotalWeeklyPaidSales = SalesByCurrentMonth
                    .Where(s => s.SaleDate.Date >= startOfWeek && s.SaleDate.Date <= today)
                    .Sum(s => s.SoldPrice),

                WeeklyPaidSalesByType = SalesByCurrentMonth
                    .Where(s => s.SaleDate.Date >= startOfWeek && s.SaleDate.Date <= today)
                    .GroupBy(s => s.Category)
                    .Select(g => (TypeName: g.Key, Total: g.Sum(s => s.SoldPrice)))
                    .ToList(),

                TotalWeeklyDueSales = SalesByCurrentMonth
                    .Where(s => s.SaleDate.Date >= startOfWeek && s.SaleDate.Date <= today)
                    .Sum(s => s.DuePrice),

                WeeklyDueSalesByType = SalesByCurrentMonth
                    .Where(s => s.SaleDate.Date >= startOfWeek && s.SaleDate.Date <= today)
                    .GroupBy(s => s.Category)
                    .Select(g => (TypeName: g.Key, Total: g.Sum(s => s.DuePrice)))
                    .ToList(),

                TotalDailyPaidSales = SalesByToday
                .Sum(s => s.SoldPrice),

                DailyPaidSalesByType = SalesByCurrentMonth
                    .Where(s => s.SaleDate.Date == today)
                    .GroupBy(s => s.Category)
                    .Select(g => (TypeName: g.Key, Total: g.Sum(s => s.SoldPrice)))
                    .ToList(),

                TotalDailyDueSales = SalesByToday
                .Sum(s => s.SoldPrice),

                DailyDueSalesByType = SalesByCurrentMonth
                    .Where(s => s.SaleDate.Date == today)
                    .GroupBy(s => s.Category)
                    .Select(g => (TypeName: g.Key, Total: g.Sum(s => s.SoldPrice)))
                    .ToList(),

                TotalDailyPaidReservations = ReservationsByToday
                    .Where(r => r.EndTime.HasValue && r.EndTime.Value.Date == today)
                    .Sum(r => r.AmountPaid) ?? 0,

                DailyPaidReservationsByDevice = ReservationsByToday
                    .Where(r => r.EndTime.HasValue && r.EndTime.Value.Date == today)
                    .GroupBy(r => r.Device.DeviceType.Name)
                    .Select(g => (TypeName: g.Key, Total: g.Sum(r => r.AmountPaid) ?? 0))
                    .ToList(),

                TotalDailyDueReservations = ReservationsByToday
                    .Where(r => r.EndTime.HasValue && r.EndTime.Value.Date == today)
                    .Sum(r => r.AmountDue) ?? 0,

                DailyDueReservationsByDevice = ReservationsByToday
                    .Where(r => r.EndTime.HasValue && r.EndTime.Value.Date == today)
                    .GroupBy(r => r.Device.DeviceType.Name)
                    .Select(g => (TypeName: g.Key, Total: g.Sum(r => r.AmountDue) ?? 0))
                    .ToList(),

                TotalMonthlyPaidReservations = ReservationsByCurrentMonth
                    .Where(r => r.EndTime.HasValue)
                    .Sum(r => r.AmountPaid) ?? 0,

                MonthlyPaidReservationsByDevice = ReservationsByCurrentMonth
                    .Where(r => r.EndTime.HasValue)
                    .GroupBy(r => r.Device.DeviceType.Name)
                    .Select(g => (TypeName: g.Key, Total: g.Sum(r => r.AmountPaid) ?? 0))
                    .ToList(),

                TotalMonthlyDueReservations = ReservationsByCurrentMonth
                    .Where(r => r.EndTime.HasValue)
                    .Sum(r => r.AmountDue) ?? 0,

                MonthlyDueReservationsByDevice = ReservationsByCurrentMonth
                    .Where(r => r.EndTime.HasValue)
                    .GroupBy(r => r.Device.DeviceType.Name)
                    .Select(g => (TypeName: g.Key, Total: g.Sum(r => r.AmountDue) ?? 0))
                    .ToList(),


            };
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> ProfitByCertainPeroid(DateTime firstDate, DateTime SecondDate)
        {

            List<SaleModel> model = await _statisticsRepository.GetSalesByaCertainPeriod(firstDate, SecondDate);

            return View(model);
        }

        public async Task<IActionResult> ProfitByCurrentMonth()
        {
            List<SaleModel> model = await _statisticsRepository.GetSalesByCurrentMonth();
            return View(model);
        }

        public async Task<IActionResult> ProfitByToday()
        {
            List<SaleModel> model = await _statisticsRepository.GetSalesByToday();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ReservationByCertainPeriod(DateTime firstDate, DateTime secondDate)
        {
            List<ReservationModel> model = await _statisticsRepository.GetReservationsByaCertainPeriod(firstDate, secondDate);
            return View(model);

        }


        public async Task<IActionResult> ReservationByCurrentMonth()
        {
            List<ReservationModel> model = await _statisticsRepository.GetReservationsByCurrentMonth();
            return View(model);
        }

        public async Task<IActionResult> ReservationByToday()
        {
            List<ReservationModel> model = await _statisticsRepository.GetReservationsByToday();
            return View(model);
        }


    }
}
