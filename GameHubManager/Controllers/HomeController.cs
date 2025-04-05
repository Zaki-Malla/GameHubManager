using GameHubManager.Models.HelperModel;
using GameHubManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameHubManager.Repositories;

namespace GameHubManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IDeviceRepository deviceRepository;
        private readonly IDeviceTypeRepository deviceTypeRepository;
        private readonly IMenuItemRepository menuItemRepository;
        public HomeController(SignInManager<UserModel> signInManager, IDeviceRepository _deviceRepository, IDeviceTypeRepository _deviceTypeRepository, IMenuItemRepository _menuItemRepository)
        {
            _signInManager = signInManager;
            deviceRepository = _deviceRepository;
            deviceTypeRepository = _deviceTypeRepository;
            menuItemRepository = _menuItemRepository;
        }

        [Authorize]
        public async Task<IActionResult> Dashboard()
        {

            var dashboardViewModel = new DashboardViewModel
            {
                Devices = await deviceRepository.GetAllDevicesWithReservationsAsync(),
                DeviceTypes = await deviceTypeRepository.GetAllDevicesTypesAsync(),
                MenuItems = await menuItemRepository.GetAllMenuItemsAsync()
            };

            return View(dashboardViewModel);
        }



    }
}
