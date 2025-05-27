using EShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    #region Site Header

    public class SiteHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteHeader");
        }
    }

    #endregion

    #region Mega Menu

    public class MegaMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("MegaMenu");
        }
    }

    #endregion

    #region Site Footer

    public class SiteFooterViewComponent : ViewComponent
    {
        private readonly ISiteService _siteService;

        public SiteFooterViewComponent(ISiteService siteService)
        {
            _siteService = siteService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var siteSetting = await _siteService.GetDefaultSiteSetting();
            return View("SiteFooter", siteSetting);
        }
    }

    #endregion
}
