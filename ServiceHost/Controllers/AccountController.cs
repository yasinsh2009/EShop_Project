using System.Security.Claims;
using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Account.User;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Constructor

        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;
        public static string ReturnUrl { get; set; }

        public AccountController(IUserService userService, ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }

        #endregion

        #region Actions

        #region User Register

        [HttpGet("user-register")]
        public async Task<IActionResult> UserRegister()
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return Redirect("/");
            }
            return View();
        }

        [HttpPost("user-register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRegister(UserRegisterDto register)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(register.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچای شما تایید نشد.";
                TempData[WarningMessage] = "لطفا از اتصال اینترنت خود اطمینان حاصل فرمایید";
                return View(register);
            }

            if (ModelState.IsValid)
            {
                if (!await _userService.IsUserExistByMobile(register.Mobile))
                {
                    return RedirectToAction("UserLogin", "Account",
                        new { mobile = register.Mobile });
                }

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
                        return RedirectToAction("ActivateMobile", "Account",
                            new { mobile = register.Mobile});
                }
            }
            return View(register);
        }

        #endregion

        #region User Login

        [HttpGet("user-login")]
        public async Task<IActionResult> UserLogin(string returnUrl, string mobile)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return RedirectToAction("Index", "Home");
            }

            ReturnUrl = returnUrl;

            ViewBag.Mobile = mobile;

            return View();
        }

        [HttpGet("user-login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLogin(UserLoginDto login, string mobile)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(login.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچای شما تایید نشد.";
                TempData[WarningMessage] = "لطفا از اتصال اینترنت خود اطمینان حاصل فرمایید";
                return View(login);
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.UserLogin(login);

                switch (result)
                {
                    case UserLoginResult.Success:

                        var user = await _userService.GetUserByMobile(mobile);

                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.MobilePhone, mobile),
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

                        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }

                        return RedirectToAction("Index", "Home");
                    case UserLoginResult.MobileNotActivated:
                        TempData[ErrorMessage] = $"شماره همراه {mobile} فعال نشده است.";
                        ModelState.AddModelError("Mobile", "شماره همراه شما فعال نشده است.");
                        break;
                    case UserLoginResult.NotFound:
                        TempData[ErrorMessage] = "کاربری با این مشخصات یافت نشد.";
                        ModelState.AddModelError("Mobile", "کاربری با این مشخصات یافت نشد.");
                        break;
                    case UserLoginResult.WrongInformation:
                        TempData[ErrorMessage] = "رمز عبور اشتباه است.";
                        ModelState.AddModelError("Mobile", "رمز عبور اشتباه است.");
                        break;
                    case UserLoginResult.Error:
                        TempData[ErrorMessage] = "در ورود به حساب کاربری خطایی رخ داد، لطفا مجددا تلاش نمایید.";
                        break;
                }
            }

            return View();
        }

        #endregion

        #region Activation Mobile

        [HttpGet("activation-mobile/{mobile}")]
        public async Task<IActionResult> ActivateMobile(string mobile)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return Redirect("/");
            }

            var activateMobile = new ActivateMobileDto() { Mobile = mobile };
            return View(activateMobile);
        }

        [HttpPost("activation-mobile/{mobile}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivateMobile(ActivateMobileDto activateMobile)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(activateMobile.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچای شما تایید نشد.";
                TempData[WarningMessage] = "لطفا از اتصال اینترنت خود اطمینان حاصل فرمایید";
                return View(activateMobile);
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.ActivateMobile(activateMobile);

                if (result)
                {
                    TempData[SuccessMessage] = "حساب کاربری شما با موفقیت فعال گردید";
                    TempData[InfoMessage] = "شما وارد حساب کاربری خود شدید";

                    return Redirect("/");
                }

                TempData[ErrorMessage] = "متاسفانه حساب کاربری شما فعال نشد";
            }

            return View(activateMobile);
        }
        #endregion

        #endregion
    }
}
