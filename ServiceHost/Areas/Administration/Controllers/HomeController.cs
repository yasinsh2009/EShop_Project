using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Contact;
using EShop.Domain.DTOs.Site;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.PresentationExtensions;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region Ctor

        private readonly IUserService _userService;
        private readonly ISiteService _siteService;
        private readonly IContactService _contactService;

        public HomeController(IUserService userService, ISiteService siteService, IContactService contactService)
        {
            _userService = userService;
            _siteService = siteService;
            _contactService = contactService;
        }

        #endregion

        #region Home

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Site Setting

        [HttpGet("site-setting")]
        public async Task<IActionResult> GetDefaultSiteSetting()
        {
            var setting = await _siteService.GetDefaultSiteSetting();
            return View(setting);
        }

        [HttpGet("site-setting/{settingId}")]
        public async Task<IActionResult> EditSiteSetting(long settingId)
        {
            var setting = await _siteService.GetSiteSettingForEdit(settingId);
            return View(setting);
        }

        [HttpPost("site-setting/{settingId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSiteSetting(EditSiteSettingDto setting)
        {
            var userName = await _userService.GetUserFullNameById(User.GetUserId());
            var result = await _siteService.EditSiteSetting(setting, userName);

            switch (result)
            {
                case EditSiteSettingResult.Success:
                    TempData[SuccessMessage] = "تغییرات با موفقیت اعمال شد.";
                    return RedirectToAction("GetDefaultSiteSetting", "Home", new { area = "Administration" });
                case EditSiteSettingResult.NotFound:
                    TempData[ErrorMessage] = "هیچ تنظیمات سایتی با این اطلاعات یافت نشد.";
                    break;
                case EditSiteSettingResult.Error:
                    TempData[ErrorMessage] = "در فرایند به روز رسانی تنظیمات سایت اختلالی پیش آمد، لطفا بعدا تلاش کنید.";
                    break;
            }
            return View(setting);
        }

        #endregion

        #region Contact Us

        [HttpGet("contact-messages")]
        public async Task<IActionResult> FilterContactMessages(FilterContactMessagesDto contactMessage)
        {
            var message = await _contactService.FilterContactMessages(contactMessage);
            return View(message);
        }

        #endregion

        #region About Us

        [HttpGet("about-us")]
        public async Task<IActionResult> GetAboutUs()
        {
            var about = await _siteService.GetAboutUs();
            return View(about);
        }

        #endregion
    }
}
