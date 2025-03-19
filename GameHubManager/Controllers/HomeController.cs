using GameHubManager.Models.HelperModel;
using GameHubManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly DSContext _dsContext;
        public HomeController(SignInManager<UserModel> signInManager, DSContext dsContext)
        {
            _signInManager = signInManager;
            _dsContext = dsContext;
        }

        [Authorize]
        public IActionResult Dashboard()
        {
            var devices = _dsContext.Devices
        .Include(d => d.DeviceType)
            .ThenInclude(dt => dt.DevicePrice)
        .Include(d => d.Reservations)
        .Select(d => new DeviceViewModel
        {
            Device = new DeviceModel
            {
                Id = d.Id,
                Name = d.Name,
                DeviceType = d.DeviceType,
            },
            ActiveReservation = d.Reservations
                .FirstOrDefault(r => r.EndTime > DateTime.Now || r.EndTime == null)
        })
        .ToList();

            var deviceTypes = _dsContext.DeviceTypes.Include(d => d.Devices).ToList();

            var dashboardViewModel = new DashboardViewModel
            {
                Devices = devices,
                DeviceTypes = deviceTypes
            };

            return View(dashboardViewModel);
        }



    }
}
