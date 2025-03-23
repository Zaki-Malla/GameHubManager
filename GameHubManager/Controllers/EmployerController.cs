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
        public EmployerController(UserManager<UserModel> userManager, IDeviceTypeRepository deviceTypeRepository)
        {
            _userManager = userManager;
            _deviceTypeRepository = deviceTypeRepository;
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
        public async Task<IActionResult> AddOrUpdate(DeviceTypeModel model, IFormFile ImageFile)
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
        public IActionResult SnacksStatistics()
        {
            return View();
        }
    }
}
