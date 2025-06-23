using System.ComponentModel.DataAnnotations;
using EShop.Domain.DTOs.Site;

namespace EShop.Domain.DTOs.Account.User;

public class UserValidationDto : CaptchaDto
{
    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string Mobile { get; set; }
}

public enum UserValidationResult
{
    ExistAndActive,
    ExistAndNotActive,
    NotExists,
    Error
}