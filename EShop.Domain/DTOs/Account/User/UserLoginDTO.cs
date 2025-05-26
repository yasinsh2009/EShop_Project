using System.ComponentModel.DataAnnotations;
using EShop.Domain.DTOs.Site;

namespace EShop.Domain.DTOs.Account.User;

public class UserLoginDto : CaptchaDto
{
    [Display(Name = "کلمه ی عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Password { get; set; }

    [Display(Name = "نمک")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Salt { get; set; }

    public bool RememberMe { get; set; }
}

public enum UserLoginResult
{
    Success,
    MobileNotActivated,
    NotFound,
    WrongInformation,
    Error
}