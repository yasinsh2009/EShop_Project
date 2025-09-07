using System.ComponentModel.DataAnnotations;
using EShop.Domain.Entities.Site;

namespace EShop.Domain.DTOs.Site.Banner
{
    public class CreateSiteBannerDto
    {
        #region Properties

        [Display(Name = "آدرس بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Link { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Description { get; set; }

        public SiteBannerGridColumnSize GridColumnSize { get; set; }

        public SiteBannerPlacement Placement { get; set; }

        [Display(Name = "فعال / غیرفعال")]
        public bool IsActive { get; set; }

        #endregion
    }

    public enum CreateSiteBannerResult
    {
        Success,
        Error
    }
}
