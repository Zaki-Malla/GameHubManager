using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using GameHubManager.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployerController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IDeviceRepository _deviceRepository;

        public EmployerController(UserManager<UserModel> userManager, IDeviceTypeRepository deviceTypeRepository, ISaleRepository saleRepository, IMenuItemRepository menuItemRepository, IDeviceRepository deviceRepository)
        {
            _userManager = userManager;
            _deviceTypeRepository = deviceTypeRepository;
            _saleRepository = saleRepository;
            _menuItemRepository = menuItemRepository;
            _deviceRepository = deviceRepository;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveChanges(ChangeEmailPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ChangeEmail)
                {
                   
                    var user = _userManager.GetUserAsync(User).Result;
                    var isPasswordValid = _userManager.CheckPasswordAsync(user, model.OldPassword).Result;

                    if (!isPasswordValid)
                    {
                        ModelState.AddModelError(string.Empty, "كلمة المرور غير صحيحة.");
                        return View("Dashboard",model);
                    }

                    user.Email = model.NewEmail;
                    var result = _userManager.UpdateAsync(user).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Success");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "فشل تغيير البريد الإلكتروني.");
                        return View("Dashboard", model);
                    }
                }
                else
                {
                    var user = _userManager.GetUserAsync(User).Result;
                    var isPasswordValid = _userManager.CheckPasswordAsync(user, model.OldPassword).Result;

                    if (!isPasswordValid)
                    {
                        ModelState.AddModelError(string.Empty, "كلمة المرور القديمة غير صحيحة.");
                        return View("Dashboard", model);
                    }

                    var changePasswordResult = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).Result;

                    if (changePasswordResult.Succeeded)
                    {
                        return RedirectToAction("Success");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "فشل تغيير كلمة المرور.");
                        return View("Dashboard", model);
                    }
                }
            }

             return View("Dashboard",model);
        }

        public async Task<IActionResult> DevicesTypes()
        {
            return View(await _deviceTypeRepository.GetAllDevicesTypesAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDeviceType(DeviceTypeModel model, IFormFile ImageFile)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            if (model.Id == 0)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    model.ImagePath = "/images/" + uniqueFileName;
                }
                await _deviceTypeRepository.AddDeviceTypeAsync(model);
            }
            else
            {
                var existingDeviceType = await _deviceTypeRepository.GetDevicesTypesByIdAsync(model.Id);
                if (existingDeviceType != null)
                {
                    existingDeviceType.Name = model.Name;

                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }

                        existingDeviceType.ImagePath = "/images/" + uniqueFileName;
                    }
                }
                await _deviceTypeRepository.UpdateDeviceTypeAsync(existingDeviceType);
            }

            return RedirectToAction("DevicesTypes");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDeviceType(int DeviceId)
        {
            var deviceType = await _deviceTypeRepository.GetDevicesTypesByIdAsync(DeviceId);
            if (deviceType == null)
            {
                return NotFound();
            }

            await _deviceTypeRepository.DeleteDeviceTypeAsync(DeviceId); 
            return RedirectToAction("DevicesTypes");
        }


        public IActionResult DevicesPrices()
        {
            return View();
        }
        public IActionResult Statistics()
        {
            return View();
        }
        public async Task<IActionResult> SnacksManage()
        {
            return View(await _menuItemRepository.GetAllMenuItemsAsync());
        }
        public async Task<IActionResult> AddMenuItem(MenuItemModel menuItem)
        {
            var existingItem = await _menuItemRepository.GetMenuItemByNameAsync(menuItem.Name);
            if (existingItem != null)
            {
                ModelState.AddModelError("Name", "المنتج موجود بالفعل، يجب عليك إعادة تعبئة الكمية.");
            }

            if (menuItem.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "الكمية يجب أن تكون أكبر من صفر.");
            }

            if (!ModelState.IsValid)
            {
                return View("SnacksManage", await _menuItemRepository.GetAllMenuItemsAsync());
            }

            await _menuItemRepository.AddMenuItemAsync(menuItem);
            return RedirectToAction("SnacksManage");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int menuItemId2, int newQuantity)
        {
            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId2);
            if (menuItem == null)
            {
                return NotFound();
            }

            if (newQuantity < 1)
            {
                ModelState.AddModelError("newQuantity", "يجب إدخال كمية أكبر من 0");
                return View("SnacksManage",menuItem); 
            }

            menuItem.Quantity += newQuantity;
            await _menuItemRepository.UpdateMenuItemAsync(menuItem);
            return RedirectToAction("SnacksManage");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePrice(int menuItemId, decimal newPrice)
        {
            if (newPrice < 0)
            {
                ModelState.AddModelError("newPrice", "السعر يجب أن يكون أكبر من 0.");
                return View("SnacksManage");
            }

            var menuItem = await _menuItemRepository.GetMenuItemByIdAsync(menuItemId);
            if (menuItem == null)
            {
                return NotFound();
            }

            menuItem.Price = newPrice;
            _menuItemRepository.UpdateMenuItemAsync(menuItem);

            return RedirectToAction("SnacksManage");
        }

        public async Task<IActionResult> Devices()
        {
            AddDeviceViewModel model = new AddDeviceViewModel { 
                Devices = await _deviceRepository.GetAllDevicesAsync(),
                DeviceTypes = await _deviceTypeRepository.GetAllDevicesTypesAsync()
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateDevice(DeviceModel model)
        {

            if (model.Id == 0)
            {
                await _deviceRepository.AddDeviceAsync(model);
            }
            else
            {
                var existingDevice = await _deviceRepository.GetDevicesByIdAsync(model.Id);
                if (existingDevice != null)
                {
                    existingDevice.Name = model.Name;
                    existingDevice.DeviceType = await _deviceTypeRepository.GetDevicesTypesByIdAsync(model.DeviceTypeId);
                }
                await _deviceRepository.UpdateDeviceAsync(existingDevice);
            }

            return RedirectToAction("Devices");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDevice(int DeviceId)
        {
            var device = await _deviceRepository.GetDevicesByIdAsync(DeviceId);
            if (device == null)
            {
                return NotFound();
            }

            await _deviceRepository.DeleteDeviceAsync(DeviceId);
            return RedirectToAction("Devices");
        }


    }
}
