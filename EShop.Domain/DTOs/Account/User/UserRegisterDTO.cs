using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Domain.DTOs.Account.User;

public class UserRegisterDTO
{
    public long RoleId { get; set; }
    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string Mobile { get; set; }

    [Display(Name = "ایمیل")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name = "نام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string LastName { get; set; }


    [Display(Name = "کلمه ی عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Password { get; set; }

    [Display(Name = "تکرار (تائید) کلمه ی عبور")]
    [Required(ErrorMessage = "لطفا تکرار {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [Compare("Password", ErrorMessage = "کلمه های عبور مغایرت دارند")]
    public string ConfirmPassword { get; set; }

    [Display(Name = "نمک")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Salt { get; set; }

    [Display(Name = "تصویر آواتار")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public IFormFile? AvatarPath { get; set; }

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