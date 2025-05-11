using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Account.User;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ServiceHost.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Constructor

        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions

        #region Register User

        [HttpGet("register-user")]
        public async Task<IActionResult> RegisterUser()
        {
            return View();
        }

        [HttpPost("register-user"), ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO register)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(register);

                switch (result)
                {
                    case RegisterUserResult.Success:
                        TempData["SuccessMessage"] =
                            $"کد تایید، جهت فعالسازی حساب کاربری به شماره همراه {register.Mobile} ارسال گردید.";
                        break;
                    case RegisterUserResult.MobileExists:
                        TempData["WarningMessage"] = $"شماره همراه {register.Mobile} تکراری می باشد.";
                        ModelState.AddModelError("Mobile", "شماره همراه تکراری می باشد.");
                        break;
                    case RegisterUserResult.Error:
                        TempData["ErrorMessage"] = "در ثبت اطلاعات خطایی رخ داد، لطفا مجددا امتحان نمایید.";
                        return RedirectToAction("ActivateMobile", "Account");
                }

                return View(register);
            }
        }

        #endregion

        #endregion
    }
}
