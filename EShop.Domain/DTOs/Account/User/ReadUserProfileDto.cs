using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Account.User;

public class ReadUserProfileDto
{
    public long Id { get; set; }

    [Display(Name = "نام")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string FirstName { get; set; }

    [Display(Name = "نام خانوادگی")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string LastName { get; set; }

    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string Mobile { get; set; }

    [Display(Name = "ایمیل")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Display(Name = "تصویر آواتار")]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string? AvatarPath { get; set; }

    public string RegisterDate { get; set; }
}