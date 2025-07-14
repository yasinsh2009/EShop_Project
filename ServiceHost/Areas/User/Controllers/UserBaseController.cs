using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.Areas.User.Controllers
{
    [Authorize]
    [Area("User")]
    [Route("user")]
    public class UserBaseController : Controller
    {
        protected string SuccessMessage = "SuccessMessage";
        protected string WarningMessage = "WarningMessage";
        protected string ErrorMessage = "ErrorMessage";
        protected string InfoMessage = "InfoMessage";
    }
}
