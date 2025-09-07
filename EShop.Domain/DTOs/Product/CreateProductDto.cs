﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Product
{
    public class CreateProductDto
    {
        public long? BrandId { get; set; }
        
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "نام برند")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? BrandName { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "فقط اعداد مجاز می باشد")]
        public int Price { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? ShortDescription { get; set; }

        [Display(Name = "توضیحات اصلی")]
        public string? Description { get; set; }

        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }

        [Display(Name = "تصویر محصول")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public IFormFile? Image { get; set; }

        //public List<CreateProductColorDto> ProductColors { get; set; }
        //public List<CreateProductSizeDto> ProductSize { get; set; }
        //public List<CreateProductFeatureDto> ProductFeatures { get; set; }
        //public List<long> SelectedCategories { get; set; }

    }

    public enum CreateProductResult
    {
        Success,
        Error,
        ImageErrorType,
        //BrandNotFound
    }
}
