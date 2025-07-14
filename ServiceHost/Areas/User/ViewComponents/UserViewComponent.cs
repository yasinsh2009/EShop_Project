using EShop.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.PresentationExtensions;

namespace ServiceHost.Areas.User.ViewComponents;

public class UserSidebarDashboardViewComponent : ViewComponent
{
    private readonly IUserService _userService;

    public UserSidebarDashboardViewComponent(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        ViewBag.AvatarImage = await _userService.GetUserAvatar(User.GetUserId()) ?? string.Empty;
        ViewBag.UserRoleName = await _userService.GetUserRole(User.GetUserId());

        return View("UserSidebarDashboard");
    }
}