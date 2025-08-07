using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.Administration.ViewComponents
{
    public class SidebarDashboardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SidebarDashboard");
        }
    }

    public class MainHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("MainHeader");
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
