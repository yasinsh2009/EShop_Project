using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.DTOs.Site;

public class FeatureDto
{
    #region Properties

    public long Id { get; set; }

    [Display(Name = "عنوان ویژگی")] 
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(550, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string FeatureTitle { get; set; }

    [Display(Name = "تصویر/آیکون ویژگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Image { get; set; }

    #endregion
}
