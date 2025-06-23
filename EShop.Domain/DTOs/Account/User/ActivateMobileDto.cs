using EShop.Domain.DTOs.Site;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Account.User;

public class ActivateMobileDto : CaptchaDto
{
    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(1, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string Mobile { get; set; }

    [Display(Name = "کد فعالسازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(6, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string MobileActiveCode { get; set; }

    [Display(Name = "کد فعالسازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(1, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string digit1 { get; set; }

    [Display(Name = "کد فعالسازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(1, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string digit2 { get; set; }

    [Display(Name = "کد فعالسازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(1, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string digit3 { get; set; }

    [Display(Name = "کد فعالسازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(1, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string digit4 { get; set; }

    [Display(Name = "کد فعالسازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(1, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string digit5 { get; set; }

    [Display(Name = "کد فعالسازی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(1, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(1, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string digit6 { get; set; }
}