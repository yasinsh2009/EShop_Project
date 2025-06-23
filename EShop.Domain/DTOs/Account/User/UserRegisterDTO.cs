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

    [Display(Name = "نام")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string LastName { get; set; }

    [Display(Name = "کلمه ی عبور")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Password { get; set; }

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "لطفا تکرار {0} را وارد کنید")]
    [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
    public string ConfirmPassword { get; set; }

    public List<long> RoleSelectedCategories { get; set; } = [2];
}

public enum UserRegisterResult
{
    Success,
    MobileExists,
    Error
}