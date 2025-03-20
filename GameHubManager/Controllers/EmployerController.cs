using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameHubManager.Controllers
{
    [Authorize(Roles = "Employer")]
    public class EmployerController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        public EmployerController(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
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
    }
}
