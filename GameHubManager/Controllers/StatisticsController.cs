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
            StatisticsViewModel model = new StatisticsViewModel
            {
                SalesByToday = await _statisticsRepository.GetSalesByToday(),
                ReservationsByToday = await _statisticsRepository.GetReservationsByToday(),
                SalesByCurrentMonth = await _statisticsRepository.GetSalesByCurrentMonth(),
                ReservationsByCurrentMonth = await _statisticsRepository.GetReservationsByCurrentMonth()

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
