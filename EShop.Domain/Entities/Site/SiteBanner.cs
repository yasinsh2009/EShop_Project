using System.ComponentModel.DataAnnotations;
using EShop.Domain.Entities.Common;

namespace EShop.Domain.Entities.Site
{
    public class SiteBanner : BaseEntity
    {
        #region Properties

        [Display(Name = "تصویر")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ImageName { get; set; }

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

        public BannersLocation BannersLocations { get; set; }

        #endregion

        public enum BannersLocation
        {
            [Display(Name = "اول")]
            Home1,

            [Display(Name = "دوم")]
            Home2,

            [Display(Name = "سوم")]
            Home3,

            [Display(Name = "چهارم")]
            Home4
        }
    }
}
