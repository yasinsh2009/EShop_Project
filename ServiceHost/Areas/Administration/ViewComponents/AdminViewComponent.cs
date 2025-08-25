using EShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.PresentationExtensions;

namespace ServiceHost.Areas.Administration.ViewComponents
{

    public class AdminSidebarDashboardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminSidebarDashboard");
        }
    }

    public class AdminHeaderViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public AdminHeaderViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userInfo = await _userService.GetUserById(User.GetUserId());
            ViewBag.Avatar = userInfo.AvatarPath;
            ViewBag.Email = userInfo.Email;

            return View("AdminHeader");
        }
    }

    public class AdminFooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AdminFooter");
        }
    }
}
