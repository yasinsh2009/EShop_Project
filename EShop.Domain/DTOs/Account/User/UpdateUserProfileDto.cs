using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Account.User;

public class UpdateUserProfileDto
{
    [Display(Name = "نام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string LastName { get; set; }

    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public required string Mobile { get; set; }

    [Display(Name = "ایمیل")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
    public string? Email { get; set; }
}

public enum UpdateUserProfileResult
{
    Success,
    Error,
    NotFound
}
