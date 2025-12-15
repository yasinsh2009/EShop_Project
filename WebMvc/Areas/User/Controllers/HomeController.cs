using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.WebMvc.Areas.User.Controllers
{
    public class HomeController : UserBaseController
    {
        #region User Dashboard

        [HttpGet("user-dashboard")]
        public async Task<IActionResult> UserDashboard()
        {
            return View();
        }

        #endregion
    }
}
