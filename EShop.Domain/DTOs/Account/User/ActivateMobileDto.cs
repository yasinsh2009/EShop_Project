using EShop.Domain.DTOs.Site;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Account.User;

public class ActivateMobileDto : CaptchaDto
{
    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string Mobile { get; set; }

    [Display(Name = "کد فعالسازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(6, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string MobileActiveCode { get; set; }
}