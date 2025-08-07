﻿using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Site;

public class AboutUsDto
{
    #region Properties

    public long Id { get; set; }

    [Display(Name = "عنوان هدر")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(550, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]

    public string HeaderTitle { get; set; }

    [Display(Name = "توضیحات")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Description { get; set; }

    public string CreateDate { get; set; }

    public string LastUpdateDate { get; set; }
    #endregion
}