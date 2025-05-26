using System.ComponentModel.DataAnnotations;
using EShop.Domain.DTOs.Site;
using Microsoft.AspNetCore.Http;

namespace EShop.Domain.DTOs.Account.User;

public class UserRegisterDto : CaptchaDto
{
    public long RoleId { get; set; }

    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string Mobile { get; set; }

    public List<long> RoleSelectedCategories { get; set; } = new List<long>
    {
        2
    };
}

public enum UserRegisterResult
{
    Success,
    MobileExists,
    Error
}