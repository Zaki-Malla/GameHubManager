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
        private readonly IGroupReservationRepository _groupReservationRepository;

        public ReservationController(UserManager<UserModel> userManager, IDeviceRepository deviceRepository, IReservationRepository reservationRepository , IGroupReservationRepository groupReservationRepository)
        {
            _userManager = userManager;
            _deviceRepository = deviceRepository;
            _reservationRepository = reservationRepository;
            _groupReservationRepository = groupReservationRepository;
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

        [HttpPost]
        public async Task<IActionResult> GroupReserveDevices(GroupReservationViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Dashboard", "Home");
            }

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Error";
                return RedirectToAction("Dashboard", "Home");
            }

            if (string.IsNullOrWhiteSpace(model.SelectedDeviceIds))
            {
                TempData["Message"] = "لم يتم اختيار أي جهاز.";
                return RedirectToAction("Dashboard", "Home");
            }

            var deviceIds = model.SelectedDeviceIds
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(id => int.Parse(id))
                                .ToList();

            if (!deviceIds.Any())
            {
                TempData["Message"] = "يجب اختيار جهاز واحد على الأقل.";
                return RedirectToAction("Dashboard", "Home");
            }

            var groupReservation = new GroupReservationModel
            {
                StartTime = model.StartTime,
                EndTime = model.IsOpenReservation ? (DateTime?)null : DateTime.Now.AddMinutes(model.TotalMinutes ?? 0),
                TotalDevices = deviceIds.Count,
                UserId = user.Id
            };

            await _groupReservationRepository.AddGroupReservationAsync(groupReservation);

            foreach (var deviceId in deviceIds)
            {
                var reservation = new ReservationModel
                {
                    DeviceId = deviceId,
                    StartTime = model.StartTime,
                    EndTime = model.IsOpenReservation ? (DateTime?)null : DateTime.Now.AddMinutes(model.TotalMinutes ?? 0),
                    TotalMinutes = model.IsOpenReservation ? null : model.TotalMinutes,
                    AmountPaid = model.IsOpenReservation ? null : model.AmountPaid,
                    AmountDue = model.IsOpenReservation ? null : model.AmountPaid,
                    NumberOfControllers = 2,
                    GroupReservationId = groupReservation.Id,
                    UserId = user.Id
                };

                await _reservationRepository.AddReservationAsync(reservation);
            }

            TempData["Message"] = "Success";
            return RedirectToAction("Dashboard", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> EndGroupReservationNow([FromBody] EndReservationRequest request)
        {
            var group = await _groupReservationRepository.GetGroupReservationsByIdAsync(request.GroupReservationId);

            if (group == null)
                return NotFound();

            var now = DateTime.Now;

            group.EndTime = now;

            await _groupReservationRepository.UpdateReservationAsync(group);

            foreach (var res in group.Reservations)
            {
                res.EndTime = now;
                if (res.StartTime != null && res.EndTime != null)
                {
                    var totalMinutes = (res.EndTime.Value - res.StartTime).TotalMinutes;
                    res.TotalMinutes = (int?)Math.Round(totalMinutes);
                }
                res.AmountDue = request.duoAmountPerDevice;
                res.AmountPaid = request.paidAmountPerDevice;
                await _reservationRepository.UpdateReservationAsync(res);
            }

            return Ok();
        }

    }
}
