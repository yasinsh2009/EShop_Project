using ECommerceApp.Application.Services.Interface;
using ECommerceApp.WebMvc.PresentationExtensions;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.WebMvc.Areas.User.ViewComponents;

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