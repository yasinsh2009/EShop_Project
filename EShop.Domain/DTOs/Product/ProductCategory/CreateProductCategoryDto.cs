﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.DTOs.Product.ProductCategory
{
    public class CreateProductCategoryDto
    {
        #region properties

        public long Id { get; set; }
        public long? ParentId { get; set; }

        [Display(Name = "عنوان دسته بندی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "تصویر دسته بندی")]
        public IFormFile? Image { get; set; }

        [Display(Name = "تصویر دسته بندی")]
        public string? ExistingImage { get; set; }

        [Display(Name = "عنوان در لینک URL")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string UrlName { get; set; }

        [Display(Name = "آیکون")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? Icon { get; set; }

        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }

        public string ParentName { get; set; }

        #endregion
    }

    public enum CreateProductCategoryResult
    {
        Success,
        Error,
        ImageErrorType,
    }
}
