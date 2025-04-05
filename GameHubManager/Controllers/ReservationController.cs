using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using GameHubManager.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(UserManager<UserModel> userManager, IDeviceRepository deviceRepository, IReservationRepository reservationRepository)
        {
            _userManager = userManager;
            _deviceRepository = deviceRepository;
            _reservationRepository = reservationRepository;
        }
        [HttpPost]
        public async Task<IActionResult> ReserveDevice(ReservationRequestModel request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Dashboard", "Home");
            }

            var device = await _deviceRepository.GetDevicesByIdAsync(request.deviceId);
            if (device == null)
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Dashboard", "Home");
            }

            if (request.IsOpenReservation)
            {
                var reservation = new ReservationModel
                {
                    UserId = user.Id,
                    StartTime = DateTime.Now,
                    DeviceId = request.deviceId,
                    NumberOfControllers = request.NumberOfControllers,
                };
                await _reservationRepository.AddReservationAsync(reservation);

            }
            else { 
            if (request.endTime <= DateTime.Now)
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Dashboard", "Home");
            }

            if (request.totalMinutes <= 0)
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Dashboard", "Home");
            }

            if (request.amountPaid < 0)
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Dashboard", "Home");
            }

            if (await _reservationRepository.IsDeviceReservedAsync(request.deviceId))
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Dashboard", "Home");
            }

            var reservation = new ReservationModel
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(request.totalMinutes.Value),
                TotalMinutes = request.totalMinutes,
                AmountDue = request.amountPaid.Value,
                AmountPaid = request.amountPaid.Value,
                DeviceId = request.deviceId,
                UserId = user.Id,
                NumberOfControllers = request.NumberOfControllers
            };

            await _reservationRepository.AddReservationAsync(reservation);
            }

            TempData["Message"] = "Success";
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost("Reservation/CloseReservation")]
        public async Task<IActionResult> CloseReservation(int reservationId, double amountPaid, double amountDue)
        {
            var reservation = await _reservationRepository.GetReservationsByIdAsync(reservationId);

            if (reservation == null)
            {
                return Json(new { status = "Error", message = "الجهاز غير موجود" });
            }

            reservation.EndTime = DateTime.Now;
            if (reservation.EndTime != null)
            {
                TimeSpan duration = reservation.EndTime.Value - reservation.StartTime;
                reservation.TotalMinutes = (int)duration.TotalMinutes;
            }
            reservation.AmountPaid = (decimal)amountPaid;
            reservation.AmountDue = (decimal)amountDue;

            await _reservationRepository.UpdateReservationAsync(reservation);
            return Json(new { status = "OK", message = "تمت العملية بنجاح" });
        }


    }
}
