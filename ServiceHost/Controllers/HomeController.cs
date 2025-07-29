using EShop.Application.Services.Implementation;
using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Contact;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.PresentationExtensions;

namespace ServiceHost.Controllers
{
    public class HomeController : SiteBaseController
    {
        private readonly ISiteService _siteService;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly IContactService _contactService;

        public HomeController(ISiteService siteService, ICaptchaValidator captchaValidator, IContactService contactService)
        {
            _siteService = siteService;
            _captchaValidator = captchaValidator;
            _contactService = contactService;
        }

        #region Home

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region About US

        [HttpGet("about-us")]
        public async Task<IActionResult> AboutUs()
        {
            var aboutUs = await _siteService.GetAboutUs();
            return View(aboutUs);
        }

        #endregion

        #region Contact Us

        [HttpGet("contact-us")]
        public async Task<IActionResult> SendContactMessage()
        {
            var siteSetting = await _siteService.GetDefaultSiteSetting();
            ViewBag.SiteSetting = siteSetting;
            return View();
        }

        [HttpPost("contact-us"), ValidateAntiForgeryToken]
        public async Task<IActionResult> SendContactMessage(SendContactMessageDto contact)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(contact.Captcha))
            {
                TempData[ErrorMessage] = "کد کپچای شما تایید نشد.";
                TempData[WarningMessage] = "لطفا از اتصال اینترنت خود اطمینان حاصل فرمایید";
                return View(contact);
            }

            if (ModelState.IsValid)
            {
                var userIp = HttpContext.GetUserIp();
                await _contactService.SendNewContactMessage(contact, userIp, null);
                TempData[SuccessMessage] = "پیام شما با موفقیت ارسال شد.";
                Redirect("/");
            }

            return View(contact);
        }

        #endregion
    }
}
