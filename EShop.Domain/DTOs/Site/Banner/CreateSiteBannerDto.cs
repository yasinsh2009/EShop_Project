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

        [Display(Name = "سایز بنر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(25, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ColSize { get; set; }

        public SiteBanner.BannersLocation BannersLocations { get; set; }

        [Display(Name = "فعال / غیرفعال")]
        public bool IsDelete { get; set; }

        #endregion

        public enum BannersLocation
        {
            Home1,
            Home2,
            Home3,
            Home4,
        }

        
    }
    public enum CreateSiteBannerResult
    {
        Success,
        Error
    }
}
