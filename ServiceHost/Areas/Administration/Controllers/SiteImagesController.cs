using EShop.Application.Services.Implementation;
using EShop.Application.Services.Interface;
using EShop.Domain.DTOs.Site.Slider;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.PresentationExtensions;

namespace ServiceHost.Areas.Administration.Controllers
{
    public class SiteImagesController : AdminBaseController
    {
        #region Ctor

        private readonly ISiteImagesService _siteImagesService;
        private readonly IUserService _userService;

        public SiteImagesController(ISiteImagesService siteImagesService, IUserService userService)
        {
            _siteImagesService = siteImagesService;
            _userService = userService;
        }

        #endregion

        #region Slider

        [HttpGet("Slides")]
        public async Task<IActionResult> Slides()
        {
            var slides = await _siteImagesService.GetAllSlides();
            return View(slides);
        }

        [HttpGet("CreateSlide")]
        public IActionResult CreateSlide()
        {
            return View();
        }

        [HttpPost("CreateSlide"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSlide(CreateSliderDto slide, IFormFile? slideImage, IFormFile? slideMobileImage)
        {
            if (ModelState.IsValid)
            {
                var result = await _siteImagesService.CreateSlide(slide, slideImage, slideMobileImage);

                if (result == CreateSliderResult.Success)
                {
                    TempData[SuccessMessage] = "اسلاید جدید با موفقیت ایجاد شد";
                    return RedirectToAction("Slides", "SiteImages", new { area = "Administration" });
                }
                else
                {
                    TempData[ErrorMessage] = "عملیات با خطا مواجه شد، لطفا مجددا تلاش کنید";
                }
            }
            return View();
        }

        [HttpGet("EditSlide/{slideId}")]
        public async Task<IActionResult> EditSlide(long slideId)
        {
            var slide = await _siteImagesService.GetSlideForEdit(slideId);
            if (slide == null) return NotFound();
            return View(slide);
        }

        [HttpPost("EditSlide/{slideId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSlide(EditSliderDto slide, IFormFile? slideImage, IFormFile? slideMobileImage)
        {
            if (ModelState.IsValid)
            {
                var editorName = await _userService.GetUserFullNameById(User.GetUserId());
                var result = await _siteImagesService.EditSlide(slide, slideImage, slideMobileImage, editorName);

                switch (result)
                {
                    case EditSliderResult.Success:
                        TempData[SuccessMessage] = "اسلاید موردنظر با موفقیت ویرایش شد.";
                        return RedirectToAction("Slides", "SiteImages", new { area = "Administration" });
                    case EditSliderResult.Error:
                        TempData[ErrorMessage] = "عملیات با خطا مواجه شد، لطفا مجددا تلاش کنید";
                        break;
                    case EditSliderResult.NotFound:
                        TempData[WarningMessage] = "متاسفانه اسلایدی با این مشخصات یافت نشد.";
                        break;
                }
            }

            return View(slide);
        }

        [HttpGet("ActivateSlide/{slideId}")]
        public async Task<IActionResult> ActivateSlide(long slideId)
        {
            var result = await _siteImagesService.ActivateSlide(slideId);
            if (result)
            {
                TempData[SuccessMessage] = "اسلاید موردنظر با موفقیت فعال شد.";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با خطا مواجه شد، لطفا مجددا تلاش کنید";
            }
            return RedirectToAction("Slides", "SiteImages", new { area = "Administration" });
        }

        [HttpGet("DeActivateSlide/{slideId}")]
        public async Task<IActionResult> DeActivateSlide(long slideId)
        {
            var result = await _siteImagesService.DeActivateSlide(slideId);
            if (result)
            {
                TempData[SuccessMessage] = "اسلاید موردنظر با موفقیت غیرفعال شد.";
            }
            else
            {
                TempData[ErrorMessage] = "عملیات با خطا مواجه شد، لطفا مجددا تلاش کنید";
            }
            return RedirectToAction("Slides", "SiteImages", new { area = "Administration" });
        }
        #endregion
    }
}
