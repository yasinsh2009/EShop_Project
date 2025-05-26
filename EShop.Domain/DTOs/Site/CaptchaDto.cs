using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Site;

public class CaptchaDto
{
    [Display(Name = "من ربات نیستم")]
    [Required(ErrorMessage = "لطفا تیک {0} را بزنید")]
    public string Captcha { get; set; }
}