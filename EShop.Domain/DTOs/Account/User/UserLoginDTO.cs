﻿using System.ComponentModel.DataAnnotations;
using EShop.Domain.DTOs.Site;

namespace EShop.Domain.DTOs.Account.User;

public class UserLoginDto : CaptchaDto
{
    [Display(Name = "تلفن همراه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(11, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(11, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
    public string Mobile { get; set; }

    [Display(Name = "کلمه ی عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}

public enum UserLoginResult
{
    Success,
    MobileNotActivated,
    UserNotFound,
    WrongPassword,
    Error
}