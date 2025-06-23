using EShop.Domain.DTOs.Site;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Account.User;

public class ForgotPasswordDto : CaptchaDto
{
    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(1, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string Mobile { get; set; }
}

public enum ForgotPasswordResult
{
    Success,
    UserNotFound,
    Error
}