using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Account.User;

public class ChangeUserPasswordDto
{
    [Display(Name = "رمز عبور فعلی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string CurrentPassword { get; set; }

    [Display(Name = "رمز عبور جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string NewPassword { get; set; }

    [Display(Name = "تکرار رمز عبور جدید")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [Compare("NewPassword", ErrorMessage = "کلمه های عبور مغایرت دارند")]
    public string ConfirmNewPassword { get; set; }
}

public enum ChangeUserPasswordResult
{
    Success,
    NotFound,
    WrongCurrentPassword,
    CurrentPasswordSameAsNew,
    Error
}