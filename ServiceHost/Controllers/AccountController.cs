using EShop.Application.Services.Interface;
using EShop.Domain.Entities.Account.User;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using EShop.Domain.DTOs.Account.User;

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

        #region User Validation

        [HttpGet("user-validation")]
        public IActionResult UserValidation()
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost("user-validation"), ValidateAntiForgeryToken]
        public async Task<IActionResult> UserValidation(UserValidationDto validate)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(validate.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچای شما تایید نشد.";
                TempData[WarningMessage] = "لطفا از اتصال اینترنت خود اطمینان حاصل فرمایید";
                return View(validate);
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.IsUserValidate(validate);

                switch (result)
                {
                    case UserValidationResult.ExistAndActive:
                        return RedirectToAction("UserLogin", "Account",
                            new { mobile = validate.Mobile });
                    case UserValidationResult.ExistAndNotActive:
                        string activationText =
                            $"به نظر می رسد که حساب شما فعال نیست، برای فعالسازی حساب کاربری خود لطفا کد شش رقمی ارسال شده به شماره همراه {validate.Mobile} را وارد کنید.";
                        return RedirectToAction("ActivateMobile", "Account",
                            new { mobile = validate.Mobile, activateText = activationText });
                    case UserValidationResult.NotExists:
                        return RedirectToAction("UserRegister", "Account",
                            new { mobile = validate.Mobile });
                    case UserValidationResult.Error:
                        TempData[ErrorMessage] = "در فرایند اعتبارسنجی کاربر خطایی رخ داد، لطفا مجددا تلاش کنید.";
                        ModelState.AddModelError("Mobile", "در فرایند اعتبارسنجی کاربر خطایی رخ داد، لطفا بعدا تلاش کنید.");
                        return RedirectToAction("UserValidation", "Account");
                }
            }
            return View();
        }

        #endregion

        #region User Register

        [HttpGet("user-register")]
        public async Task<IActionResult> UserRegister(string mobile)
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
                var result = await _userService.UserRegister(register);

                switch (result)
                {
                    case UserRegisterResult.Success:
                        TempData[SuccessMessage] = "کاربر با موفقیت ثبت گردید.";
                        string activationText =
                            $"لطفا کد شش رقمی ارسال شده به شماره همراه {register.Mobile} را وارد کنید.";
                        return RedirectToAction("ActivateMobile", "Account",
                            new { mobile = register.Mobile, activateText = activationText });
                    case UserRegisterResult.MobileExists:
                        return RedirectToAction("UserLogin", "Account",
                            new { mobile = register.Mobile });
                    case UserRegisterResult.Error:
                        TempData[ErrorMessage] = "در ثبت اطلاعات خطایی رخ داد، لطفا مجددا امتحان نمایید.";
                        break;
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

        [HttpPost("user-login"), ValidateAntiForgeryToken]
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
                    case UserLoginResult.MobileNotActivated:
                        TempData[ErrorMessage] = $"شماره همراه {mobile} فعال نشده است.";
                        ModelState.AddModelError("Mobile", "شماره همراه شما فعال نشده است.");
                        return RedirectToAction("ActivateMobile", "Account",
                            new { mobile = login.Mobile });
                    case UserLoginResult.UserNotFound:
                        return RedirectToAction("UserRegister", "Account",
                            new { mobile = login.Mobile });
                    case UserLoginResult.WrongPassword:
                        TempData[ErrorMessage] = "رمز عبور اشتباه می باشد.";
                        ModelState.AddModelError("Password", "رمز عبور اشتباه می باشد.");
                        TempData[InfoMessage] =
                            "می توانید برای بازیابی رمز عبور خود از سرویس بازیابی رمز عبور استفاده کنید";
                        break;
                    case UserLoginResult.Error:
                        TempData[ErrorMessage] = "در ورود به حساب کاربری خطایی رخ داد، لطفا مجددا تلاش نمایید.";
                        break;
                    case UserLoginResult.Success:

                        var user = await _userService.GetUserByMobile(mobile);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.MobilePhone, mobile),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
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

                        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }

                        if (user.TwoFactorAuthentication == true)
                        {
                            return RedirectToAction();
                        }

                        TempData[InfoMessage] = "شما با موفقیت وارد حساب کاربری خود شدید.";

                        return Redirect("/");
                }
            }

            return View();
        }

        #endregion

        #region User Logout

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData[InfoMessage] = "شما از حساب کاربری خارج شدید";
            return Redirect("/");
        }

        #endregion

        #region Activation Mobile

        [HttpGet("activation-mobile/{mobile}")]
        public async Task<IActionResult> ActivateMobile(string mobile, string activateText)
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return Redirect("/");
            }

            var activateMobile = new ActivateMobileDto { Mobile = mobile };
            ViewBag.ActivationText = activateText;

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
                    return RedirectToAction("UserLogin", "Account",
                        new { mobile = activateMobile.Mobile });
                }

                TempData[ErrorMessage] = "متاسفانه حساب کاربری شما فعال نشد، لطفا مجددا تلاش کنید.";
            }

            return View(activateMobile);
        }

        #endregion

        #region Restore User Password

        [HttpGet("restore-user-password/{mobile}")]
        public async Task<IActionResult> RestoreUserPassword()
        {
            if (User.Identity is { IsAuthenticated: true })
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost("restore-user-password/{mobile}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreUserPassword(ForgotPasswordDto forgot)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(forgot.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچای شما تایید نشد.";
                TempData[WarningMessage] = "لطفا از اتصال اینترنت خود اطمینان حاصل فرمایید";
                return View(forgot);
            }

            if (ModelState.IsValid)
            {
                var result = await _userService.RestoreUserPassword(forgot);

                switch (result)
                {
                    case ForgotPasswordResult.Success:
                        TempData[SuccessMessage] = $"رمز عبور جدید با موفقیت به شماره همراه {forgot.Mobile} ارسال شد.";
                        TempData[InfoMessage] = "لطفا پس از ورود به حساب کاربری خود، رمز عبور خود را تغییر دهید.";
                        return RedirectToAction("UserLogin", "Account",
                            new { mobile = forgot.Mobile });
                    case ForgotPasswordResult.UserNotFound:
                        TempData[InfoMessage] =
                            $"کاربری با شماره همراه {forgot.Mobile} وجود ندارد، شما می توانید با این شماره همراه ثبت نام کنید";
                        return RedirectToAction("UserRegister", "Account",
                            new { mobile = forgot.Mobile });
                    case ForgotPasswordResult.Error:
                        TempData[ErrorMessage] = "در فرایند بازیابی رمز عبور خطایی رخ داد، لطفا مجددا تلاش کنید.";
                        break;
                }
            }

            return View(forgot);
        }

        #endregion

        #endregion
    }
}