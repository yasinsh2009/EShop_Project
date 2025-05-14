using System.Security.Claims;
using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Account.User;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;

namespace ServiceHost.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Constructor

        private readonly IUserService _userService;
        public static string ReturnUrl { get; set; }

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions

        #region User Register

        [HttpGet("user-register")]
        public async Task<IActionResult> UserRegister()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost("user-register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(UserRegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UserRegister(register);

                switch (result)
                {
                    case UserRegisterResult.Success:
                        TempData[SuccessMessage] = "کاربر با موفقیت ثبت گردید.";
                        TempData[InfoMessage] =
                            $"کد تایید، جهت فعالسازی حساب کاربری به شماره همراه {register.Mobile} ارسال گردید.";
                        break;
                    case UserRegisterResult.MobileExists:
                        TempData[WarningMessage] = $"شماره همراه {register.Mobile} تکراری می باشد.";
                        ModelState.AddModelError("Mobile", "شماره همراه تکراری می باشد.");
                        break;
                    case UserRegisterResult.Error:
                        TempData[ErrorMessage] = "در ثبت اطلاعات خطایی رخ داد، لطفا مجددا امتحان نمایید.";
                        //return RedirectToAction("ActivateMobile", "Account");
                        break;
                }
            }
            return View(register);
        }

        #endregion

        #region User Login

        [HttpGet("user-login")]
        public async Task<IActionResult> UserLogin(string returnUrl)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ReturnUrl = returnUrl;
            
            return View();
        }

        [HttpGet("user-login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(UserLoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UserLogin(login);

                switch (result)
                {
                    case UserLoginResult.Success:
                        var user = await _userService.GetUserByMobile(login.Mobile);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.MobilePhone, login.Mobile),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                            new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe,
                            RedirectUri = HttpContext.Request.Query["RedirectUri"]
                        };

                        await HttpContext.SignInAsync(principal, properties);

                        TempData[SuccessMessage] = "ورود شما با موفقیت انجام شد.";
                        return RedirectToAction("Index", "Home");
                    case UserLoginResult.MobileNotActivated:
                        TempData[ErrorMessage] = $"شماره همراه {login.Mobile} فعال نشده است.";
                        ModelState.AddModelError("Mobile", "شماره همراه شما فعال نشده است.");
                        break;
                    case UserLoginResult.NotFound:
                        TempData[ErrorMessage] = "کاربری با این مشخصات یافت نشد.";
                        ModelState.AddModelError("Mobile", "کاربری با این مشخصات یافت نشد.");
                        break;
                    case UserLoginResult.WrongInformation:
                        TempData[ErrorMessage] = "شماره همراه یا رمز عبور اشتباه است.";
                        ModelState.AddModelError("Mobile", "شماره همراه یا رمز عبور اشتباه است.");
                        break;
                    case UserLoginResult.Error:
                        TempData[ErrorMessage] = "در ورود به حساب کاربری خطایی رخ داد، لطفا مجددا تلاش نمایید.";
                        break;
                }
            }

            return View();
        }

        #endregion

        #endregion
    }
}
