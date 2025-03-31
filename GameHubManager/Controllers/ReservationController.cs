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
        public async Task<IActionResult> ReserveDevice([FromBody] ReservationRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var device = await _deviceRepository.GetDevicesByIdAsync(request.deviceId);
            if (device == null)
            {
                return NotFound();
            }

            if (request.endTime <= DateTime.Now)
            {
                return BadRequest("يجب أن يكون وقت الانتهاء بعد وقت البداية.");
            }

            if (request.totalMinutes <= 0)
            {
                return BadRequest("عدد الدقائق يجب أن يكون أكبر من صفر.");
            }

            if (request.amountPaid < 0)
            {
                return BadRequest("المبلغ المدفوع لا يمكن أن يكون سالباً.");
            }

            if (await _reservationRepository.IsDeviceReservedAsync(request.deviceId))
            {
                return BadRequest("هذا الجهاز محجوز حالياً.");
            }

            var reservation = new ReservationModel
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddMinutes(request.totalMinutes),
                TotalMinutes = request.totalMinutes,
                AmountDue = request.amountPaid,
                AmountPaid = request.amountPaid,
                DeviceId = request.deviceId,
                UserId = user.Id
            };

            await _reservationRepository.AddReservationAsync(reservation);

            return Ok(new { message = "تم الحجز بنجاح!" });
        }

    }
}
