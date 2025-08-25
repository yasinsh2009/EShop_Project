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

        var userInfo = await _userService.GetUserById(User.GetUserId());
        ViewBag.AvatarImage = userInfo.AvatarPath ?? string.Empty;

        return View("UserSidebarDashboard");
    }
}