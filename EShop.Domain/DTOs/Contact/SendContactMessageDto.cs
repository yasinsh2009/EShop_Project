﻿using EShop.Domain.DTOs.Site;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Contact;

public class SendContactMessageDto :  CaptchaDto
{
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده صحیح نمی باشد")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Email { get; set; }

    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Fullname { get; set; }

    [Display(Name = "عنوان پیام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string MessageSubject { get; set; }

    [Display(Name = "متن پیام")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string MessageText { get; set; }
}